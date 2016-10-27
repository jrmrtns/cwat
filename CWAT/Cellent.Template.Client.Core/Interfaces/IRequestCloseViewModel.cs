using System;

namespace Cellent.Template.Client.Core.Interfaces
{
    /// <summary>
    /// Stellt die Möglichkeit bereit eine View aus dem Viewmodel zu schließen
    /// </summary>
    public interface IRequestCloseViewModel
    {
        /// <summary>
        /// Soll das Fenster geschlossen werden
        /// </summary>
        event EventHandler RequestClose;
    }
}
