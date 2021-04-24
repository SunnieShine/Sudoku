﻿#pragma warning disable IDE0057

using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Sudoku.CodeGen.HashCode
{
	partial class AutoGetHashCodeGenerator
	{
		private static partial string PrintHeader() => new StringBuilder()
			.AppendLine("//")
			.AppendLine("// <auto-generated>")
			.AppendLine("//     This file is generated by compiler, powered by source generator.")
			.AppendLine("// </auto-generated>")
			.AppendLine("//")
			.ToString();

		private static partial string PrintOpenBraceToken() => "(";

		private static partial string PrintClosedBraceToken() => ")";

		private static partial string PrintNamespaceKeywordToken() => "namespace ";

		private static partial string PrintPartialKeywordToken() => "partial ";

		private static partial string PrintTypeKeywordToken(bool? isRecord, TypeKind typeKind) => isRecord switch
		{
			true => "record ",
			false => "record struct ",
			_ => typeKind switch
			{
				TypeKind.Class => "class ",
				TypeKind.Struct => "struct ",
				TypeKind.Interface => "interface "
			}
		};

		private static partial string PrintPublicKeywordToken() => "public ";

		private static partial string PrintReadOnlyKeywordToken() => "readonly ";

		private static partial string PrintIntKeywordToken() => "int ";

		private static partial string PrintOverrideKeywordToken() => "override ";

		private static partial string PrintGetHashCode() => "GetHashCode";

		private static partial string PrintReturnKeywordToken() => "return ";

		private static partial string PrintSemicolonToken() => ";";

		private static partial string PrintExclusiveOrOperatorToken() => " ^ ";

		private static partial string PrintThisKeywordToken() => "this ";

		private static partial string PrintDotToken() => ".";

		private static partial string PrintLambdaOperatorToken() => " => ";

		private static partial string PrintOpenBracketToken(int indentingCount) =>
			indentingCount == 0 ? "{" : $"{PrintIndenting(indentingCount)}{{";

		private static partial string PrintClosedBracketToken(int indentingCount) =>
			indentingCount == 0 ? "}" : $"{PrintIndenting(indentingCount)}}}";

		private static partial string PrintPragmaWarningDisableCS1591() => "#pragma warning disable 1591";

		private static partial string PrintUsingDirectives() => "using System.Runtime.CompilerServices;";

		private static partial string PrintNullableEnable() => "#nullable enable";

		private static partial string PrintIndenting(int indentingCount)
		{
			var indenting = new StringBuilder();
			for (int i = 0; i < indentingCount; i++)
			{
				indenting.Append(UsingTabsAsIndentingCharacters ? "\t" : "    ");
			}

			return indenting.ToString();
		}

		private static partial string PrintCompilerGenerated(int indentingCount)
		{
			const int o = 9;
			const string name = nameof(CompilerGeneratedAttribute);

			return $"{PrintIndenting(indentingCount)}[{name.Substring(0, name.Length - o)}]";
		}
	}
}
