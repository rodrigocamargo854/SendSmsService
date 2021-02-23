using System;
using Domain.Models.SendEntities;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace SendEmailAplication
{
    public class PostSms
    {
        public static void SendSms(SenderEntity entity)
        {
            string accountSid = "AC1f23341bbbd8f2427e79bbc100da08b0";
            string authToken = "65e59ac5908aac0ce495814dbf47ea1d";
            TwilioClient.Init (accountSid, authToken);
            var to = entity.UserPhone;
            var from = "+18478921617";
            var message = MessageResource.Create (
                to: to,
                from: from,
                body: $"O produto {entity.ProductName } que você escolheu chegou a faixa de preço desejada link: {entity.ProductLinkRedirect}");

            Console.WriteLine (message.Sid);
            Console.ReadLine ();
            

        }
    }
}