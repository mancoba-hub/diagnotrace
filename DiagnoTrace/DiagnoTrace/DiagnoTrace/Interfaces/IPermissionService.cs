using Xamarin.Essentials;

namespace DiagnoTrace.Interfaces
{
    public interface IPermissionService
    {
        /// <summary>
        /// Checks the network connectivity.
        /// </summary>
        /// <returns>The network connectivity status.</returns>
        NetworkAccess CheckNetworkConnectivity();
    }
}