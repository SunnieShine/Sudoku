﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Sudoku.CodeAnalysis.Extensions;
using Quadruple = System.ValueTuple<
	bool,
	Microsoft.CodeAnalysis.CSharp.Syntax.ClassDeclarationSyntax,
	Microsoft.CodeAnalysis.CSharp.Syntax.PropertyDeclarationSyntax?,
	Microsoft.CodeAnalysis.IPropertySymbol?
>;

namespace Sudoku.CodeAnalysis.Analyzers
{
	partial class PropertiesInStepSearcherAnalyzer
	{
		/// <summary>
		/// Encapsulates a target property searcher.
		/// </summary>
		private sealed class InnerWalker : CSharpSyntaxWalker
		{
			/// <summary>
			/// Indicates the target property name to check (i.e. <c>Properties</c>).
			/// </summary>
			private const string TargetPropertyName = "Properties";

			/// <summary>
			/// Indicates the full name of the type.
			/// </summary>
			private const string StepSearcherTypeFullName = "Sudoku.Solving.Manual.StepSearcher";


			/// <summary>
			/// Indicates the compilation.
			/// </summary>
			private readonly Compilation _compilation;

			/// <summary>
			/// Indicates the semantic model of this syntax tree.
			/// </summary>
			private readonly SemanticModel _semanticModel;


			/// <summary>
			/// Initializes an instance with the specified compilation and the semantic model.
			/// </summary>
			/// <param name="compilation">The compilation.</param>
			/// <param name="semanticModel">The semantic model.</param>
			public InnerWalker(Compilation compilation, SemanticModel semanticModel)
			{
				_compilation = compilation;
				_semanticModel = semanticModel;
			}


			/// <summary>
			/// Indicates the result collection.
			/// </summary>
			public IList<Quadruple>? TargetPropertyInfo { get; private set; }


			/// <inheritdoc/>
			public override void VisitClassDeclaration(ClassDeclarationSyntax node)
			{
				switch (_semanticModel.GetDeclaredSymbol(node))
				{
					case null:
					case { IsAbstract: true }:
					case { IsAnonymousType: true }:
					case { IsStatic: true }:
					{
						return;
					}
					case var classSymbol when classSymbol.DerivedFrom(_compilation, StepSearcherTypeFullName):
					{
						var propertySymbols = classSymbol.GetMembers().OfType<IPropertySymbol>();
						if (!propertySymbols.Any())
						{
							TargetPropertyInfo ??= new List<Quadruple>();

							if
							(
								!TargetPropertyInfo.Any(
									quad =>
									{
										var symbol = _semanticModel.GetDeclaredSymbol(quad.Item2);
										return SymbolEqualityComparer.Default.Equals(symbol, classSymbol);
									}
								)
							)
							{
								// If the class isn't in this list, add it.
								TargetPropertyInfo.Add((false, node, null, null));
							}
						}
						else
						{
							foreach (var propertySymbol in propertySymbols)
							{
								if (propertySymbol is not { IsIndexer: true, Name: not TargetPropertyName })
								{
									TargetPropertyInfo ??= new List<Quadruple>();

									TargetPropertyInfo.Add(
										(
											true,
											node,
											propertySymbol.FindMatchingNode(node, _semanticModel),
											propertySymbol
										)
									);
								}
							}
						}

						break;
					}
				}
			}
		}
	}
}
