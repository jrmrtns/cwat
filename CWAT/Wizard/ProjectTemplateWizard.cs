using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;
using System.Collections.Generic;

namespace VSIX
{
    public class ProjectTemplateWizard : IWizard
    {
        #region Public Fields

        // Use to communicate $saferootprojectname$ to ChildWizard
        public static Dictionary<string, string> GlobalDictionary =
            new Dictionary<string, string>();

        #endregion Public Fields

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
        }

        // Add global replacement parameters
        public void RunStarted(object automationObject,
        Dictionary<string, string> replacementsDictionary,
        WizardRunKind runKind, object[] customParams)
        {
            // Add custom parameters.
            replacementsDictionary.Add("$saferootprojectname$",
                TemplateWizard.GlobalDictionary["$saferootprojectname$"]);
        }

        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }

        #endregion Public Methods
    }
}