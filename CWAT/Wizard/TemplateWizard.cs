using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;
using System.Collections.Generic;

namespace VSIX
{
    public class TemplateWizard : IWizard
    {
        #region Public Fields

        // Use to communicate $saferootprojectname$ to ChildWizard
        public static Dictionary<string, string> GlobalDictionary =
            new Dictionary<string, string>();

        #endregion Public Fields

        #region Private Fields

        private DTE _dte;

        #endregion Private Fields

        #region Public Methods

        public void BeforeOpeningFile(ProjectItem projectItem)
        {
        }

        public void ProjectFinishedGenerating(Project project)
        {
        }

        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
        }

        public void RunFinished()
        {
            _dte.ItemOperations.Navigate("https://github.com/jrmrtns/cwat", vsNavigateOptions.vsNavigateOptionsNewWindow);
        }

        // Add global replacement parameters
        public void RunStarted(object automationObject,
            Dictionary<string, string> replacementsDictionary,
            WizardRunKind runKind, object[] customParams)
        {
            _dte = automationObject as DTE;

            // Place "$saferootprojectname$ in the global dictionary.
            // Copy from $safeprojectname$ passed in my root vstemplate
            GlobalDictionary["$saferootprojectname$"] = replacementsDictionary["$safeprojectname$"];
        }

        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }

        #endregion Public Methods
    }
}