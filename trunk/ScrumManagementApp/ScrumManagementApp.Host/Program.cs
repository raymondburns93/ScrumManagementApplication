using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace ScrumManagementApp.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create base address
            Uri baseAddress_user = new Uri("http://localhost:8000/scrummanagementapp/users/");
            Uri baseAddress_project = new Uri("http://localhost:8000/scrummanagementapp/projects/");

            // Create ServiceHost
            ServiceHost userHost = new ServiceHost(typeof(ScrumManagementApp.WebService.Services.UserService), baseAddress_user);
            ServiceHost projectHost = new ServiceHost(typeof(ScrumManagementApp.WebService.Services.ProjectService), baseAddress_project);

            try
            {
                // Add endpoints
                userHost.AddServiceEndpoint(typeof(ScrumManagementApp.WebService.Interfaces.IUserService), new WSHttpBinding(), "UserService");
                projectHost.AddServiceEndpoint(typeof(ScrumManagementApp.WebService.Interfaces.IProjectService), new WSHttpBinding(), "ProjectService");
                // Metadata
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();    
                smb.HttpGetEnabled = true;
                userHost.Description.Behaviors.Add(smb);
                projectHost.Description.Behaviors.Add(smb);

                // Start service
                userHost.Open();
                projectHost.Open();
                Console.WriteLine("Service is running on " + baseAddress_user.ToString() + ".");
                Console.WriteLine("Press <Enter> to kill service.");
                Console.WriteLine();
                Console.ReadLine();

                userHost.Close();
                projectHost.Close();

            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("An exception occurred: {0}", ce.Message);
                userHost.Abort();
                projectHost.Abort();
            }
        }
    }
}
