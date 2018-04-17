using System;
using System.Windows;
using System.Windows.Controls;

namespace CookTimeWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly string disclaimer_ENG = @"reminder: Cooking time mostly apply to the outer surface of the dish.
        - Perforate / chop / split up dishes to compensate for lack of heat penetration at lower cooking time.
    - Use bigger chunks(?) when cooking more slowly.
    - This is just an estimation to help already proven cooking skills
    - Apply judgement to end result of this calculation.";

        private static bool canUpdateHeatPoints = true; // false unless reset

        //static bool canUpdateBoxes = false;
        public MainWindow()
        {
            InitializeComponent();
            ///Note: Will not dynamically refresh. Need to find how to use that fun code.
            //var AccentColor = SystemParameters.WindowGlassBrush;
            //this.ResetButton.Background = AccentColor;
            //this.AccentBorder.Background = AccentColor;
            /// One day...
            //Loaded += MainWindow_Loaded;
        }

        //void MainWindow_Loaded(object sender, RoutedEventArgs e)
        //{
        //    AeroResourceInitializer.Initialize();
        //}

        private Temperature CookingTemp = new Temperature();
        private double GlobalTime = 0.0;
        private double GlobalHeatPoints = 0.0;//14000.0;// DEBUG
        //Window GetWindow()
        //{
        //    return this.GetWindow();
        //}

        // Text Box values
        private readonly string TextForKelvin_ALL = "Kelvin";

        private readonly string TextForCelsius_ALL = "Celsius";
        private readonly string TextForFarenheit_ALL = " Farenheit";
        private readonly string TextForMinutes_ALL = "Minutes";
        private readonly string TextForHeatPoints_ENG = "Heat Applied";
        private readonly string TextForHeatPoints_FRA = "Heat Applied";
        private static bool isUILanguageFrench = false;

        // TODO: Automatically split up Minutes value into hours on focus release when >= 61min (leave 60min as-is)
        //private readonly string TextForHours_ENG = "Hour(s)"; // TODO: Dynamic plural?
        //private readonly string TextForHours_FRA = "Heure(s)"; // TODO: Dynamic plural?
        private bool IsCelsiusEmpty()
        {
            return CelsiusBox.Text == TextForCelsius_ALL;
        }

        private bool IsFarenheitEmpty()
        {
            return FarenheitBox.Text == TextForFarenheit_ALL;
        }

        private bool IsKelvinEmpty()
        {
            return KelvinBox.Text == TextForKelvin_ALL;
        }

        private bool IsMinutesEmpty()
        {
            return MinutesBox.Text == TextForMinutes_ALL;
        }

        private bool IsHeatPointsEmpty()
        {
            // Maybe use tryparse since text will return false? IDK
            return HeatPointsBox.Text == (isUILanguageFrench ? TextForHeatPoints_FRA : TextForHeatPoints_ENG);
        }

        private string GetHRTime()
        {
            //TODO: use XAML Text="{Binding Value, StringFormat={}{0:#,#.00}}" instead
            return Math.Round(GlobalTime, 3).ToString();
        }

        private void UpdateHeatPointBoxText()
        {
            /*
            if (!IsCelsiusEmpty())
            {
                CelsiusBox.Text = CookingTemp.Celsius.ToString();
            }
            if (!IsFarenheitEmpty())
            {
                FarenheitBox.Text = CookingTemp.Fahrenheit.ToString();
            }
            if (!IsKelvinEmpty())
                KelvinBox.Text = CookingTemp.Kelvin.ToString();
            if (!IsMinutesEmpty())
                MinutesBox.Text = GlobalTime.ToString();
                */
            //if (!IsHeatPointsEmpty()) // Always update for DEBUG
            HeatPointsBox.Text = GlobalHeatPoints.ToString();
        }

        private void setBoxDefaultValues()
        {
            // TODO: Translate based on Windows UI language ;)
            /// Eventually we should use a Resource Dictionary (https://docs.microsoft.com/en-us/windows/uwp/design/controls-and-patterns/resourcedictionary-and-xaml-resource-references)
            /// but for now let's use manually-defined strings

            // Same value for both languages
            //CelsiusBox.Text = TextForCelsius_ALL;
            //FarenheitBox.Text = TextForFarenheit_ALL;
            //KelvinBox.Text = TextForKelvin_ALL;
            //MinutesBox.Text = TextForMinutes_ALL;
            //// Translatable values
            ////isUILanguageFrench = false; // Get UI language here...
            //if (!isUILanguageFrench)
            //{
            //    HeatPointsBox.Text = TextForHeatPoints_ENG;
            //}
            //else
            //{
            //    HeatPointsBox.Text = TextForHeatPoints_FRA;
            //}
            // SHOVE DISCLAIMER TEXT DATA HERE INTO DISCLAIMER BOX
        }

        //private void UpdateMinutesBox()
        //{
        //    if(KelvinBox.Text == TextForKelvin_ALL)
        //    {
        //        // Default value; Minutes was entered first
        //        // Do nothing as more values are needed...
        //    }
        //    else
        //    {
        //        // We don't "parse" the kelvin box, we already have the value stored in the Temperature Class.
        //        //bool success = Double.TryParse(KelvinBox, out k);
        //        // We could use functions here but this will save us variable creation and such
        //        // What should we do when doing math on the value being typed in? Just check if not focused?
        //        // Maybe do the "related" math in the TextChanged calls? (i.e change farenheit if celsius is focused, then change time if filled in)
        //        double k = CookingTemp.Kelvin; // sample value in function for math
        //        if (!CelsiusBox.IsKeyboardFocused)
        //        {
        //            CelsiusBox.Text = CookingTemp.Celsius.ToString();
        //        }
        //        FarenheitBox.Text = CookingTemp.Fahrenheit.ToString();
        //    }
        //
        //}
        //private void doMath()
        //{
        //    /// THE MATH:
        //    /// Bake time variations for a recipe that calls for 400 degrees for 30 minutes
        //    /// converted to a 450 cooking time and a 350 cooking time:
        //    ///
        //    /// 400 Farenheit = 477.594 Kelvin
        //    ///
        //    /// 477.594 x 30 minutes = 14327.82 HeatPoints
        //    ///
        //    /// 450 F = 505.372 K
        //    ///
        //    /// 14327.82 HP / 505.372 K = 28.35 or 28 minutes 21 seconds
        //    ///
        //
        //    // calculate a new temperature from time with same heatpoints
        //    /// 14327.82 HP / 505.372 K = 28.35 or 28 minutes 21 seconds
        //    /// heatpoints / kelvin = time;
        //    /// heat point      time
        //    /// ----------  = -------
        //    /// kelvin           1
        //    //if (!hasHeatPoints)
        //    {
        //        // Kelvin is implicitely converted, we always have it as long as we enter some temperature.
        //        /// This is the math when calculating the new kelvin value after time changed if it's missing
        //        var k = (GlobalHeatPoints /*   * 1   */) / GlobalTime;
        //        //KelvinBox.Text = k.ToString();
        //        CookingTemp.Kelvin = k;
        //        /// end of kelvin math
        //        double math = CookingTemp.Kelvin * GlobalTime;
        //        HeatPointsBox.Text = math.ToString();
        //        //FarenheitBox.Text = success ? GlobalTemp.Fahrenheit.ToString() : "Please enter Temp in C or F";
        //        //KelvinBox.Text = success ? GlobalTemp.Kelvin.ToString() : "Please enter Temp in C or F";
        //    }
        //    //else
        //    //{
        //    //    Console.WriteLine("Skipping HeatPoint math due to already defined")
        //    //}
        //}
        private void CelsiusBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // TODO: Change celsius box when heat points change
            if (CelsiusBox.IsKeyboardFocused)
            {
                // User is keying a new Celsius value
                double c = 0.0;
                if (Double.TryParse(CelsiusBox.Text, out c))
                {
                    CookingTemp.Celsius = c;
                    // Now that the temperature is updated, calculate the new value for the other boxes
                    //double minute = 0.0;
                    // No need to re-parse, we already have the value if it existed before. Might need
                    // some error checking in here...
                    //if (Double.TryParse(MinutesBox.Text, out minute))
                    // Reminder: Don't STORE global time here, do this in MinutesChanged (and HoursChanged when implemented)
                    // We also don't change the amount of heatpoints, otherwise our food will burn or undercook.
                    // This should no longer be necessary
                    //FarenheitBox.Text = CookingTemp.Fahrenheit.ToString();
                    //KelvinBox.Text = CookingTemp.Kelvin.ToString();
                }
            }
            else
            {
                // Value changed from math result, do we need to do anything? Probably not; should be handled by edited field's function
            }
        }

        private void MinutesBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (MinutesBox.IsKeyboardFocused)
            {
                double time = 0.0;
                if (Double.TryParse(MinutesBox.Text, out time)) // Time is valid
                {
                    // calculate a new kelvin from the new time with same heatpoints
                    /// TODO: Use unit testing to make sure the math is correct based on the following (proven) data.
                    /// 477.594 K * 30 minutes = 14327.82 HeatPoints
                    /// 14327.82 HP / 505.372 K = 28.35 or 28 minutes 21 seconds
                    ///
                    /// K = 477.594;
                    /// Time = 30.0;
                    /// HP = 14327.82
                    ///
                    /// K * Time = HP
                    ///
                    /// HP / K = Time
                    ///
                    ///   K      1
                    /// ----- = ----
                    ///   HP    Time
                    ///
                    /// HP * 1 / Time = K
                    GlobalTime = time;    // TODO: change boxes to have hours and minutes but store time in Minutes?
                    if (canUpdateHeatPoints)
                    {
                        // The heat can change because we reset or are empty
                        GlobalHeatPoints = (CookingTemp.Kelvin * time);
                        // HeatPointsBox.Text = GlobalHeatPoints.ToString();
                    }
                    else
                    {
                        // Heat cannot change; use it to do the math
                        double k = GlobalHeatPoints /* *1 */ / time; // minutes
                        CookingTemp.Kelvin = k;
                    }
                    //HeatPointsBox.Text = GlobalHeatPoints.ToString();
                    UpdateHeatPointBoxText();
                }
            }
        }

        //private void KelvinBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    // update other temperature boxes here (cascade effect), does not require focus, or even writeable box.
        //
        //}

        private void FarenheitBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (FarenheitBox.IsKeyboardFocused)
            {
                // Editing farenheit box, don't change that box from here
                // update other boxes here
                double f = 0.0;
                if (Double.TryParse(FarenheitBox.Text, out f))
                {
                    CookingTemp.Fahrenheit = f;
                    // Update cooking time
                }
            }
            else
            {
                UpdateHeatPointBoxText();
            }
        }

        private void HoursBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // update temp boxes here
        }

        private void HeatPointsBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ///if (!PointsBox.IsKeyboardFocused)
            ///{
            ///    double celsius = 0.0;
            ///    double farenheit = 0.0;
            ///    double kelvin = 0.0;
            ///    double time = 0.0;
            ///    double points = 0.0;
            ///    bool success = Double.TryParse(PointsBox.Text, out points);
            ///    if (success)
            ///    {
            ///        success = Double.TryParse(CelsiusBox.Text, out celsius);
            ///        if (success)
            ///        {
            ///            temp.Celsius = celsius;
            ///            FarenheitBox.Text = temp.Fahrenheit.ToString();
            ///            KelvinBox.Text = temp.Kelvin.ToString();
            ///        }
            ///        /*
            ///        else
            ///        {
            ///            success = success = Double.TryParse(CelsiusBox.Text, out celsius);
            ///        }
            ///        */
            ///        success = Double.TryParse(MinutesBox.Text, out time);
            ///        if (success)
            ///        {
            ///
            ///
            ///            //double math = temp.Kelvin * inputval;
            ///            //PointsBox.Text = success ? math.ToString() : "Heat Points";
            ///        }
            ///    }
            ///}
            ///bool isFocused = HeatPointsBox.IsKeyboardFocused;
            ///bool isEmpty = IsHeatPointsEmpty();
            ///if (isFocused)
            ///{
            ///    // User tried to change the heat points value, probably because the math broke and they tried to correct it;
            ///    // reset to "we don't have heat points" and re-do the math.
            ///    // I should make a reset button.
            ///    //hasHeatPoints = false;
            ///    GlobalHeatPoints = (CookingTemp.Kelvin * GlobalTime);
            ///    // yes.
            ///    HeatPointsBox.Text = GlobalHeatPoints.ToString();
            ///    hasHeatPoints = true;
            ///
            ///}
            ///if (!hasHeatPoints && (isFocused || isEmpty))
            ///{
            ///    hasHeatPoints = true;
            ///}
            ///else
            ///{
            ///    Console.WriteLine("BAD BAD BAD BAD!!!!");
            ///}
            //reaching this place more than once is bad, bad bad.
            // we should protect ourselves against that until the reset button is hit.
            //if (canUpdateHeatPoints)
            {
                //UpdateGUIBoxes();
            }
            Console.WriteLine("Box Value = " + HeatPointsBox.Text);
            Console.WriteLine("Global Value = " + GlobalHeatPoints.ToString());
        }

        private void KelvinBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if(!IsMinutesEmpty())
            //{
            //    // we already got a heat point calculation, update the box with it
            //HeatPointsBox.Text = GlobalHeatPoints.ToString();
            //}

            if (MinutesBox.IsKeyboardFocused /* && something*/)
            {
                // New time was entered, update temps
                //double k = GlobalHeatPoints / GlobalTime;
                CookingTemp.Kelvin = GlobalHeatPoints / GlobalTime;
                UpdateHeatPointBoxText();
            }
            else
            {
                // New temp was entered, update time
                GlobalTime = GlobalHeatPoints / CookingTemp.Kelvin;
                MinutesBox.Text = GetHRTime();
                UpdateHeatPointBoxText();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Toggle heat points Lock
            canUpdateHeatPoints = !canUpdateHeatPoints;
            if (canUpdateHeatPoints)
            {
                ResetButton.Content = "🔓";
            }
            else
            {
                ResetButton.Content = "🔐";
            }
        }
    }
}