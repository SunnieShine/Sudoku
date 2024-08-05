namespace Sudoku.SourceGeneration.Handlers;

internal static class FactoryPropertyHandler
{
	public static void Output(SourceProductionContext spc, ImmutableArray<CollectedResult> values)
	{
		var result = new List<string>();
		foreach (var typeGroup in
			values.GroupBy<CollectedResult, INamedTypeSymbol>(static r => r.TypeSymbol, SymbolEqualityComparer.Default))
		{
			var typeSymbol = typeGroup.Key;
			var methodDeclarations = string.Join(
				"\r\n\r\n",
				from collectedResult in typeGroup select collectedResult.MethodDeclarationCodeSnippet
			);

			var namespaceString = typeSymbol.ContainingNamespace.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
			result.Add(
				$$"""
				namespace {{namespaceString["global::".Length..]}}
				{
					/// <summary>
					/// Provides with a list of methods that are used as factory methods on modifying properties with multiple method invocations:
					/// <code><![CDATA[
					/// var instance = new Instance()
					///     .WithProperty1(42)
					///     .WithProperty2(100)
					///     .WithFoo(static () => { })
					///     .WithBar(null)
					///     .WithBaz(1, 2, 3);
					/// ]]></code>
					/// </summary>
					[global::System.CodeDom.Compiler.GeneratedCodeAttribute("{{typeof(FactoryPropertyHandler).FullName}}", "{{Value}}")]
					[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
					public static class __{{typeSymbol.Name}}FactoryMethods
					{
				{{string.Join("\r\n\r\n\t\t", methodDeclarations)}}
					}
				}
				"""
			);
		}

		spc.AddSource(
			"FactoryProperty.g.cs",
			$$"""
			{{Banner.AutoGenerated}}
			
			#nullable enable

			{{string.Join("\r\n\r\n", result)}}
			"""
		);
	}

	public static CollectedResult? Transform(GeneratorAttributeSyntaxContext gasc, CancellationToken cancellationToken)
	{
		if (gasc is not
			{
				TargetSymbol: IPropertySymbol
				{
					ContainingType: { TypeKind: var typeKind, TypeParameters: var typeParameters } type,
					DeclaredAccessibility: var accessibility,
					Name: var propertyName,
					Type: var propertyType,
					IsIndexer: false,
					IsReadOnly: false
				},
				Attributes: [{ NamedArguments: var namedArguments }]
			})
		{
			return null;
		}

		var typeParametersRawString = string.Join(", ", from tp in typeParameters select tp.ToDisplayString());
		var typeParametersString = typeParameters.Length == 0 ? string.Empty : $"<{typeParametersRawString}>";
		var typeString = type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
		var typeModifier = typeKind == TypeKind.Struct ? "ref " : string.Empty;
		var parameterModifiersString = namedArguments.TryGetValueOrDefault<string>("ParameterModifiers", out var m) ? $"{m} " : string.Empty;
		var parameterNameString = namedArguments.TryGetValueOrDefault<string>("ParameterName", out var p)
			? p
			: propertyName.ToCamelCasing();
		var propertyTypeString = (namedArguments.TryGetValueOrDefault<ITypeSymbol>("ParameterType", out var pt) ? pt! : propertyType)
			.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
		var methodSuffixNameString = namedArguments.TryGetValueOrDefault<string>("MethodSuffixName", out var ms)
			? ms
			: propertyName.ToPascalCasing();
		var accessibilityString = namedArguments.TryGetValueOrDefault<string>("Accessibility", out var a)
			? a
			: accessibility switch
			{
				DeclaredAccessibility.Private => "private ",
				DeclaredAccessibility.ProtectedAndInternal => "private protected ",
				DeclaredAccessibility.Protected => "protected ",
				DeclaredAccessibility.Internal => "internal ",
				DeclaredAccessibility.ProtectedOrInternal => "protected internal ",
				DeclaredAccessibility.Public => "public ",
				_ => string.Empty
			};
		return new(
			type,
			$$"""
					/// <summary>
					/// Sets the property <see cref="{{typeString}}.{{propertyName}}"/> with the target value.
					/// </summary>
					/// <param name="instance">The instance to be set or updated.</param>
					/// <param name="{{parameterNameString}}">The value to be set or updated.</param>
					/// <returns>The value same as <see cref="{{typeString}}"/>.</returns>
					[global::System.CodeDom.Compiler.GeneratedCodeAttribute("{{typeof(FactoryPropertyHandler).FullName}}", "{{Value}}")]
					[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
					{{accessibilityString}}static {{typeModifier}}{{typeString}} With{{methodSuffixNameString}}{{typeParametersString}}(this {{typeModifier}}{{typeString}} instance, {{parameterModifiersString}}{{propertyTypeString}} {{parameterNameString}})
					{
						instance.{{propertyName}} = {{parameterNameString}};
						return instance;
					}
			"""
		);
	}


	public sealed record CollectedResult(INamedTypeSymbol TypeSymbol, string MethodDeclarationCodeSnippet);
}
