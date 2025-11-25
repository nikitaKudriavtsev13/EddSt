using EddSt.Page.data;
using System.Collections.ObjectModel;
using System.IO;

namespace EddSt.Page;

public partial class StatEgg : ContentPage
{
    


    public StatEgg()
	{
		InitializeComponent();
       
    }

    private void stWeek_Clicked(object sender, EventArgs e)
    {
        var stEggViewModel =  new EggViewModel();
        sArefm.Text = stEggViewModel.EggSred().ToString();
            all.Text    = stEggViewModel.EggAll().ToString();
    }
}