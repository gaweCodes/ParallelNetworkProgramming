using System.Threading.Tasks;
using System.Windows;

namespace TestUIThread 
{
    public partial class MainWindow : Window 
    {
        private bool Cancelled { get; set; }
        private bool Calculating { get; set; }
        public MainWindow() => InitializeComponent();
        private async void startCalculationButton_Click(object sender, RoutedEventArgs e)
        {
            if (Calculating) 
            { 
                Cancelled = true;
                return;
            }
            if (!long.TryParse(baseNumberTextBox.Text, out var initial) ||  !long.TryParse(succeedingPrimesTextBox.Text, out var amount))
                return;

            Calculating = true;
            startCalculationButton.Content = "Cancel";
            await ComputeNextPrimesAsync(initial, amount);
            progressLabel.Content = Cancelled ? "cancelled" : "done";
            startCalculationButton.Content = "Start";
            Calculating = false;
            Cancelled = false;
        }
        private async Task ComputeNextPrimesAsync(long inital, long amount) 
        {
            for (var number = inital; number < inital + amount; number++) 
            {
                if (await IsPrime(number))
                    resultListView.Items.Add(number);
                var progress = (number - inital + 1) * 100 / amount;
                progressLabel.Content = progress + "% computed";
            }
        }
        private static Task<bool> IsPrime(long number)
        {
            return Task.Run(() => {
                for (long i = 2; i * i <= number; i++)
                {
                    if (number % i == 0) return false;
                }
                return true;
            });
        }
    }
}
