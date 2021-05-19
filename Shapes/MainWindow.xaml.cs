using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Shapes {
    public partial class MainWindow {
        public MainWindow() {
            InitializeComponent();
            DataContext = this;
            UpdateSum();
        }

        public ObservableCollection<Shape> Shapes { get; } = new();
        
        private void ValidateNumericInput(object sender, TextCompositionEventArgs e) => e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        private void TypeCbx_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
            switch ((string) ((ComboBoxItem) ((ComboBox) sender).SelectedItem).Content) {
                case "Rectangle":
                case "Triangle":
                    RadiusLbl.Visibility = Visibility.Hidden;
                    HeightLbl.Visibility = Visibility.Visible;
                    WidthLbl.Visibility = Visibility.Visible;
                    
                    RadiusTbx.Visibility = Visibility.Hidden;
                    HeightTbx.Visibility = Visibility.Visible;
                    WidthTbx.Visibility = Visibility.Visible;
                    break;
                case "Circle":
                    RadiusLbl.Visibility = Visibility.Visible;
                    HeightLbl.Visibility = Visibility.Hidden;
                    WidthLbl.Visibility = Visibility.Hidden;
                    
                    RadiusTbx.Visibility = Visibility.Visible;
                    HeightTbx.Visibility = Visibility.Hidden;
                    WidthTbx.Visibility = Visibility.Hidden;
                    break;
            }
        }
        private void AddBtn_OnClick(object sender, RoutedEventArgs e) {
            int r = 0;
            int w = 0;
            int h = 0;
            if (TypeCbx.Text == "Circle")
                r = GetIntSafe(RadiusTbx.Text);
            else {
                w = GetIntSafe(WidthTbx.Text);
                h = GetIntSafe(HeightTbx.Text);
            }
            
            Shapes.Add(new Shape(TypeCbx.Text, r, w, h, CalculationHelper.GetArea(TypeCbx.Text, r, w, h)));
            RadiusTbx.Text = "";
            WidthTbx.Text = "";
            HeightTbx.Text = "";
            
            UpdateSum();
        }
        private static int GetIntSafe(string text) => int.TryParse(text, out int v) ? v : 0;

        private void UpdateSum() {
            AreaSumLbl.Content = "Sum: " + Shapes.Select(s => s.Area).Sum();
        }
    }
}
