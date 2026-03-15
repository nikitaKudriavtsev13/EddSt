namespace EddSt.Page;

public partial class Finance : ContentPage
{
	public Finance()
	{
		LableDNow.Text = DateOnly.FromDateTime(DateTime.Now).ToString();
		InitializeComponent();


		
    }
}