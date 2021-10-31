﻿namespace Sudoku.Diagnostics.CodeGen.Generators;

/// <summary>
/// Defines the global configuration value source generator.
/// </summary>
[Generator(LanguageNames.CSharp)]
public sealed class GlobalConfigValueGenerator : ISourceGenerator
{
	/// <inheritdoc/>
	public void Execute(GeneratorExecutionContext context)
	{
		if (context.AdditionalFiles is not { IsDefaultOrEmpty: false } additionalFiles)
		{
			return;
		}

		var additionalFile = additionalFiles.FirstOrDefault(static a => a.Path.EndsWith("Directory.Build.props"));
		if (additionalFile is not { Path: var path })
		{
			return;
		}

		var xmlDocument = new XmlDocument();
		xmlDocument.Load(path);
		if (xmlDocument is not { DocumentElement: { } root })
		{
			return;
		}

		if (root.SelectNodes("descendant::PropertyGroup") is not { } propertyGroupList)
		{
			return;
		}

		if (propertyGroupList.Cast<XmlNode>().FirstOrDefault() is not { ChildNodes: { Count: not 0 } elements })
		{
			return;
		}

		Version? versionResult = null;
		bool found = false;
		foreach (XmlNode element in elements)
		{
			if (element is { Name: "Version", InnerText: var v } && Version.TryParse(v, out versionResult))
			{
				found = true;
				break;
			}
		}
		if (!found)
		{
			return;
		}

		context.AddSource(
			"Constants.Version.g.cs",
			$@"namespace Sudoku.Diagnostics.CodeGen;

partial class Constants
{{
	/// <summary>
	/// Indictaes the version of this project.
	/// </summary>
	public const string VersionValue = ""{versionResult!}"";
}}"
		);
	}

	/// <inheritdoc/>
	public void Initialize(GeneratorInitializationContext context)
	{
	}
}
