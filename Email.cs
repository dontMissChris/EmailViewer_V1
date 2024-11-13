using System;

namespace EmailPortal.Models
{
    public class Email
    {
        public string Subject { get; set; }
        public string Sender { get; set; }
        public DateTime ReceivedDate { get; set; }
        public bool IsSeen { get; set; }
    }
}
