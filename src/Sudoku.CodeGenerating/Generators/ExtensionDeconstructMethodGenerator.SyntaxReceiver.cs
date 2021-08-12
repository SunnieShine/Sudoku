﻿namespace Sudoku.CodeGenerating.Generators
{
	partial class ExtensionDeconstructMethodGenerator
	{
		/// <summary>
		/// Indicates the inner syntax receiver.
		/// </summary>
		private sealed class SyntaxReceiver : ISyntaxReceiver
		{
			/// <summary>
			/// Indicates the attributes result that targets to a module.
			/// </summary>
			public IList<AttributeSyntax> Attributes { get; } = new List<AttributeSyntax>();


			/// <inheritdoc/>
			public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
			{
				if (
					syntaxNode is not AttributeListSyntax
					{
						Attributes: { Count: not 0 } attributes,
						Target: { Identifier: { ValueText: "assembly" } }
					}
					|| attributes.Any(static attribute => attribute is
					{
						Name: IdentifierNameSyntax
						{
							Identifier: { ValueText: nameof(AutoDeconstructExtensionAttribute) }
						}
					})
				)
				{
					return;
				}

				foreach (var attribute in attributes)
				{
					Attributes.Add(attribute);
				}
			}
		}
	}
}
