using Microsoft.UI.Xaml.Controls;
using SudokuStudio.ComponentModel;
using static SudokuStudio.Strings.StringsAccessor;

namespace SudokuStudio.Views.Pages.ContentDialogs;

/// <summary>
/// Defines a dialog content for adding or loading a puzzle library.
/// </summary>
[DependencyProperty<bool>("IsAdding", DocSummary = "Indicates whether the puzzle library is adding a new one, instead of loading.")]
[DependencyProperty<string>("FilePath", IsNullable = true, DocSummary = "Indicates the target file path.")]
[DependencyProperty<string>("FileId", IsNullable = true, DocSummary = "Indicates the file ID.")]
[DependencyProperty<string>("LibraryName", IsNullable = true, DocSummary = "Indicates the library name.")]
[DependencyProperty<string>("LibraryAuthor", IsNullable = true, DocSummary = "Indicates the author of the library.")]
[DependencyProperty<string>("LibraryDescription", IsNullable = true, DocSummary = "Indicates the description of the library.")]
[DependencyProperty<string[]>("LibraryTags", IsNullable = true, DocSummary = "Indicates the tags to the library.")]
public sealed partial class LibraryLoadOrAddContent : Page
{
	[Default]
	private static readonly string LibraryAuthorDefaultValue = GetString("AnonymousAuthor");


	/// <summary>
	/// Initializes a <see cref="LibraryLoadOrAddContent"/> instance.
	/// </summary>
	public LibraryLoadOrAddContent() => InitializeComponent();
}
