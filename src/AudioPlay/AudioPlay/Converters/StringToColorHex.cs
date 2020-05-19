using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace AudioPlay.Converters
{
    public class StringToColorHex : IValueConverter
    {
        string[] colors = new string[] { "#FC350B", "##284A78", "#306345", "#CB161B", "#291A2D", "#9B720F", "#1E4239" };
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Random random = new Random();
            var color = (string)value;
            switch (color)
            {
                case "Rap":
                    return Color.FromHex(colors[0]);
                case "HipHop":
                    return Color.FromHex(colors[1]);
                case "Pop Music":
                    return Color.FromHex(colors[2]);
                case "Blues":
                    return Color.FromHex(colors[3]);
                case "Children’s Music":
                    return Color.FromHex(colors[4]);
                case "Classical":
                    return Color.FromHex(colors[5]);
                case "Techno":
                    return Color.FromHex(colors[6]);
                default:
                    return Color.FromHex(colors[random.Next(6)]);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Color.FromHex(colors[6]);
        }
    }
}
