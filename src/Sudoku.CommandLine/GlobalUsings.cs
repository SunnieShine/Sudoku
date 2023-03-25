global using System;
global using System.Collections.Generic;
global using System.CommandLine;
global using System.CommandLine.Annotations;
global using System.Linq;
global using System.Numerics;
global using System.Reactive.Linq;
global using System.Reflection;
global using System.Runtime.CompilerServices;
global using System.Runtime.Versioning;
global using System.Text;
global using System.Text.RegularExpressions;
global using System.Threading;
global using System.Threading.Tasks;
global using Flurl.Http;
global using Mirai.Net.Data.Events.Concretes.Group;
global using Mirai.Net.Data.Events.Concretes.Request;
global using Mirai.Net.Data.Exceptions;
global using Mirai.Net.Data.Messages.Receivers;
global using Mirai.Net.Sessions;
global using Mirai.Net.Sessions.Http.Managers;
global using Mirai.Net.Utils.Scaffolds;
global using Sudoku.Algorithms.Generating;
global using Sudoku.Algorithms.Solving;
global using Sudoku.Analytics;
global using Sudoku.CommandLine;
global using Sudoku.CommandLine.RootCommands;
global using Sudoku.CommandLine.Strings;
global using Sudoku.CommandLine.ValueConverters;
global using Sudoku.Concepts;
global using Sudoku.Drawing;
global using Sudoku.Solving.Logical;
global using Sudoku.Text.Parsing;
global using Sudoku.Workflow.Bot.Oicq.Collections;
global using Sudoku.Workflow.Bot.Oicq.Lifecycle;
global using static System.Math;
global using static System.Runtime.CompilerServices.Unsafe;
global using static Sudoku.CommandLine.CommonConstants;
global using static Sudoku.Resources.MergedResources;
global using static Sudoku.Workflow.Bot.Oicq.Constants;
global using static Sudoku.Workflow.Bot.Oicq.Lifecycle.EnvironmentVariables;
