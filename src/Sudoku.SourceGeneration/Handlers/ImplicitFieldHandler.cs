namespace Sudoku.SourceGeneration.Handlers;

/// <summary>
/// The generator handler for implicit fields.
/// </summary>
internal static class ImplicitFieldHandler
{
	public static void Output(SourceProductionContext spc, ImmutableArray<CollectedResult> values)
	{
		var types = new List<string>();
		foreach (var group in values.GroupBy(static value => value.ContainingType, (IEqualityComparer<ITypeSymbol>)SymbolEqualityComparer.Default))
		{
			var type = group.Key;
			var @namespace = type.ContainingNamespace;
			var fieldDeclarations = new List<string>();
			foreach (var (_, property, readOnlyModifier) in group)
			{
				var readOnlyKeyword = readOnlyModifier ? "readonly " : string.Empty;
				var nullableToken = property.Type.NullableAnnotation == Annotated ? "?" : string.Empty;
				fieldDeclarations.Add(
					$$"""
					/// <summary>
							/// Indicates the backing field of property <see cref="{{property.Name}}"/>.
							/// </summary>
							/// <seealso cref="{{property.Name}}"/>
							[global::System.CodeDom.Compiler.GeneratedCodeAttribute("{{typeof(ImplicitFieldHandler).FullName}}", "{{Value}}")]
							[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
							private {{readOnlyKeyword}}{{property.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)}}{{nullableToken}} {{property.Name.ToUnderscoreCamelCasing()}};
					"""
				);
			}

			var typeKindString = type switch
			{
				{ TypeKind: TypeKind.Class, IsRecord: true } => "record",
				{ TypeKind: TypeKind.Class } => "class",
				{ TypeKind: TypeKind.Struct, IsRecord: true } => "record struct",
				{ TypeKind: TypeKind.Struct } => "struct",
				{ TypeKind: TypeKind.Interface } => "interface"
			};
			types.Add(
				$$"""
				namespace {{@namespace.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)["global::".Length..]}}
				{
					partial {{typeKindString}} {{type.Name}}
					{
						{{string.Join("\r\n\r\n\t\t", fieldDeclarations)}}
					}
				}
				"""
			);
		}

		spc.AddSource(
			"ImplicitField.g.cs",
			$"""
			{Banner.AutoGenerated}

			#nullable enable

			{string.Join("\r\n\r\n", types)}
			"""
		);
	}

	/// <inheritdoc/>
	public static CollectedResult? Transform(GeneratorAttributeSyntaxContext gasc, CancellationToken _)
	{
		if (gasc is not
			{
				TargetSymbol: IPropertySymbol { ContainingType: var containingType } property,
				Attributes: [{ NamedArguments: var namedArgs }],
				TargetNode: PropertyDeclarationSyntax { Parent: TypeDeclarationSyntax { Modifiers: var modifiers and not [] } }
			})
		{
			return null;
		}

		if (!modifiers.Any(SyntaxKind.PartialKeyword))
		{
			return null;
		}

		var readOnlyKeyword = namedArgs.TryGetValueOrDefault<bool>("RequiredReadOnlyModifier", out var r) && r;
		return new(containingType, property, readOnlyKeyword);
	}


	/// <summary>
	/// Indicates the data collected via <see cref="ImplicitFieldHandler"/>.
	/// </summary>
	/// <seealso cref="ImplicitFieldHandler"/>
	internal sealed record CollectedResult(INamedTypeSymbol ContainingType, IPropertySymbol Property, bool ReadOnlyModifier);
}
