global using System;
global using System.Algorithm;
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
global using System.Resources;
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
global using Sudoku.Analytics.Inferring;
global using Sudoku.Analytics.Metadata;
global using Sudoku.Analytics.Rating;
global using Sudoku.Analytics.Steps;
global using Sudoku.Analytics.StepSearcherModules;
global using Sudoku.Analytics.StepSearchers;
global using Sudoku.Analytics.Traits;
global using Sudoku.Compatibility.Hodoku;
global using Sudoku.Compatibility.SudokuExplainer;
global using Sudoku.Concepts;
global using Sudoku.Concepts.ObjectModel;
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
global using static Sudoku.SolutionFields;
global using static Sudoku.Text.Languages;
global using unsafe AnitGurthSymmetricalPlacementModuleSearcherFuncPtr = delegate*<ref readonly Sudoku.Concepts.Grid, ref Sudoku.Analytics.AnalysisContext, Sudoku.Analytics.Steps.AntiGurthSymmetricalPlacementStep?>;
global using unsafe CollectorPredicateFuncPtr = delegate*<ref readonly Sudoku.Concepts.CellMap, bool>;
global using IrregularWingStrongLinkEntry = System.Collections.Generic.Dictionary<(int /*House*/ House, int /*Digit*/ Digit), System.Collections.Generic.List<Sudoku.Concepts.StrongLinkInfo>>;
global using unsafe SingleModuleSearcherFuncPtr = delegate*<Sudoku.Analytics.StepSearchers.SingleStepSearcher, ref Sudoku.Analytics.AnalysisContext, ref readonly Sudoku.Concepts.Grid, Sudoku.Analytics.Step?>;
global using unsafe StepRatingEvaluatorFuncPtr = delegate*<Sudoku.Analytics.Step[], delegate*<Sudoku.Analytics.Step, decimal>, decimal>;
global using unsafe SubsetModuleSearcherFuncPtr = delegate*<ref Sudoku.Analytics.AnalysisContext, ref readonly Sudoku.Concepts.Grid, int, bool, Sudoku.Analytics.Step?>;
global using unsafe SymmetricalPlacementCheckerFuncPtr = delegate*<ref readonly Sudoku.Concepts.Grid, out Sudoku.Concepts.SymmetricType, out System.ReadOnlySpan<int /*Digit*/?>, out short /*Mask*/, bool>;
global using TargetCandidatesGroup = Sudoku.Linq.BitStatusMapGroup<Sudoku.Concepts.CandidateMap, int /*Candidate*/, Sudoku.Concepts.CandidateMap.Enumerator, int /*Cell*/>;
global using TargetCellsGroup = Sudoku.Linq.BitStatusMapGroup<Sudoku.Concepts.CellMap, int /*Cell*/, Sudoku.Concepts.CellMap.Enumerator, int /*House*/>;
