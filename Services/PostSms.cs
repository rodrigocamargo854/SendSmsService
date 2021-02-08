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
            string accountSid = "ACf11bb0b42705c262cb47ccf1e6ebaa8b";
            string authToken = "3f16399316f2ae0873dadb8bae42b612";
            TwilioClient.Init (accountSid, authToken);
            var to = entity.UserPhone;
            var from = "+14158959780";
            var message = MessageResource.Create (
                to: to,
                from: from,
                body: $"O produto {entity.ProductName } que você escolheu chegou a faixa de preço desejada link: {entity.ProductLinkRedirect}");

            Console.WriteLine (message.Sid);
            Console.ReadLine ();
            

        }
    }
}