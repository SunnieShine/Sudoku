﻿namespace System.CommandLine;

/// <summary>
/// Defines a command line parser that can parse the command line arguments as real instances.
/// </summary>
public static class Parser
{
	/// <summary>
	/// Try to parse the command line arguments and apply to the options into the specified instance.
	/// </summary>
	/// <param name="commandLineArguments">Indicates the command line arguments.</param>
	/// <param name="rootCommand">The option instance that stores the options.</param>
	/// <remarks>
	/// Due to using reflection, the type argument must be a <see langword="class"/> in order to prevent
	/// potential boxing and unboxing operations, which will make an unexpected error that the assignment
	/// will always be failed on <see langword="struct"/> types.
	/// </remarks>
	/// <exception cref="CommandLineParserException">
	/// Throws when the command line arguments is <see langword="null"/> or empty currently,
	/// or the command name is invalid.
	/// </exception>
	public static void ParseAndApplyTo(string[] commandLineArguments, IRootCommand rootCommand)
	{
		var typeOfRootCommand = rootCommand.GetType();
		switch (rootCommand)
		{
			// Special case: If the type is the special one, just return.
			case ISpecialCommand:
			{
				const string propName = nameof(IRootCommand.SupportedCommands);
				if (commandLineArguments is [var c]
					&& FetchPropertyValue<string[]>(propName, typeOfRootCommand) is var tempSupportedNames
					&& tempSupportedNames.Any(e => e.Equals(c, StringComparison.OrdinalIgnoreCase)))
				{
					return;
				}

				throw new CommandLineParserException(ParserError.SpecialCommandDoNotRequireOtherArguments);
			}
			default:
			{
				break;
			}
		}

		// Checks the validity of the command line arguments.
		if (commandLineArguments is not [var possibleCommandName, .. var otherArgs])
		{
			throw new CommandLineParserException(ParserError.ArgumentFormatInvalid);
		}

		// Checks whether the current command line name matches the specified one.
		bool rootCommandMatcher(string e) => e.Equals(possibleCommandName, StringComparison.OrdinalIgnoreCase);
		string[] supportedCommands = FetchPropertyValue<string[]>(nameof(IRootCommand.SupportedCommands), typeOfRootCommand);
		if (!supportedCommands.Any(rootCommandMatcher))
		{
			throw new CommandLineParserException(ParserError.CommandNameIsInvalid);
		}

		// Now gets the information of the global configration.
		var targetAssembly = typeOfRootCommand.Assembly;
		var globalOptions = targetAssembly.GetCustomAttribute<GlobalConfigurationAttribute>() ?? new();

		// Checks for each argument of type string, and assigns the value using reflection.
		int i = 0;
		while (i < otherArgs.Length)
		{
			// Gets the name of the command.
			string currentArg = otherArgs[i];
			if (globalOptions.FullCommandNamePrefix is var fullCommandNamePrefix
				&& currentArg.StartsWith(fullCommandNamePrefix)
				&& currentArg.Length > fullCommandNamePrefix.Length)
			{
				// Okay. Long name.
				string realSubcommand = currentArg[fullCommandNamePrefix.Length..];

				// Then find property in the type.
				var properties = (
					from propertyInfo in typeOfRootCommand.GetProperties()
					where propertyInfo is { CanRead: true, CanWrite: true }
					let attribute = propertyInfo.GetCustomAttribute<CommandAttribute>()
					where attribute?.FullName.Equals(realSubcommand, StringComparison.OrdinalIgnoreCase) ?? false
					select propertyInfo
				).ToArray();
				if (properties is not [{ PropertyType: var propertyType } property])
				{
					throw new CommandLineParserException(ParserError.ArgumentsAmbiguousMatchedOrMismatched);
				}

				// Assign the real value.
				assignPropertyValue(property, propertyType);

				// Advances the move.
				i += 2;
			}
			else if (
				globalOptions.ShortCommandNamePrefix is var shortCommandNamePrefix
				&& currentArg.StartsWith(shortCommandNamePrefix)
				&& currentArg.Length == shortCommandNamePrefix.Length + 1
			)
			{
				// Okay. Short name.
				char realSubcommand = currentArg[^1];

				// Then find property in the type.
				var properties = (
					from propertyInfo in typeOfRootCommand.GetProperties()
					where propertyInfo is { CanRead: true, CanWrite: true }
					let attribute = propertyInfo.GetCustomAttribute<CommandAttribute>()
					where attribute?.ShortName == realSubcommand
					select propertyInfo
				).ToArray();
				if (properties is not [{ PropertyType: var propertyType } property])
				{
					throw new CommandLineParserException(ParserError.ArgumentsAmbiguousMatchedOrMismatched);
				}

				// Assign the real value.
				assignPropertyValue(property, propertyType);

				// Advances the move.
				i += 2;
			}
			else
			{
				// Mismatched.
				throw new CommandLineParserException(ParserError.ArgumentMismatched);
			}


			void assignPropertyValue(PropertyInfo property, Type propertyType)
			{
				if (i + 1 >= otherArgs.Length)
				{
					throw new CommandLineParserException(ParserError.ArgumentExpected);
				}

				// Converts the real argument value into the target property typed instance.
				string realValue = otherArgs[i + 1];
				var propertyConverterAttribute = property.GetCustomAttribute<CommandConverterAttribute>();
				if (propertyConverterAttribute is { ConverterType: var converterType })
				{
					// Creates a converter instance.
					var instance = (IValueConverter)Activator.CreateInstance(converterType)!;

					// Set the value to the property.
					property.SetValue(rootCommand, instance.Convert(realValue));
				}
				else
				{
					property.SetValue(
						rootCommand,
						propertyType == typeof(string)
							? realValue
							: throw new CommandLineParserException(ParserError.ConvertedTypeMustBeString));
				}
			}
		}
	}

	/// <summary>
	/// To fetch the property value via reflection.
	/// </summary>
	/// <typeparam name="TClass">The type of the target property.</typeparam>
	/// <param name="name">The name of the <see langword="static"/> property.</param>
	/// <param name="type">The containing type.</param>
	/// <returns>The instance of type <typeparamref name="TClass"/>.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static TClass FetchPropertyValue<TClass>(string name, Type type) where TClass : class =>
		(TClass)type.GetProperty(name, BindingFlags.Public | BindingFlags.Static)!.GetValue(null)!;
}
