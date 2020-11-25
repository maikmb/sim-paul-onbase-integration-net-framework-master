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
                //var sutOnboard = new OnboardRunner();
                //sutOnboard.OnWorkflowScriptExecute(con);

                var sutOnboard = new RegistrationRunner();
                sutOnboard.Approve("01914813006");
                //sutOnboard.OnWorkflowScriptExecute(con);

            }
            finally
            {
                connector.Disconect();
            }

        }
    }
}
