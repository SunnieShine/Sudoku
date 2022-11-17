﻿namespace Sudoku.Diagnostics.CodeGen.Generators;

/// <summary>
/// Defines a source generator that generates the source code for default-overridden members
/// from type <see cref="object"/> or <see cref="ValueType"/>.
/// </summary>
/// <seealso cref="object"/>
/// <seealso cref="ValueType"/>
[Generator(LanguageNames.CSharp)]
public sealed class DefaultOverriddenMembersGenerator : IIncrementalGenerator
{
	/// <inheritdoc/>
	public void Initialize(IncrementalGeneratorInitializationContext context)
	{
		context.RegisterSourceOutput(
			context.SyntaxProvider
				.ForAttributeWithMetadataName(
					"System.Diagnostics.CodeGen.GeneratedOverriddingMemberAttribute",
					static (node, _) => node is MethodDeclarationSyntax { Modifiers: var modifiers } && modifiers.Any(SyntaxKind.PartialKeyword),
					transformEqualsData
				)
				.Where(static data => data is not null)
				.Collect(),
			outputEquals
		);


		static EqualsData? transformEqualsData(GeneratorAttributeSyntaxContext gasc, CancellationToken ct)
		{
#pragma warning disable format
			if (gasc is not
				{
					Attributes: [{ ConstructorArguments: [{ Value: int rawMode }] }],
					TargetNode: MethodDeclarationSyntax { Modifiers: var modifiers },
					TargetSymbol: IMethodSymbol
					{
						OverriddenMethod: var overridenMethod,
						ContainingType: { } type,
						Name: "Equals",
						IsOverride: true,
						IsStatic: false,
						ReturnType.SpecialType: SpecialType.System_Boolean,
						IsGenericMethod: false,
						Parameters:
						[
							{
								Name: var parameterName,
								Type: { SpecialType: SpecialType.System_Object, NullableAnnotation: NullableAnnotation.Annotated }
							}
						]
					} method
				})
#pragma warning restore format
			{
				return null;
			}

			// Check whether the method is overridden from object.Equals(object?).
			var rootMethod = overridenMethod;
			var currentMethod = method;
			for (; rootMethod is not null; rootMethod = rootMethod.OverriddenMethod, currentMethod = currentMethod!.OverriddenMethod)
			{
			}
			if (currentMethod!.ContainingType.SpecialType is not (SpecialType.System_Object or SpecialType.System_ValueType))
			{
				return null;
			}

#pragma warning disable format
			if ((rawMode, type) switch
				{
					(0, { TypeKind: TypeKind.Struct, IsRefLikeType: true }) => false,
					(1, _) => false,
					(2, { TypeKind: TypeKind.Class }) => false,
					_ => true
				})
#pragma warning restore format
			{
				return null;
			}

			return new(rawMode, modifiers, type, parameterName);
		}

		void outputEquals(SourceProductionContext spc, ImmutableArray<EqualsData?> data)
		{
			foreach (var tuple in data.CastToNotNull())
			{
				if (tuple is not (var mode, var modifiers, { Name: var typeName, ContainingNamespace: var @namespace } type, var paramName))
				{
					continue;
				}

				var extraAttributeStr = mode switch
				{
					0 => """
					[global::System.Obsolete(global::System.Runtime.Messages.RefStructDefaultImplementationMessage.OverriddenEqualsMethod, false, DiagnosticId = "SCA0104", UrlFormat = "https://sunnieshine.github.io/Sudoku/code-analysis/sca0104")]
						
					""",
					_ => string.Empty
				};
				var targetExpression = mode switch
				{
					0 => "false",
					1 => $"{paramName} is {type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)} comparer && Equals(comparer)",
					2 => $"Equals({paramName} as {type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)})"
				};
				var attributeStr = mode switch
				{
					0 => string.Empty,
					1 or 2 => "[global::System.Diagnostics.CodeAnalysis.NotNullWhenAttribute(true)] "
				};

				var namespaceStr = @namespace.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)["global::".Length..];
				spc.AddSource(
					$"{type.ToFileName()}.g.{Shortcuts.GeneratedOverriddenMemberEquals}.cs",
					$$"""
					// <auto-generated/>

					#nullable enable

					namespace {{namespaceStr}};

					partial {{type.GetTypeKindModifier()}} {{typeName}}
					{
						/// <inheritdoc cref="object.Equals(object?)"/>
						[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
						[global::System.CodeDom.Compiler.GeneratedCodeAttribute("{{GetType().FullName}}", "{{VersionValue}}")]
						[global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
						{{extraAttributeStr}}{{modifiers}} bool Equals({{attributeStr}}object? {{paramName}})
							=> {{targetExpression}};
					}
					"""
				);
			}
		}
	}
}

file readonly record struct EqualsData(int GeneratedMode, SyntaxTokenList MethodModifiers, INamedTypeSymbol Type, string ParameterName);
