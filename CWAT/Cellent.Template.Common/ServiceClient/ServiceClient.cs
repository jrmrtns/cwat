using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Cellent.Template.Common.ServiceClient
{
    /// <summary>
    /// Generischer Client für WCF Services
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class ServiceClient<T>
    {
        private static readonly ChannelFactory<T> ChannelFactory = new ChannelFactory<T>(Constants.Constants.Urls.EndpointConfigurationName);

        /// <summary>
        /// Executes a wcf service call asynchronous.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="action">The action.</param>
        /// <returns>the result</returns>
        public static async Task<TResult> ExecuteAsync<TResult>(Func<T, Task<TResult>> action)
        {
            IClientChannel clientChannel = (IClientChannel)ChannelFactory.CreateChannel();

            bool success = false;
            TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
            try
            {
                taskCompletionSource.TrySetResult(await DoExecuteAsync(action, clientChannel));
                clientChannel.Close();
                success = true;
            }
            catch (Exception ex)
            {
                taskCompletionSource.TrySetException(ex);
            }
            finally
            {
                if (!success)
                {
                    clientChannel.Abort();
                }
            }
            return await taskCompletionSource.Task;
        }

        private static Task<TResult> DoExecuteAsync<TResult>(Func<T, Task<TResult>> action, IClientChannel clientChannel)
        {
#if DEBUG
            if (!string.IsNullOrEmpty(Constants.Constants.OnBehalfUser))
            {
                using (new OperationContextScope(clientChannel))
                {
                    System.ServiceModel.Channels.MessageHeader header = System.ServiceModel.Channels.MessageHeader.CreateHeader("OnBehalfUser", "http://daimler.com", Constants.Constants.OnBehalfUser);
                    OperationContext.Current.OutgoingMessageHeaders.Add(header);
                    return action((T)clientChannel);
                }
            }
#endif
            return action((T)clientChannel);
        }

        /// <summary>
        /// Executes the specified action.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="action">The action.</param>
        /// <returns>the result</returns>
        public static TResult Execute<TResult>(Func<T, TResult> action)
        {
            IClientChannel clientChannel = (IClientChannel)ChannelFactory.CreateChannel();
            TResult result;

            bool success = false;
            try
            {
                result = action((T)clientChannel);
                clientChannel.Close();
                success = true;
            }
            finally
            {
                if (!success)
                {
                    clientChannel.Abort();
                }
            }
            return result;
        }

        /// <summary>
        /// Executes the specified action.
        /// </summary>
        /// <param name="action">The action.</param>
        public static void Execute(Action<T> action)
        {
            IClientChannel clientChannel = (IClientChannel)ChannelFactory.CreateChannel();

            bool success = false;

            try
            {
                action((T)clientChannel);
                clientChannel.Close();
                success = true;
            }
            finally
            {
                if (!success)
                {
                    clientChannel.Abort();
                }
            }
        }
    }
}