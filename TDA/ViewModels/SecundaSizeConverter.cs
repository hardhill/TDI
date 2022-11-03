using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDA.ViewModels
{
    internal class SecundaSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BreathParam paramBreath = (BreathParam)value;
            int size = Int32.Parse((string)parameter);
            if (paramBreath == null) return null;
            int secondsOfExhale, newSize;
            if (paramBreath.StadiaSec > paramBreath.BreathSec)
            {
                // exhale stadia
                
                secondsOfExhale = paramBreath.StadiaSec - paramBreath.BreathSec;
                double a = (double)secondsOfExhale / (double)paramBreath.ExhaleSec;
                double b = a * size;
                newSize = (int)Math.Round(b)+5;
                
            }
            else
            {
                // breath statia
                newSize = size - (int)Math.Round(((double)paramBreath.StadiaSec/(double)paramBreath.BreathSec)*size)+5;

            }

            return newSize;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
