namespace SudokuStudio.Views.Pages;

/// <summary>
/// A page that displays all techniques.
/// </summary>
[DependencyProperty<Technique>("CurrentSelectedTechnique", Accessibility = Accessibility.Internal, DocSummary = "Indicates the currently selected technique.")]
public sealed partial class TechniqueGalleryPage : Page
{
	/// <summary>
	/// Initializes a <see cref="TechniqueGalleryPage"/> instance.
	/// </summary>
	public TechniqueGalleryPage() => InitializeComponent();


	private void TechniqueCoreView_CurrentSelectedTechniqueChanged(TechniqueView sender, TechniqueViewCurrentSelectedTechniqueChangedEventArgs e)
		=> CurrentSelectedTechnique = e.Technique;
}
