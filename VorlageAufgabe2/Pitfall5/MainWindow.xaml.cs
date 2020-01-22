using System;
using System.Threading.Tasks;
using System.Windows;

namespace Pitfall5 
{
    public partial class MainWindow : Window 
    {
        public MainWindow() => InitializeComponent();
        private async void calculationButton_Click(object sender, RoutedEventArgs e) 
        {
            var task = CalculateAsync();
            resultLabel.Content = task.Result;
        }
        private async Task<string> CalculateAsync() 
        {
            var number = long.Parse(inputTextBox.Text);
            return await Task.Run(() => {
                for (long i = 2; i <= Math.Sqrt(number); i++) 
                {
                    if (number % i == 0) {
                        return "Not prime";
                    }
                }
                return "Prime";
            });
        }
    }
}
