namespace EddSt.Page;

public partial class Finance : ContentPage
{
	public Finance()
	{
        
		InitializeComponent();
LableDNow.Text = DateOnly.FromDateTime(DateTime.Now).ToString();

		
    }
}