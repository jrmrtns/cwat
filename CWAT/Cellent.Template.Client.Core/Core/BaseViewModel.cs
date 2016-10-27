using Prism.Regions;

namespace Cellent.Template.Client.Core.Core
{
    /// <summary>
    /// Basisklasse für ViewMOdels
    /// </summary>
    public class BaseViewModel : INavigationAware
    {
        /// <summary>
        /// Called when the implementer has been navigated to.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
        }

        /// <summary>
        /// Called to determine if this instance can handle the navigation request.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        /// <returns>
        /// <see langword="true" /> if this instance accepts the navigation request; otherwise, <see langword="false" />.
        /// </returns>
        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        /// <summary>
        /// Called when the implementer is being navigated away from.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}