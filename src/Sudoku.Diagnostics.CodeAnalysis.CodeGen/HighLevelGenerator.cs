﻿namespace Sudoku.Diagnostics.CodeGen;

/// <summary>
/// Indicates the high-level source generator that generates the source generator.
/// </summary>
[Generator(LanguageNames.CSharp)]
public sealed partial class HighLevelGenerator : ISourceGenerator
{
	/// <inheritdoc/>
	public void Execute(GeneratorExecutionContext context)
	{
		if (
			context is not
			{
				AdditionalFiles: { Length: 1 } additionalFiles,
				SyntaxContextReceiver: Receiver { Diagnostics: var diagnostics, Result: var shortNames }
			}
		)
		{
			return;
		}

		// Get the compiler diagnostics and insert into the analyzer types.
		var descriptors = 
			from detail in MarkdownHandler.SplitTable(File.ReadAllText(additionalFiles[0].Path))
			select detail.ToDescriptor();

		// Report compiler diagnostics.
		diagnostics.ForEach(context.ReportDiagnostic);

		// Append analzyers.
		foreach (var (shortName, fullName, diagnosticIds) in shortNames)
		{
			context.AddSource(
				$"{shortName}Analyzer.g.cs",
				$@"namespace Sudoku.Diagnostics.CodeAnalysis.Analyzers;

[CompilerGenerated]
[Generator(LanguageNames.CSharp)]
public sealed class {shortName}Analyzer : ISourceGenerator
{{
	/// <inheritdoc/>
	[CompilerGenerated]	
	public void Execute(GeneratorExecutionContext context) =>
		(({shortName}SyntaxChecker)context.SyntaxContextReceiver!).Diagnostics.ForEach(context.ReportDiagnostic);

	/// <inheritdoc/>
	[CompilerGenerated]
	public void Initialize(GeneratorInitializationContext context) =>
		context.RegisterForSyntaxNotifications(() => new {shortName}SyntaxChecker(context.CancellationToken));
}}
"
			);

			string descriptorsStr = string.Join(
				"\r\n\r\n\t",
				from descriptor in descriptors
				where Array.IndexOf(diagnosticIds, descriptor.Id) != -1
				select $@"/// <summary>
	/// Indicates the descriptor {descriptor.Id} ({descriptor.Title}).
	/// </summary>
	[CompilerGenerated]
	private static readonly DiagnosticDescriptor {descriptor.Id} = new(
		id: nameof({descriptor.Id}),
		title: ""{descriptor.Title}"",
		messageFormat: ""{descriptor.MessageFormat}"",
		category: ""{descriptor.Category}"",
		defaultSeverity: DiagnosticSeverity.{descriptor.DefaultSeverity},
		isEnabledByDefault: true,
		helpLinkUri: {(descriptor.HelpLinkUri is var s and not "" ? s : "null")}
	);"
			);

			context.AddSource(
				$"{fullName}.g.cs",
				$@"namespace Sudoku.Diagnostics.CodeAnalysis.SyntaxContextReceivers;

[CompilerGenerated]
partial class {fullName}
{{
	{descriptorsStr}


	/// <summary>
	/// Indicates the cancellation token used.
	/// </summary>
	[CompilerGenerated]
	private readonly CancellationToken _cancellationToken;


	/// <summary>
	/// Initializes a <see cref=""{fullName}""/> instance using the cancellation token.
	/// </summary>
	/// <param name=""cancellationToken"">The cancellation token to cancel the operation.</param>
	[CompilerGenerated]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public {fullName}(CancellationToken cancellationToken) => _cancellationToken = cancellationToken;


	/// <summary>
	/// Indicates all possible diagnostics types used.
	/// </summary>
	[CompilerGenerated]
	public List<Diagnostic> Diagnostics {{ get; }} = new();
}}
"
			);
		}
	}

	/// <inheritdoc/>
	public void Initialize(GeneratorInitializationContext context) =>
		context.RegisterForSyntaxNotifications(() => new Receiver(context.CancellationToken));
}
