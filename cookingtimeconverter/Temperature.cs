using System.Linq;
using System.Windows;

namespace CookTimeWPF
{
    public class Temperature
    {
        private double _kelvin = 0.0;
        public bool Dirty = true;

        public double Kelvin
        {
            get
            { //return _kelvin;
                return System.Math.Round(_kelvin, 2);
            }
            set
            {
                _kelvin = value;
                var ThatWindowOverThere = Application.Current.Windows.Cast<Window>()
                    .FirstOrDefault(window => window is MainWindow) as MainWindow;
                ThatWindowOverThere.KelvinBox.Text = value.ToString();
                ThatWindowOverThere.FarenheitBox.Text = Fahrenheit.ToString();
                ThatWindowOverThere.CelsiusBox.Text = Celsius.ToString();
            }
        }

        public double Fahrenheit
        {
            get
            {
                //double newval = (9.0 / 5.0) * (_kelvin - 273.15) + 32.0;
                //Console.WriteLine(_kelvin + " Kelvin => " + newval + " Farenheit");
                return System.Math.Round(((9.0 / 5.0) * (_kelvin - 273.15) + 32.0), 2);
            }
            set
            {
                _kelvin = (5.0 / 9.0) * (value - 32.0) + 273.15; /* convert Fahrenheit to Kelvin */
                var ThatWindowOverThere = Application.Current.Windows.Cast<Window>()
                        .FirstOrDefault(window => window is MainWindow) as MainWindow;
                ThatWindowOverThere.FarenheitBox.Text = value.ToString();
                ThatWindowOverThere.CelsiusBox.Text = Celsius.ToString();
                ThatWindowOverThere.KelvinBox.Text = _kelvin.ToString();
            }
        }

        public double Celsius
        {
            get
            {
                //double newval = _kelvin - 273.15;
                //Console.WriteLine(_kelvin + " Kelvin => " + newval + " Celsius");
                return System.Math.Round((_kelvin - 273.15), 2);/* convert Kelvin to Celsius */
            }
            //set { _kelvin = value + changeToK; /* convert Celsius to Kelvin */ }
            set
            {
                _kelvin = (value + 273.15);
                //Console.WriteLine(value + " Celsius => " + _kelvin + " Kelvin"); /* convert Celsius to Kelvin */
                var ThatWindowOverThere = Application.Current.Windows.Cast<Window>()
                        .FirstOrDefault(window => window is MainWindow) as MainWindow;
                ThatWindowOverThere.KelvinBox.Text = _kelvin.ToString();
                ThatWindowOverThere.FarenheitBox.Text = Fahrenheit.ToString();
                ThatWindowOverThere.CelsiusBox.Text = Celsius.ToString();
            }
        }

        public static Temperature FromKelvin(double kelvin)
        {
            var temperature = new Temperature();
            temperature.Kelvin = kelvin;
            return temperature;
        }

        public static Temperature FromFahrenheit(double fahrenheit)
        {
            var temperature = new Temperature();
            temperature.Fahrenheit = fahrenheit;
            return temperature;
        }

        public static Temperature FromCelsius(double celsius)
        {
            var temperature = new Temperature();
            temperature.Celsius = celsius;
            //Console.WriteLine(temperature.Celsius + " Celsius => " + temperature.Kelvin + " Kelvin");
            return temperature;
        }

        public static Temperature operator +(Temperature yc1, Temperature yc2)
        {
            return new Temperature() { _kelvin = yc1.Kelvin + yc2.Kelvin };
        }

        public static Temperature operator /(Temperature yc1, Temperature yc2)
        {
            return new Temperature() { _kelvin = yc1.Kelvin / yc2.Kelvin };
        }

        public static Temperature operator *(Temperature yc1, Temperature yc2)
        {
            return new Temperature() { _kelvin = yc1.Kelvin * yc2.Kelvin };
        }

        public static Temperature operator -(Temperature yc1, Temperature yc2)
        {
            return new Temperature() { _kelvin = yc1.Kelvin - yc2.Kelvin };
        }
    }
}