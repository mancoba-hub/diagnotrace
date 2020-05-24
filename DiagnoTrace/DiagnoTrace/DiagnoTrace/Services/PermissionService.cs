using DiagnoTrace.Interfaces;
using Xamarin.Essentials;

namespace DiagnoTrace.Services
{
    public class PermissionService : IPermissionService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionService"/> class.
        /// </summary>
        public PermissionService()
        {
        }

        /// <summary>
        /// Checks the network connectivity.
        /// </summary>
        /// <returns>
        /// The network connectivity status.
        /// </returns>
        public NetworkAccess CheckNetworkConnectivity()
        {
            // returns the network status
            return Connectivity.NetworkAccess;
        }
    }
}
