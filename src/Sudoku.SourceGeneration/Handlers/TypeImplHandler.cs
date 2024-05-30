namespace Sudoku.SourceGeneration.Handlers;

internal static class TypeImplHandler
{
	private const string IsLargeStructurePropertyName = "IsLargeStructure";

	private const string OtherModifiersOnEqualsPropertyName = "OtherModifiersOnEquals";

	private const string EqualsBehaviorPropertyName = "EqualsBehavior";

	private const string GetHashCodeBehaviorPropertyName = "GetHashCodeBehavior";


	public static List<string>? Transform(GeneratorAttributeSyntaxContext gasc, CancellationToken cancellationToken)
	{
		var typeSources = new List<string>();
		if (Object_Equals(gasc) is { } source1)
		{
			typeSources.Add(source1);
		}
		if (Object_GetHashCode(gasc, cancellationToken) is { } source2)
		{
			typeSources.Add(source2);
		}
		return typeSources;
	}

	public static void Output(SourceProductionContext spc, ImmutableArray<List<string>> value)
		=> spc.AddSource(
			"TypeImpl.g.cs",
			$"""
			{Banner.AutoGenerated}

			#nullable enable
			
			{string.Join("\r\n\r\n", from element in value from nested in element select nested)}
			"""
		);

	private static string? Object_Equals(GeneratorAttributeSyntaxContext gasc)
	{
		if (gasc is not
			{
				Attributes: [{ ConstructorArguments: [{ Value: int ctorArg }] } attribute],
				TargetSymbol: INamedTypeSymbol
				{
					TypeKind: var kind and (TypeKind.Struct or TypeKind.Class),
					Name: var typeName,
					IsRecord: false, // Records cannot manually overrides 'Equals' method.
					IsReadOnly: var isReadOnly,
					IsRefLikeType: var isRefStruct,
					TypeParameters: var typeParameters,
					ContainingNamespace: var @namespace,
					ContainingType: null // Must be top-level type.
				} type,
				SemanticModel.Compilation: var compilation
			})
		{
			return null;
		}

		if (!((TypeImplFlag)ctorArg).HasFlag(TypeImplFlag.Object_Equals))
		{
			return null;
		}

		var isLargeStructure = attribute.GetNamedArgument(IsLargeStructurePropertyName, false);
		var namespaceString = @namespace.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)["global::".Length..];
		var behavior = attribute.GetNamedArgument(EqualsBehaviorPropertyName, 0) switch
		{
			0 => (isRefStruct, kind) switch
			{
				(true, _) => EqualsBehavior.ReturnFalse,
				(_, TypeKind.Struct) => EqualsBehavior.IsCast,
				(_, TypeKind.Class) => EqualsBehavior.AsCast,
				_ => throw new InvalidOperationException("Invalid state.")
			},
			1 => EqualsBehavior.Throw,
			2 => EqualsBehavior.MakeAbstract,
			_ => throw new InvalidOperationException("Invalid state.")
		};
		var otherModifiers = attribute.GetNamedArgument<string>(OtherModifiersOnEqualsPropertyName) switch
		{
			{ } str => str.Split([' '], StringSplitOptions.RemoveEmptyEntries),
			_ => []
		};
		var typeArgumentsString = typeParameters is []
			? string.Empty
			: $"<{string.Join(", ", from typeParameter in typeParameters select typeParameter.Name)}>";
		var typeNameString = $"{typeName}{typeArgumentsString}";
		var fullTypeNameString = $"global::{namespaceString}.{typeNameString}";
		var typeKindString = kind switch
		{
			TypeKind.Class => "class",
			TypeKind.Struct => "struct",
			_ => throw new InvalidOperationException("Invalid state.")
		};
		var otherModifiersString = otherModifiers.Length == 0 ? string.Empty : $"{string.Join(" ", otherModifiers)} ";
		if (behavior == EqualsBehavior.MakeAbstract)
		{
			return $$"""
				namespace {{namespaceString}}
				{
					partial {{typeKindString}} {{typeNameString}}
					{
						/// <inheritdoc cref="object.Equals(object?)"/>
						public {{otherModifiersString}}abstract override bool Equals([global::System.Diagnostics.CodeAnalysis.NotNullWhenAttribute(true)] object? obj);
					}
				}
				""";
		}
		else
		{
			var inKeyword = isLargeStructure ? "in " : string.Empty;
			var expressionString = behavior switch
			{
				EqualsBehavior.ReturnFalse => "false",
				EqualsBehavior.IsCast => $"obj is {fullTypeNameString} comparer && Equals({inKeyword}comparer)",
				EqualsBehavior.AsCast => $"Equals(obj as {fullTypeNameString})",
				EqualsBehavior.Throw => """throw new global::System.NotSupportedException("This method is not supported or disallowed by author.")""",
				_ => throw new InvalidOperationException("Invalid state.")
			};
			var attributesMarked = isRefStruct
				? behavior == EqualsBehavior.ReturnFalse
					? """
					[global::System.ObsoleteAttribute("Calling this method is unexpected because author disallow you call this method on purpose.", true)]
					"""
					: """
					[global::System.Diagnostics.CodeAnalysis.DoesNotReturnAttribute]
							[global::System.ObsoleteAttribute("Calling this method is unexpected because author disallow you call this method on purpose.", true)]
					"""
				: """
				[global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				""";
			var readOnlyModifier = kind == TypeKind.Struct && !isReadOnly ? "readonly " : string.Empty;
			var isDeprecated = attributesMarked.Contains("ObsoleteAttribute");
			var suppress0809 = isDeprecated ? "#pragma warning disable CS0809\r\n\t" : "\t";
			var enable0809 = isDeprecated ? "#pragma warning restore CS0809\r\n\t" : string.Empty;
			return $$"""
				namespace {{namespaceString}}
				{
				{{suppress0809}}partial {{typeKindString}} {{typeNameString}}
					{
						/// <inheritdoc cref="object.Equals(object?)"/>
						{{attributesMarked}}
						[global::System.CodeDom.Compiler.GeneratedCodeAttribute("{{typeof(TypeImplHandler).FullName}}", "{{Value}}")]
						[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
						public {{otherModifiersString}}override {{readOnlyModifier}}bool Equals([global::System.Diagnostics.CodeAnalysis.NotNullWhenAttribute(true)] object? obj)
							=> {{expressionString}};
					}
				{{enable0809}}}
				""";
		}
	}

	private static string? Object_GetHashCode(GeneratorAttributeSyntaxContext gasc, CancellationToken cancellationToken)
	{
		if (gasc is not
			{
				Attributes: [{ ConstructorArguments: [{ Value: int ctorArg }] } attribute],
				TargetSymbol: INamedTypeSymbol
				{
					Name: var typeName,
					ContainingNamespace: var @namespace,
					TypeParameters: var typeParameters,
					TypeKind: var kind and (TypeKind.Class or TypeKind.Struct),
					IsRecord: var isRecord,
					IsReadOnly: var isReadOnly,
					IsRefLikeType: var isRefStruct,
					ContainingType: null
				} type,
				TargetNode: TypeDeclarationSyntax { ParameterList: var parameterList }
					and (RecordDeclarationSyntax or ClassDeclarationSyntax or StructDeclarationSyntax),
				SemanticModel: { Compilation: var compilation } semanticModel
			})
		{
			return null;
		}

		if (!((TypeImplFlag)ctorArg).HasFlag(TypeImplFlag.Object_GetHashCode))
		{
			return null;
		}

		var namespaceString = @namespace.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)["global::".Length..];
		var typeParametersString = typeParameters is []
			? string.Empty
			: $"<{string.Join(", ", from typeParameter in typeParameters select typeParameter.Name)}>";
		var typeNameString = $"{typeName}{typeParametersString}";

		const string dataMemberAttributeTypeName = "System.SourceGeneration.PrimaryConstructorParameterAttribute";
		var dataMemberAttributeTypeNameSymbol = compilation.GetTypeByMetadataName(dataMemberAttributeTypeName);
		if (dataMemberAttributeTypeNameSymbol is null)
		{
			return null;
		}

		const string hashCodeMemberAttributeTypeName = "System.SourceGeneration.HashCodeMemberAttribute";
		var hashCodeMemberAttributeSymbol = compilation.GetTypeByMetadataName(hashCodeMemberAttributeTypeName);
		if (hashCodeMemberAttributeSymbol is null)
		{
			return null;
		}

		var referencedMembers = PrimaryConstructor.GetCorrespondingMemberNames(
			type,
			semanticModel,
			parameterList,
			dataMemberAttributeTypeNameSymbol,
			a => SymbolEqualityComparer.Default.Equals(a.AttributeClass, hashCodeMemberAttributeSymbol),
			static symbol => symbol switch
			{
				IFieldSymbol { Type.SpecialType: System_Byte or System_SByte or System_Int16 or System_UInt16 or System_Int32 } => true,
				IFieldSymbol { Type.TypeKind: TypeKind.Enum } => false,
				IPropertySymbol { Type.SpecialType: System_Byte or System_SByte or System_Int16 or System_UInt16 or System_Int32 } => true,
				IPropertySymbol { Type.TypeKind: TypeKind.Enum } => false,
				IParameterSymbol { Type.SpecialType: System_Byte or System_SByte or System_Int16 or System_UInt16 or System_Int32 } => true,
				IParameterSymbol { Type.TypeKind: TypeKind.Enum } => false,
				_ => default(bool?)
			},
			cancellationToken
		);

		var behavior = (isRefStruct, attribute) switch
		{
			(true, _) => GetHashCodeBehavior.Throw,
			_ => attribute.GetNamedArgument<int>(GetHashCodeBehaviorPropertyName) switch
			{
				0 => referencedMembers switch
				{
				[] => GetHashCodeBehavior.ReturnNegativeOne,
				[(_, true)] => GetHashCodeBehavior.Direct,
				[(_, false)] => GetHashCodeBehavior.EnumExplicitCast,
					{ Length: > 8 } => GetHashCodeBehavior.HashCodeAdd,
					_ => GetHashCodeBehavior.Specified
				},
				1 => GetHashCodeBehavior.Throw,
				2 => GetHashCodeBehavior.MakeAbstract,
				_ => throw new InvalidOperationException("Invalid state.")
			}
		};
		var kindString = (isRecord, kind) switch
		{
			(true, TypeKind.Class) => "record",
			(true, TypeKind.Struct) => "record struct",
			(_, TypeKind.Class) => "class",
			(_, TypeKind.Struct) => "struct",
			_ => throw new InvalidOperationException("Invalid state.")
		};
		var otherModifiers = attribute.GetNamedArgument<string>("OtherModifiersOnGetHashCode") switch
		{
			{ } str => str.Split([' '], StringSplitOptions.RemoveEmptyEntries),
			_ => []
		};
		var otherModifiersString = otherModifiers.Length == 0 ? string.Empty : $"{string.Join(" ", otherModifiers)} ";
		if (behavior == GetHashCodeBehavior.MakeAbstract)
		{
			return $$"""
				namespace {{namespaceString}}
				{
					partial {{kindString}} {{typeNameString}}
					{
						/// <inheritdoc cref="object.GetHashCode"/>
						public {{otherModifiersString}}abstract override int GetHashCode();
					}
				}
				""";
		}
		else
		{
			var codeBlock = behavior switch
			{
				GetHashCodeBehavior.ReturnNegativeOne => @"	=> -1;",
				GetHashCodeBehavior.Direct => $@"	=> {referencedMembers[0].Name};",
				GetHashCodeBehavior.EnumExplicitCast => $@"	=> (int){referencedMembers[0].Name};",
				GetHashCodeBehavior.Specified
					=> $@"	=> global::System.HashCode.Combine({string.Join(", ", from pair in referencedMembers select pair.Name)});",
				GetHashCodeBehavior.Throw
					=> @"	=> throw new global::System.NotSupportedException(""This method is not supported or disallowed by author."");",
				GetHashCodeBehavior.HashCodeAdd
					=> $$"""
						{
							var hashCode = new global::System.HashCode();
							{{string.Join("\r\n\r\n", from member in referencedMembers select $"hashCode.Add({member.Name});")}}
							return hashCode.ToHashCode();
						}
					""",
				_ => throw new InvalidOperationException("Invalid state.")
			};
			var attributesMarked = (isRefStruct, behavior) switch
			{
				(true, not GetHashCodeBehavior.ReturnNegativeOne) or (_, GetHashCodeBehavior.Throw)
					=> """
					[global::System.Diagnostics.CodeAnalysis.DoesNotReturnAttribute]
							[global::System.ObsoleteAttribute("Calling this method is unexpected because author disallow you call this method on purpose.", true)]
					""",
				(true, _)
					=> """
					[global::System.ObsoleteAttribute("Calling this method is unexpected because author disallow you call this method on purpose.", true)]
					""",
				_
					=> """
					[global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
					"""
			};
			var readOnlyModifier = kind == TypeKind.Struct && !isReadOnly ? "readonly " : string.Empty;
			var isDeprecated = attributesMarked.Contains("ObsoleteAttribute");
			var suppress0809 = isDeprecated
				? "#pragma warning disable CS0809\r\n\t"
				: "\t";
			var enable0809 = isDeprecated
				? "#pragma warning restore CS0809\r\n\t"
				: "\t";
			return $$"""
				namespace {{namespaceString}}
				{
				{{suppress0809}}partial {{kindString}} {{typeNameString}}
					{
						/// <inheritdoc cref="object.GetHashCode"/>
						{{attributesMarked}}
						[global::System.CodeDom.Compiler.GeneratedCodeAttribute("{{typeof(TypeImplHandler).FullName}}", "{{Value}}")]
						[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
						public {{otherModifiersString}}override {{readOnlyModifier}}int GetHashCode()
						{{codeBlock}}
				{{enable0809}}}
				}
				""";
		}
	}
}

/// <summary>
/// Indicates the implementation flags.
/// </summary>
[Flags]
file enum TypeImplFlag
{
	Object_Equals = 1 << 0,
	Object_GetHashCode = 1 << 1,
	Object_ToString = 1 << 2,
	EqualityOperators = 1 << 3,
	ComparisonOperators = 1 << 4
}

/// <summary>
/// Represents a behavior for generating <see cref="object.Equals(object)"/> method.
/// </summary>
/// <seealso cref="object.Equals(object)"/>
file enum EqualsBehavior
{
	ReturnFalse,
	IsCast,
	AsCast,
	Throw,
	MakeAbstract
}

/// <summary>
/// Represents a behavior for generating <see cref="object.GetHashCode"/> method.
/// </summary>
/// <seealso cref="object.GetHashCode"/>
file enum GetHashCodeBehavior
{
	ReturnNegativeOne,
	Direct,
	EnumExplicitCast,
	Specified,
	Throw,
	HashCodeAdd,
	MakeAbstract
}
