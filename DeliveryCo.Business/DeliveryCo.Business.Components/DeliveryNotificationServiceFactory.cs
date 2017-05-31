using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeliveryCo.Services.Interfaces;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace DeliveryCo.Business.Components
{
    public class DeliveryNotificationServiceFactory
    {
        public static IDeliveryNotificationService GetDeliveryNotificationService(String pAddress)
        {
            return GetChannelFactory<IDeliveryNotificationService>(pAddress).CreateChannel();
        }

        public static ChannelFactory<T1> GetChannelFactory<T1>(string pHandlerAddress)
        {
            Binding lBinding;
            if (pHandlerAddress.Contains("net.tcp"))
            {
                lBinding = new NetTcpBinding();
            }
            else if (pHandlerAddress.Contains("net.msmq"))
            {
                lBinding = new NetMsmqBinding(NetMsmqSecurityMode.None) {Durable = true, ExactlyOnce = false };
            }
            else
            {
                throw new Exception("Unrecognized address type");
            }

            EndpointAddress myEndpoint = new EndpointAddress(pHandlerAddress);
            ChannelFactory<T1> myChannelFactory = new ChannelFactory<T1>(lBinding, myEndpoint);

            return myChannelFactory;
        }
    }
}
