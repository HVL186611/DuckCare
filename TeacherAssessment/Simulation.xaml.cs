using TeacherAssessment.ViewModels; //ha med for "using"

namespace TeacherAssessment;

public partial class Simulation : ContentPage
{
	public Simulation()
	{
		InitializeComponent();

		BindingContext = new MainViewModel(); //Set BindingContext som MainViewModel();
	}
}