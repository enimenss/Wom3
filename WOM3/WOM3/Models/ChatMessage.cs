using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WOM3.Models
{
    public class ChatMessage
    {
        public string Message { get; set; }
        public DateTime Datum { get; set; }

        public ChatMessage(Messages m)
        {
            if (m == null)
            {
                Message = "";
                Datum = DateTime.Parse("2/3/2010");
                return;
            }
            Message = m.Message;
            Datum = m.Datum;
        }
    }
}