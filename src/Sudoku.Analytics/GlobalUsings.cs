global using System;
global using System.Collections;
global using System.Collections.Frozen;
global using System.Collections.Generic;
global using System.ComponentModel;
global using System.Diagnostics;
global using System.Diagnostics.CodeAnalysis;
global using System.Globalization;
global using System.Linq;
global using System.Numerics;
global using System.Reflection;
global using System.Runtime.CompilerServices;
global using System.Runtime.InteropServices;
global using System.Runtime.Versioning;
global using System.SourceGeneration;
global using System.Text;
global using System.Text.Json;
global using System.Text.Json.Serialization;
global using System.Text.RegularExpressions;
global using System.Threading;
global using System.Timers;
global using Expressive;
global using Expressive.Exceptions;
global using Expressive.Expressions;
global using Expressive.Expressions.Binary;
global using Expressive.Operators;
global using Sudoku.Algorithm.Ittoryu;
global using Sudoku.Analytics;
global using Sudoku.Analytics.Categorization;
global using Sudoku.Analytics.Configuration;
global using Sudoku.Analytics.Metadata;
global using Sudoku.Analytics.Rating;
global using Sudoku.Analytics.Steps;
global using Sudoku.Analytics.StepSearcherModules;
global using Sudoku.Analytics.StepSearchers;
global using Sudoku.Compatibility.Hodoku;
global using Sudoku.Compatibility.SudokuExplainer;
global using Sudoku.ComponentModel;
global using Sudoku.Concepts;
global using Sudoku.Concepts.ObjectModel;
global using Sudoku.Filtering.Bottleneck;
global using Sudoku.Filtering.Expressions;
global using Sudoku.Filtering.Operators;
global using Sudoku.Linq;
global using Sudoku.Rendering;
global using Sudoku.Rendering.Nodes;
global using Sudoku.Resources;
global using Sudoku.Runtime.CompilerServices;
global using Sudoku.Runtime.MaskServices;
global using Sudoku.Text;
global using Sudoku.Text.Converters;
global using Sudoku.Text.Parsers;
global using static System.Algorithm.Sequences;
global using static System.Numerics.BitOperations;
global using static Sudoku.Analytics.CachedFields;
global using static Sudoku.Analytics.ConclusionType;
global using static Sudoku.Concepts.Intersection;
global using static Sudoku.Rendering.RenderingMode;
global using static Sudoku.SolutionWideReadOnlyFields;
global using static Sudoku.Text.Languages;
global using LockedMember = (Sudoku.Concepts.CellMap LockedCells, int /*House*/ LockedBlock);
global using TargetCellsGroup = Sudoku.Linq.BitStatusMapGroup<Sudoku.Concepts.CellMap, int /*Cell*/, int /*House*/>;
global using SolvingPathElement = (Sudoku.Concepts.Grid SteppingGrid, Sudoku.Analytics.Step Step);
global using unsafe SubsetModuleSearcherFunc = delegate*<ref Sudoku.Analytics.AnalysisContext, ref readonly Sudoku.Concepts.Grid, int, bool, Sudoku.Analytics.Step?>;
global using unsafe SymmetricalPlacementCheckerFunc = delegate*<ref readonly Sudoku.Concepts.Grid, out Sudoku.Concepts.SymmetricType, out System.ReadOnlySpan<int /*Digit*/?>, out short /*Mask*/, bool>;
global using unsafe CollectorPredicateFunc = delegate*<ref readonly Sudoku.Concepts.CellMap, bool>;
global using unsafe SingleModuleSearcherFunc = delegate*<Sudoku.Analytics.StepSearchers.SingleStepSearcher, ref Sudoku.Analytics.AnalysisContext, ref readonly Sudoku.Concepts.Grid, Sudoku.Analytics.Step?>;
global using unsafe AnitGurthSymmetricalPlacementModuleSearcherFunc = delegate*<ref readonly Sudoku.Concepts.Grid, ref Sudoku.Analytics.AnalysisContext, Sudoku.Analytics.Steps.AntiGurthSymmetricalPlacementStep?>;
