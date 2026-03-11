namespace EddSt.Page;

public partial class Abaut : ContentPage
{
	public Abaut()
	{
		InitializeComponent();
	}
    private void languagePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
		var item=sender as Picker;
		switch (item.SelectedIndex)
        {
            case 0:
                Application.Current.UserAppTheme = AppTheme.Light;
                break;
            case 1:
                Application.Current.UserAppTheme = AppTheme.Dark;
                break;
            case 2:
                Application.Current.UserAppTheme = AppTheme.Unspecified;
                break;

            default:
                break;
		}
	}
}