using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Windows;

namespace Demo2_AsyncDownloadGUI {
    /**
     * One Way to solve is my current implementation. Another way would be a semaphor or something like this.
     */
    public partial class MainWindow : Window 
    {
        private IEnumerable<string> UrlCollection => urlListBox.Items.Cast<string>();
        public MainWindow() => InitializeComponent();
        private void addButton_Click(object sender, RoutedEventArgs e) => urlListBox.Items.Add(urlEntryTextBox.Text);
        private async void downloadButton_Click(object sender, RoutedEventArgs e) 
        {
            var client = new HttpClient();
            foreach (var url in UrlCollection.ToList())
            {
                var data = await client.GetStringAsync(url);
                outputTextBox.Text += $"{url} downloaded: {data.Length} bytes {Environment.NewLine}";
            }
        }
    }
}
