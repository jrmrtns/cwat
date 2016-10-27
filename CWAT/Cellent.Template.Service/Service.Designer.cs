namespace Cellent.Template.Service
{
    partial class Service
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                if (_resourceService != null)
                    _resourceService.Close();

                if (_roleService != null)
                    _roleService.Close();

                if (_userService != null)
                    _userService.Close();

                if (components != null)
                    components.Dispose();

                if (_startup != null)
                    _startup.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            // 
            // Service
            // 
            this.ServiceName = "Service";

        }

        #endregion
    }
}
