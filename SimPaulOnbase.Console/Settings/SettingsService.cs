using SimPaulOnbase.Infraestructure.ApiDataAccess;
using SimPaulOnbase.Infraestructure.Gateways;
using System;
using System.Configuration;

namespace SimPaulOnbase.Console.Settings
{
    public class SettingsService
    {
        public static CustomerApiSettings GetApiSettings()
        {
            CustomerApiSettings apiSettings = new CustomerApiSettings
            {
                BaseUrl = "https://stg-gapi.simpaul.com.br/",
                IncompletedResource = "stg/backoffice/client/incomplete",
                ApproveResource = "stg/backoffice/client/sinacor",
                ReproveResource = "stg/backoffice/client/{id}/status",
                CustomerResource = "stg/backoffice/client/onboarding",
                LoginResource = "stg/backoffice/authentication",
                SuitabilityResource = "stg/backoffice/client/{id}/suitability",
                RegisterAginResource = "stg/backoffice/client/reregister",
                UserLogin = "07915143743",
                PasswordLogin = "kZzoYoF+6sxNy/TaVFq603QclLvKlf5/13zhpj3kKEo="
            };

            return apiSettings;
        }

        public static OnbaseSettings GetOnbaseSettings()
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
