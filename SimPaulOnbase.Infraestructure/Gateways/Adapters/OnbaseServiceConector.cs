using Hyland.Unity;
using System;

namespace SimPaulOnbase.Infraestructure.Gateways
{
    /// <summary>
    /// CustomerOnbaseIntegration class
    /// </summary>
    public class OnbaseServiceConector : IOnbaseConector
    {
        /// <summary>
        /// OnbaseSettings Instance
        /// </summary>
        protected OnbaseSettings _onbaseSettings;

        public OnbaseServiceConector(OnbaseSettings onbaseSettings)
        {
            _onbaseSettings = onbaseSettings;
        }

        public Hyland.Unity.Application GetApplication()
        {
            var authProps = Hyland.Unity.Application.CreateOnBaseAuthenticationProperties(_onbaseSettings.AppServerURL, _onbaseSettings.Username, _onbaseSettings.Password, _onbaseSettings.DataSource);
            authProps.LicenseType = LicenseType.Default;

            try
            {
                Connection = Hyland.Unity.Application.Connect(authProps);
                return Connection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Disconect()
        {
            Connection.Disconnect();
        }

        public Hyland.Unity.Application Connection { get; set; }
    }
}
