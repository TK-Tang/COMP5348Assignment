using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using VideoStore.Business.Components.Interfaces;
using System.Messaging;

namespace VideoStore.Business.Components
{
    public class EmailProvider : IEmailProvider
    {
        public void SendMessage(EmailMessage pMessage)
        {


            ExternalServiceFactory.Instance.EmailService.SendEmail
                (
                    new global::EmailService.MessageTypes.EmailMessage()
                    {
                        Message = pMessage.Message,
                        ToAddresses = pMessage.ToAddress,
                        Date = DateTime.Now
                    }
                );
        }
    }
}
