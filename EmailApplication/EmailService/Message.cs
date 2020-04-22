using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailService
{
    public class Message
    {
        public string Receiver { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }

    }
}
