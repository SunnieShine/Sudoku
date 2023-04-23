global using System;
global using System.Collections;
global using System.Collections.Generic;
global using System.Collections.Immutable;
global using System.Diagnostics.CodeAnalysis;
global using System.Diagnostics.CodeGen;
global using System.Numerics;
global using System.Runtime.CompilerServices;
global using System.Text.Json.Serialization;
global using Sudoku.Analytics;
global using Sudoku.Concepts;
global using Sudoku.Text.Notations;
global using Sudoku.Rendering.Nodes;
global using Sudoku.Rendering.Nodes.Grouped;
global using Sudoku.Rendering.Nodes.Shapes;
global using static Sudoku.SolutionWideReadOnlyFields;
global using Candidate = int;
global using Cell = int;
global using House = int;
global using Mask = short;
