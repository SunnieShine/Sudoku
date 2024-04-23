global using System;
global using System.Collections;
global using System.Collections.Frozen;
global using System.Collections.Generic;
global using System.ComponentModel;
global using System.Diagnostics;
global using System.Diagnostics.CodeAnalysis;
global using System.Globalization;
global using System.IO;
global using System.Linq;
global using System.Numerics;
global using System.Reflection;
global using System.Resources;
global using System.Runtime.CompilerServices;
global using System.Runtime.InteropServices;
global using System.Runtime.Intrinsics;
global using System.Runtime.Versioning;
global using System.SourceGeneration;
global using System.Text;
global using System.Text.Json;
global using System.Text.Json.Serialization;
global using System.Text.RegularExpressions;
global using System.Threading;
global using System.Threading.Tasks;
global using Sudoku.Analytics;
global using Sudoku.Concepts;
global using Sudoku.Linq;
global using Sudoku.Rendering;
global using Sudoku.Rendering.Nodes;
global using Sudoku.Resources;
global using Sudoku.Runtime.CompilerServices;
global using Sudoku.Runtime.MaskServices;
global using Sudoku.Solving;
global using Sudoku.Text;
global using Sudoku.Text.Converters;
global using Sudoku.Text.Parsers;
global using Sudoku.Text.Serialization.Specialized;
global using static System.Combinatorial;
global using static System.Numerics.BitOperations;
global using static Sudoku.Concepts.ConclusionType;
global using static Sudoku.SolutionFields;
global using CandidateMapPredicate = Sudoku.Concepts.BitStatusMapPredicate<Sudoku.Concepts.CandidateMap, int /*Candidate*/, Sudoku.Concepts.CandidateMap.Enumerator>;
global using CellMapPredicate = Sudoku.Concepts.BitStatusMapPredicate<Sudoku.Concepts.CellMap, int /*Cell*/, Sudoku.Concepts.CellMap.Enumerator>;
