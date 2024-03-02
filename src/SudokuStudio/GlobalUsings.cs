global using System;
global using System.Collections;
global using System.Collections.Generic;
global using System.Collections.ObjectModel;
global using System.Collections.Specialized;
global using System.ComponentModel;
global using System.Diagnostics;
global using System.Diagnostics.CodeAnalysis;
global using System.Globalization;
global using System.IO;
global using System.Linq;
global using System.Numerics;
global using System.Reflection;
global using System.Runtime.CompilerServices;
global using System.Runtime.InteropServices;
global using System.Runtime.InteropServices.WindowsRuntime;
global using System.Runtime.Versioning;
global using System.SourceGeneration;
global using System.Text.Json;
global using System.Text.Json.Serialization;
global using System.Threading;
global using System.Threading.Tasks;
global using CommunityToolkit.WinUI;
global using CommunityToolkit.WinUI.Controls;
global using Expressive.Exceptions;
global using Microsoft.Graphics.Canvas.Text;
global using Microsoft.UI;
global using Microsoft.UI.Composition;
global using Microsoft.UI.Composition.SystemBackdrops;
global using Microsoft.UI.Input;
global using Microsoft.UI.Text;
global using Microsoft.UI.Windowing;
global using Microsoft.UI.Xaml;
global using Microsoft.UI.Xaml.Controls;
global using Microsoft.UI.Xaml.Controls.Primitives;
global using Microsoft.UI.Xaml.Data;
global using Microsoft.UI.Xaml.Documents;
global using Microsoft.UI.Xaml.Input;
global using Microsoft.UI.Xaml.Markup;
global using Microsoft.UI.Xaml.Media;
global using Microsoft.UI.Xaml.Media.Animation;
global using Microsoft.UI.Xaml.Media.Imaging;
global using Microsoft.UI.Xaml.Navigation;
global using Microsoft.UI.Xaml.Shapes;
global using Microsoft.Windows.AppLifecycle;
global using QuestPDF.Fluent;
global using QuestPDF.Infrastructure;
global using SkiaSharp;
global using Sudoku.Algorithm.Backdoors;
global using Sudoku.Algorithm.Generating;
global using Sudoku.Algorithm.Ittoryu;
global using Sudoku.Algorithm.Solving;
global using Sudoku.Analytics;
global using Sudoku.Analytics.Categorization;
global using Sudoku.Analytics.Configuration;
global using Sudoku.Analytics.Metadata;
global using Sudoku.Analytics.Rating;
global using Sudoku.Analytics.StepSearchers;
global using Sudoku.Compatibility.Hodoku;
global using Sudoku.Compatibility.SudokuExplainer;
global using Sudoku.Concepts;
global using Sudoku.Filtering;
global using Sudoku.Rendering;
global using Sudoku.Rendering.Nodes;
global using Sudoku.Runtime.CompilerServices;
global using Sudoku.Runtime.LibraryServices;
global using Sudoku.Strategying;
global using Sudoku.Strategying.Constraints;
global using Sudoku.Text;
global using Sudoku.Text.Converters;
global using SudokuStudio;
global using SudokuStudio.BindableSource;
global using SudokuStudio.Collection;
global using SudokuStudio.ComponentModel;
global using SudokuStudio.Configuration;
global using SudokuStudio.Input;
global using SudokuStudio.Interaction;
global using SudokuStudio.Interaction.Conversions;
global using SudokuStudio.Rendering;
global using SudokuStudio.Resources;
global using SudokuStudio.Runtime.InteropServices;
global using SudokuStudio.Storage;
global using SudokuStudio.Views.Attached;
global using SudokuStudio.Views.Controls;
global using SudokuStudio.Views.Controls.Shapes;
global using SudokuStudio.Views.Pages;
global using SudokuStudio.Views.Pages.Analyze;
global using SudokuStudio.Views.Pages.ContentDialogs;
global using SudokuStudio.Views.Pages.Manuals;
global using SudokuStudio.Views.Pages.Operation;
global using SudokuStudio.Views.Pages.Settings;
global using SudokuStudio.Views.Windows;
global using Windows.ApplicationModel.Activation;
global using Windows.ApplicationModel.DataTransfer;
global using Windows.Foundation;
global using Windows.Foundation.Collections;
global using Windows.Graphics;
global using Windows.Graphics.Imaging;
global using Windows.Storage;
global using Windows.Storage.Pickers;
global using Windows.Storage.Search;
global using Windows.Storage.Streams;
global using Windows.System;
global using Windows.UI;
global using Windows.UI.Core;
global using Windows.UI.Text;
global using Windows.UI.ViewManagement;
global using Windows.Win32;
global using WinRT;
global using WinRT.Interop;
global using static System.Math;
global using static System.Numerics.BitOperations;
global using static Sudoku.Analytics.ConclusionType;
global using static Sudoku.SolutionFields;
global using static SudokuStudio.ProjectWideConstants;
global using static SudokuStudio.Rendering.RenderableFactory;
global using ColorIdentifierKind = Sudoku.Rendering.WellKnownColorIdentifierKind;
global using DispatcherQueue = Microsoft.UI.Dispatching.DispatcherQueue;
global using DispatcherQueueSynchronizationContext = Microsoft.UI.Dispatching.DispatcherQueueSynchronizationContext;
global using Geometry = Microsoft.UI.Xaml.Media.Geometry;
global using Grid = Sudoku.Concepts.Grid;
global using GridLayout = Microsoft.UI.Xaml.Controls.Grid;
global using HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment;
global using @io = System.IO;
global using LaunchActivatedEventArgs = Microsoft.UI.Xaml.LaunchActivatedEventArgs;
global using LineGeometry = Microsoft.UI.Xaml.Media.LineGeometry;
global using Path = Microsoft.UI.Xaml.Shapes.Path;
global using PathGeometry = Microsoft.UI.Xaml.Media.PathGeometry;
global using PdfColors = QuestPDF.Helpers.Colors;
global using PdfContainer = QuestPDF.Infrastructure.IContainer;
global using ResourceDictionary = System.Resources.ResourceDictionary;
global using Size = Windows.Foundation.Size;
global using SolvingPath = SudokuStudio.Views.Pages.Analyze.SolvingPath;
global using SpecialFolder = System.Environment.SpecialFolder;
global using unsafe StringCheckerFuncPtr = delegate*<string, bool>;
global using TokenItemRemovingEventArgs = SudokuStudio.Interaction.TokenItemRemovingEventArgs;
global using VerticalAlignment = Microsoft.UI.Xaml.VerticalAlignment;
