using SimPaulOnbase.Infraestructure.Gateways;
using System;
using System.Configuration;

namespace SimPaulOnbase.Console
{
    public class Program
    {
        public static void Main()
        {
            var connector = new OnbaseServiceConector(GetOnbaseSettings());
            var con = connector.GetApplication();

            try
            {
                //var sutOnboard = new OnboardRunner();
                //sutOnboard.OnWorkflowScriptExecute(con);

                var sutOnboard = new RegistrationRunner();                
                sutOnboard.OnWorkflowScriptExecute(con);

            }
            finally
            {
                connector.Disconect();
            }

        }

        private static OnbaseSettings GetOnbaseSettings()
        {
            OnbaseSettings onbaseSettings = new OnbaseSettings();
            onbaseSettings.FormIntegrationID = Convert.ToInt32(ConfigurationManager.AppSettings.Get("OnbaseSettings:FormIntegrationID"));
            onbaseSettings.AppServerURL = ConfigurationManager.AppSettings.Get("OnbaseSettings:AppServerURL");
            onbaseSettings.Username = ConfigurationManager.AppSettings.Get("OnbaseSettings:Username");
            onbaseSettings.Password = ConfigurationManager.AppSettings.Get("OnbaseSettings:Password");
            onbaseSettings.DataSource = ConfigurationManager.AppSettings.Get("OnbaseSettings:DataSource");
            onbaseSettings.CustomerDocumentType = ConfigurationManager.AppSettings.Get("OnbaseSettings:CustomerDocumentType");
            onbaseSettings.CustomerDocumentFileType = ConfigurationManager.AppSettings.Get("OnbaseSettings:CustomerDocumentFileType");
            return onbaseSettings;
        }
    }
}
