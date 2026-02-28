
using System.Globalization;


namespace EddSt.Page;


public partial class EggPage : ContentPage
{
	
    
    public EggPage()
	{
		InitializeComponent();
         
       


    }

}


internal class ConvertHeght : IMultiValueConverter
{
    
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
		
        return (double)values[0]- (double)values[1]-30;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
#pragma warning disable CS8603 // ¬озможно, возврат ссылки, допускающей значение NULL.
        return null;
#pragma warning restore CS8603 // ¬озможно, возврат ссылки, допускающей значение NULL.
    }
}
 