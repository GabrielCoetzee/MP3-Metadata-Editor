using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace MP3_MetadataEditor_WindowsService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            #if DEBUG
                Mp3MetadataEditorWindowsService myWindowsService = new Mp3MetadataEditorWindowsService();
                myWindowsService.OnDebug();
                System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
            #else
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new MP3MetadataEditorWindowsService()
                };
                ServiceBase.Run(ServicesToRun);
            #endif

        }
    }
}
