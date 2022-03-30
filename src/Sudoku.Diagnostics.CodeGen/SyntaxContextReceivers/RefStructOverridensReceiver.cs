﻿namespace Sudoku.Diagnostics.CodeGen.SyntaxContextReceivers;

/// <summary>
/// Defines a syntax context receiver that provides the gathered node for the usages on the source generator
/// <see cref="RefStructOverridensGenerator"/>.
/// </summary>
/// <param name="CancellationToken">The cancellation token to cancel the operation.</param>
/// <seealso cref="RefStructOverridensGenerator"/>
internal sealed record RefStructOverridensReceiver(CancellationToken CancellationToken) :
	IResultCollectionReceiver<INamedTypeSymbol>
{
	private static readonly DiagnosticDescriptor SCA0001 = new(
		id: nameof(SCA0001),
		title: "Ref structs requires the keyword 'partial'",
		messageFormat: "Ref structs requires the keyword 'partial'",
		category: "SourceGen",
		defaultSeverity: DiagnosticSeverity.Error,
		isEnabledByDefault: true,
		helpLinkUri: null
	);


	/// <inheritdoc/>
	public ICollection<INamedTypeSymbol> Collection { get; } = new List<INamedTypeSymbol>();

	/// <summary>
	/// Indicates the diagnostic results found.
	/// </summary>
	internal List<Diagnostic> Diagnostics { get; } = new();


	/// <inheritdoc/>
	public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
	{
		if (
			context is not
			{
				Node: TypeDeclarationSyntax
				{
					Identifier: var identifier,
					Modifiers: { Count: not 0 } modifiers
				} n,
				SemanticModel: { Compilation: { } compilation } semanticModel
			}
		)
		{
			return;
		}

		if (!modifiers.Any(SyntaxKind.RefKeyword))
		{
			return;
		}

		var symbol = semanticModel.GetDeclaredSymbol(n, CancellationToken);
		if (symbol is not { TypeKind: TypeKind.Struct } typeSymbol)
		{
			return;
		}

		if (!modifiers.Any(SyntaxKind.PartialKeyword))
		{
			Diagnostics.Add(Diagnostic.Create(SCA0001, identifier.GetLocation(), messageArgs: null));
			return;
		}

		Collection.Add(typeSymbol);
	}
}
