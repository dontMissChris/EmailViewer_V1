using EmailPortal.Models;
using EmailPortal.Services;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace EmailPortal
{
    public partial class MainWindow : Window
    {
        private EmailService emailService;

        public MainWindow()
        {
            InitializeComponent();
            emailService = new EmailService(); // Initialisiere den EmailService
        }

        // Button für "Unbearbeitete" Mails
        private void SortByUnprocessed(object sender, RoutedEventArgs e)
        {
            var unprocessedEmails = emailService.FetchAllEmails(); // Abruf der Mails
            DisplayEmails(unprocessedEmails);  // Anzeige der E-Mails
        }

        // Button für "Bearbeitete" Mails
        private void SortByProcessed(object sender, RoutedEventArgs e)
        {
            var processedEmails = emailService.FetchAllEmails(); // Abruf der Mails
            DisplayEmails(processedEmails);
        }

        // Button für "In Bearbeitung" Mails
        private void SortByInProgress(object sender, RoutedEventArgs e)
        {
            var inProgressEmails = emailService.FetchAllEmails(); // Abruf der Mails
            DisplayEmails(inProgressEmails);
        }

        // Methode, um die E-Mails anzuzeigen
        private void DisplayEmails(List<Email> emails)
        {
            emailListBox.Items.Clear();  // ListBox leeren

            foreach (var email in emails)
            {
                // Erstelle ein neues StackPanel für jede E-Mail
                var emailItem = new StackPanel();
                var subjectTextBlock = new TextBlock { Text = email.Subject, FontWeight = FontWeights.Bold };
                var senderTextBlock = new TextBlock { Text = email.Sender };
                var receivedDateTextBlock = new TextBlock { Text = email.ReceivedDate.ToString() };

                // Hier wird der Rand (Border) der E-Mail angepasst
                var border = new Border
                {
                    BorderBrush = System.Windows.Media.Brushes.Black,  // Schwarzer Rand
                    BorderThickness = new System.Windows.Thickness(2),  // Randstärke
                    Padding = new System.Windows.Thickness(5),  // Etwas Abstand innerhalb des Rands
                    Margin = new System.Windows.Thickness(5),  // Abstand zu anderen Elementen
                };

                // Farbige Markierung je nach Status
                if (!email.IsSeen) // Unbearbeitete Mails (Ungelesen)
                {
                    emailItem.Background = System.Windows.Media.Brushes.Red;
                    subjectTextBlock.Foreground = System.Windows.Media.Brushes.White; // Weißer Text für bessere Lesbarkeit
                }
                else // Bearbeitete Mails (Gelesen)
                {
                    emailItem.Background = System.Windows.Media.Brushes.Green;
                    subjectTextBlock.Foreground = System.Windows.Media.Brushes.White;
                }

                // Füge die TextBlocks zur StackPanel hinzu
                emailItem.Children.Add(subjectTextBlock);
                emailItem.Children.Add(senderTextBlock);
                emailItem.Children.Add(receivedDateTextBlock);

                // Füge das StackPanel in das Border-Element ein
                border.Child = emailItem;

                // Füge das Border-Element zur ListBox hinzu
                emailListBox.Items.Add(border);
            }
        }
    }
}
