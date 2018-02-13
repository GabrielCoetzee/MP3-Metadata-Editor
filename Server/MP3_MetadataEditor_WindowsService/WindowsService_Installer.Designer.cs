namespace MP3_MetadataEditor_WindowsService
{
    partial class WindowsService_Installer
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
                components.Dispose();
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
            this.MP3MetadataEditorWindowsServiceInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.MP3MetadataEditorWindowsService = new System.ServiceProcess.ServiceInstaller();
            // 
            // MP3MetadataEditorWindowsServiceInstaller
            // 
            this.MP3MetadataEditorWindowsServiceInstaller.Password = null;
            this.MP3MetadataEditorWindowsServiceInstaller.Username = null;
            // 
            // MP3MetadataEditorWindowsService
            // 
            this.MP3MetadataEditorWindowsService.ServiceName = "MP3MetadataEditorWindowsService";
            // 
            // WindowsService_Installer
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.MP3MetadataEditorWindowsServiceInstaller,
            this.MP3MetadataEditorWindowsService});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller MP3MetadataEditorWindowsServiceInstaller;
        private System.ServiceProcess.ServiceInstaller MP3MetadataEditorWindowsService;
    }
}