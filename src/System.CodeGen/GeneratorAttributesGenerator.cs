﻿namespace Sudoku.Diagnostics.CodeGen.Generators;

/// <summary>
/// Defines a source generator that produces generator-related attributes.
/// </summary>
[Generator(LanguageNames.CSharp)]
public sealed class GeneratorAttributesGenerator : IIncrementalGenerator
{
	/// <inheritdoc/>
	public void Initialize(IncrementalGeneratorInitializationContext context)
		=> context.RegisterSourceOutput(
			context.CompilationProvider,
			(spc, compilation) => spc.AddSource(
				$"Attributes.g.{Shortcuts.GeneratorAttributes}.cs",
				$$"""
				// <auto-generated/>

				#pragma warning disable CS1591
				#nullable enable

				namespace {{GeneratorAttributesNamespace}};

				[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
				[global::System.CodeDom.Compiler.GeneratedCodeAttribute("{{GetType().FullName}}", "{{VersionValue}}")]
				[global::System.AttributeUsageAttribute(global::System.AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
				public sealed class {{AutoExtensionDeconstructionAttribute}} : global::System.Attribute
				{
					public {{AutoExtensionDeconstructionAttribute}}(global::System.Type type, params string[] memberExpressions)
						=> (Type, MemberExpressions) = (
							type.IsAssignableTo(typeof(global::System.Delegate)) || type.IsAssignableTo(typeof(global::System.Enum))
								? throw new global::System.ArgumentException("The type cannot be a delegate or enumeration.", nameof(type))
								: type,
							memberExpressions.Length == 0
								? throw new global::System.ArgumentException("The argument cannot be empty.", nameof(memberExpressions))
								: memberExpressions
						);

					public bool EmitsInKeyword { get; init; } = false;

					public string? Namespace { get; init; } = null;

					public string[] MemberExpressions { get; }

					public global::System.Type Type { get; }
				}

				[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
				[global::System.CodeDom.Compiler.GeneratedCodeAttribute("{{GetType().FullName}}", "{{VersionValue}}")]
				[global::System.AttributeUsageAttribute(global::System.AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
				public sealed class {{EnumSwitchExpressionArmAttribute}} : global::System.Attribute
				{
					public {{EnumSwitchExpressionArmAttribute}}(string key, string value) => (Key, Value) = (key, value);


					public string Key { get; }

					public string Value { get; }
				}

				[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
				[global::System.CodeDom.Compiler.GeneratedCodeAttribute("{{GetType().FullName}}", "{{VersionValue}}")]
				public enum {{EnumSwitchExpressionDefaultBehavior}} : byte
				{
					ReturnIntegerValue,

					ReturnNull,

					Throw
				}
					
				[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
				[global::System.CodeDom.Compiler.GeneratedCodeAttribute("{{GetType().FullName}}", "{{VersionValue}}")]
				[global::System.AttributeUsageAttribute(global::System.AttributeTargets.Enum, AllowMultiple = true, Inherited = false)]
				public sealed class {{EnumSwitchExpressionRootAttribute}} : global::System.Attribute
				{
					public {{EnumSwitchExpressionRootAttribute}}(string key) => Key = key;


					public string? MethodDescription { get; init; }

					public string? ThisParameterDescription { get; init; }

					public string? ReturnValueDescription { get; init; }

					public string Key { get; }

					public global::{{GeneratorAttributesNamespace}}.{{EnumSwitchExpressionDefaultBehavior}} DefaultBehavior { get; init; } = global::{{GeneratorAttributesNamespace}}.{{EnumSwitchExpressionDefaultBehavior}}.Throw;
				}
				"""
			)
		);
}
