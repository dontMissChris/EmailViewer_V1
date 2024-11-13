using MailKit.Net.Imap;
using MailKit;
using MimeKit;
using System;
using System.Collections.Generic;
using EmailPortal.Models;

namespace EmailPortal.Services
{
    public class EmailService : EmailServiceBase
    {
        private readonly string _imapServer = "imap.gmx.net";
        private readonly int _imapPort = 993;
        private readonly string _email = "TestSandra@gmx.de";
        private readonly string _password = "SandraIstNett";

        public List<Email> FetchAllEmails()
        {
            var emails = new List<Email>();

            try
            {
                using (var client = new ImapClient())
                {
                    client.Connect(_imapServer, _imapPort, true);
                    client.Authenticate(_email, _password);

                    var inbox = client.Inbox;
                    inbox.Open(FolderAccess.ReadOnly);

                    var messages = inbox.Fetch(0, -1, MessageSummaryItems.Envelope | MessageSummaryItems.Flags);

                    Console.WriteLine("Start: E-Mails werden geladen..."); // Debug-Ausgabe
                    foreach (var message in messages)
                    {
                        var email = new Email
                        {
                            Subject = message.Envelope.Subject,
                            Sender = message.Envelope.From.ToString(),
                            ReceivedDate = message.Envelope.Date?.LocalDateTime ?? DateTime.MinValue,
                            IsSeen = message.Flags.HasValue && message.Flags.Value.HasFlag(MessageFlags.Seen)
                        };
                        Console.WriteLine($"Geladen: {email.Subject} - Gelesen: {email.IsSeen}"); // Debug-Ausgabe
                        emails.Add(email);
                    }
                    Console.WriteLine("Ende: E-Mail-Ladevorgang abgeschlossen"); // Debug-Ausgabe

                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Abrufen der E-Mails: {ex.Message}");
            }

            return emails;
        }
    }
}
