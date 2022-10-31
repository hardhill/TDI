using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDA.ViewModels
{
    internal class SourceButtonConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            if (targetType != typeof(ImageSource))
                throw new InvalidOperationException("The target must be a boolean");
            bool IsRunning = (bool)value;
            if (IsRunning)
            {
                var source = ImageSource.FromFile("pause_icon.svg");
                return source;
            }
            else
            {
                var source = ImageSource.FromFile("play_icon.svg");
                return source;
            }
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
