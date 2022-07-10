﻿global using System;
global using System.Collections;
global using System.Collections.Generic;
global using System.Collections.ObjectModel;
global using System.Collections.Specialized;
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
global using System.Runtime.InteropServices.WindowsRuntime;
global using System.Text;
global using System.Text.Json;
global using System.Text.Json.Serialization;
global using System.Text.RegularExpressions;
global using System.Threading;
global using System.Threading.Tasks;
global using Microsoft.Graphics.Canvas.Text;
global using Microsoft.UI;
global using Microsoft.UI.Composition;
global using Microsoft.UI.Composition.SystemBackdrops;
global using Microsoft.UI.Dispatching;
global using Microsoft.UI.Windowing;
global using Microsoft.UI.Xaml;
global using Microsoft.UI.Xaml.Controls;
global using Microsoft.UI.Xaml.Controls.Primitives;
global using Microsoft.UI.Xaml.Data;
global using Microsoft.UI.Xaml.Input;
global using Microsoft.UI.Xaml.Media;
global using Microsoft.UI.Xaml.Media.Animation;
global using Microsoft.UI.Xaml.Navigation;
global using Microsoft.UI.Xaml.Printing;
global using Microsoft.Windows.AppLifecycle;
global using Microsoft.Windows.System.Power;
global using Sudoku.Concepts;
global using Sudoku.Concepts.Solving;
global using Sudoku.Generating.Puzzlers;
global using Sudoku.Runtime.AnalysisServices;
global using Sudoku.Solving.Manual;
global using Sudoku.UI.Data.AppLifecycle;
global using Sudoku.UI.Data.Configuration;
global using Sudoku.UI.Data.Metadata;
global using Sudoku.UI.Data.Navigation;
global using Sudoku.UI.Drawing;
global using Sudoku.UI.Drawing.Shapes;
global using Sudoku.UI.Input;
global using Sudoku.UI.Interop;
global using Sudoku.UI.LocalStorages;
global using Sudoku.UI.Models;
global using Sudoku.UI.Models.InfoBarMessages;
global using Sudoku.UI.Models.SettingItems;
global using Sudoku.UI.Views.Controls;
global using Sudoku.UI.Views.Pages;
global using Sudoku.UI.Views.Windows;
global using Windows.ApplicationModel.Activation;
global using Windows.ApplicationModel.DataTransfer;
global using Windows.Graphics;
global using Windows.Graphics.Printing;
global using Windows.Storage;
global using Windows.Storage.Pickers;
global using Windows.Storage.Provider;
global using Windows.Storage.Search;
global using Windows.Storage.Streams;
global using Windows.System;
global using Windows.UI.ViewManagement;
global using WinRT;
global using WinRT.Interop;
global using static System.Math;
global using static System.Numerics.BitOperations;
global using static Sudoku.Resources.MergedResources;
global using static Sudoku.Runtime.AnalysisServices.CommonReadOnlies;
global using MsDispatcherQueue = Microsoft.UI.Dispatching.DispatcherQueue;
global using MsLaunchActivatedEventArgs = Microsoft.UI.Xaml.LaunchActivatedEventArgs;
global using MsWindowActivatedEventArgs = Microsoft.UI.Xaml.WindowActivatedEventArgs;
global using Grid = Sudoku.Concepts.Collections.Grid;
global using EnvironmentFolders = System.Environment.SpecialFolder;
global using SioDirectory = System.IO.Directory;
global using SioFile = System.IO.File;
global using SioPath = System.IO.Path;
global using Key = Windows.System.VirtualKey;
global using ModifierKey = Windows.System.VirtualKeyModifiers;
global using Color = Windows.UI.Color;
