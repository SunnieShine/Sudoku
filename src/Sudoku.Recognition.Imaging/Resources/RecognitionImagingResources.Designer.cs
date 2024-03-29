﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sudoku.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class RecognitionImagingResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal RecognitionImagingResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Sudoku.Resources.RecognitionImagingResources", typeof(RecognitionImagingResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot recognize any image relating to cell..
        /// </summary>
        internal static string ErrorInfo_CannotRecognizeCellImage {
            get {
                return ResourceManager.GetString("ErrorInfo_CannotRecognizeCellImage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The training file can&apos;t be found in the folder &apos;traineddata&apos;..
        /// </summary>
        internal static string ErrorInfo_MissingTrainedDataFile {
            get {
                return ResourceManager.GetString("ErrorInfo_MissingTrainedDataFile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot fill cell {0} with digit {1}.
        /// </summary>
        internal static string Message_FailedToFillValueException {
            get {
                return ResourceManager.GetString("Message_FailedToFillValueException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The recognition tools should have been initialized before using the current function..
        /// </summary>
        internal static string Message_RecognizerNotInitializedException {
            get {
                return ResourceManager.GetString("Message_RecognizerNotInitializedException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Tesseract has encountered an error: {0}..
        /// </summary>
        internal static string Message_TesseractException {
            get {
                return ResourceManager.GetString("Message_TesseractException", resourceCulture);
            }
        }
    }
}
