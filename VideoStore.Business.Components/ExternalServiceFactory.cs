using Bank.Services.Interfaces;
using DeliveryCo.Services.Interfaces;
using EmailService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace VideoStore.Business.Components
{
    public class ExternalServiceFactory
    {
        private static ExternalServiceFactory sFactory = new ExternalServiceFactory();

        public static ExternalServiceFactory Instance
        {
            get
            {
                return sFactory;
            }
        }



        public IEmailService EmailService
        {
            get
            {
                return GetMsmqService<IEmailService>("net.msmq://localhost/private/EmailService");
            }
        }

        public ITransferService TransferService
        {
            get
            {
                return GetMsmqService<ITransferService>("net.msmq://localhost/private/TransferService");
            }
        }

        public IDeliveryService DeliveryService
        {
            get
            {
                return GetMsmqService<IDeliveryService>("net.msmq://localhost/private/DeliveryService");
            }
        }

        private T GetMsmqService<T>(String pAddress)
        {
            NetMsmqBinding netMsmqBinding = new NetMsmqBinding(NetMsmqSecurityMode.None) { Durable = false , ExactlyOnce = false };
            netMsmqBinding.Security.Mode = 0;
            EndpointAddress address = new EndpointAddress(pAddress);
            return new ChannelFactory<T>(netMsmqBinding, address).CreateChannel();
        }
    }
}
