using SimPaulOnbase.Console.Settings;
using SimPaulOnbase.Infraestructure.Gateways;
using System;
using System.Configuration;

namespace SimPaulOnbase.Console
{
    public class Program
    {
        public static void Main()
        {
            var connector = new OnbaseServiceConector(SettingsService.GetOnbaseSettings());
            var con = connector.GetApplication();

            try
            {
                var sutOnboard = new RegistrationRunner();
                sutOnboard.OnWorkflowScriptExecute(con);
                //sutOnboard.Approve("44480822020");


            }
            finally
            {
                connector.Disconect();
            }

        }
    }
}
