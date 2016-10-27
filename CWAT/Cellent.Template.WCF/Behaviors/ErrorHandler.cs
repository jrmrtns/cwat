using Cellent.Template.Common.Exceptions;
using Cellent.Template.Common.Logger;
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using Cellent.Template.Common.Constants;

namespace Cellent.Template.WCF.Behaviors
{
    /// <summary>
    /// ErrorHandler
    /// </summary>
    public class ErrorHandler : IErrorHandler
    {
        /// <summary>
        /// Enables the creation of a custom <see cref="T:System.ServiceModel.FaultException`1" /> that is returned from an exception in the course of a service method.
        /// </summary>
        /// <param name="error">The <see cref="T:System.Exception" /> object thrown in the course of the service operation.</param>
        /// <param name="version">The SOAP version of the message.</param>
        /// <param name="fault">The <see cref="T:System.ServiceModel.Channels.Message" /> object that is returned to the client, or service, in the duplex case.</param>
        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            if (error is FaultException)
                return;

            FaultException<RemoteFault> faultException =
                new FaultException<RemoteFault>(new RemoteFault(error.Message, Constants.FaultExceptionEnum.Default), error.Message);

            fault = Message.CreateMessage(version, faultException.CreateMessageFault(), faultException.Action);
        }

        /// <summary>
        /// Enables error-related processing and returns a value that indicates whether the dispatcher aborts the session and the instance context in certain cases.
        /// </summary>
        /// <param name="error">The exception thrown during processing.</param>
        /// <returns>
        /// true if Windows Communication Foundation (WCF) should not abort the session (if there is one) and instance context if the instance context is not <see cref="F:System.ServiceModel.InstanceContextMode.Single" />; otherwise, false. The default is false.
        /// </returns>
        public bool HandleError(Exception error)
        {
            Logger.Write(error);
            return true;
        }
    }
}