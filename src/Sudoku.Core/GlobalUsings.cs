﻿global using System;
global using System.Algorithm;
global using System.Buffers;
global using System.Collections;
global using System.Collections.Generic;
global using System.Collections.Immutable;
global using System.ComponentModel;
global using System.Diagnostics;
global using System.Diagnostics.CodeAnalysis;
global using System.Diagnostics.CodeGen;
global using System.Globalization;
global using System.IO;
global using System.Linq;
global using System.Numerics;
global using System.Reflection;
global using System.Runtime.CompilerServices;
global using System.Runtime.InteropServices;
global using System.Text;
global using System.Text.Json;
global using System.Text.Json.Serialization;
global using System.Text.RegularExpressions;
global using System.Threading;
global using System.Threading.Tasks;
global using System.Timers;
global using Sudoku.Buffers;
global using Sudoku.Compatibility.Hodoku;
global using Sudoku.Compatibility.SudokuExplainer;
global using Sudoku.Concepts;
global using Sudoku.Enumerating;
global using Sudoku.Linq;
global using Sudoku.Presentation;
global using Sudoku.Presentation.Nodes;
global using Sudoku.Resources;
global using Sudoku.Runtime.AnalysisServices;
global using Sudoku.Runtime.AnalysisServices.Configuration;
global using Sudoku.Runtime.DisplayingServices;
global using Sudoku.Solving;
global using Sudoku.Solving.Algorithms.Backtracking;
global using Sudoku.Solving.Algorithms.Bitwise;
global using Sudoku.Solving.Algorithms.EnumerableQuery;
global using Sudoku.Solving.Logics;
global using Sudoku.Solving.Logics.Implementations.Data;
global using Sudoku.Solving.Logics.Implementations.Searchers;
global using Sudoku.Solving.Logics.Implementations.Steps;
global using Sudoku.Solving.Logics.Prototypes;
global using Sudoku.Techniques;
global using Sudoku.Techniques.Patterns;
global using Sudoku.Text;
global using Sudoku.Text.Formatting;
global using Sudoku.Text.Notations;
global using Sudoku.Text.Parsing;
global using Sudoku.Text.Serialization.Specialized;
global using static System.Algorithm.Sequences;
global using static System.Algorithm.Sorting;
global using static System.Math;
global using static System.Numerics.BitOperations;
global using static System.Text.Json.JsonSerializer;
global using static Sudoku.Buffers.FastProperties;
global using static Sudoku.Resources.MergedResources;
global using static Sudoku.Runtime.AnalysisServices.CommonReadOnlies;
global using static Sudoku.Runtime.MaskServices.MaskOperations;
global using static Sudoku.Solving.ConclusionType;
global using ViewList = System.Collections.Immutable.ImmutableArray<Sudoku.Presentation.View>;
global using ConclusionList = System.Collections.Immutable.ImmutableArray<Sudoku.Solving.Conclusion>;
