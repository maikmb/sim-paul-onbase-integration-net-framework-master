using Hyland.Unity;
using Hyland.Unity.UnityForm;
using SimPaulOnbase.Core.Gateways;
using SimPaulOnbase.Core.Domain;
using System.Collections.Generic;

namespace SimPaulOnbase.Infraestructure.Gateways
{
    /// <summary>
    /// CustomerOnbaseIntegration class
    /// </summary>
    public class CustomerTransactionalOnbaseService : OnbaseServiceBase, ICustomerTransactionalOnbaseService
    {
        private readonly OnbaseSettings _onbaseSettings;
        private readonly ILogger _logger;

        public CustomerTransactionalOnbaseService(OnbaseSettings onbaseSettings, IOnbaseConector _onbaseConector, ILogger logger) : base(onbaseSettings, _onbaseConector)
        {
            this._onbaseSettings = onbaseSettings;
            this._logger = logger;
        }

        public void Handle(List<CustomerTransactional> divergedRegistrations)
        {
            this.GetConector();

            foreach (var customer in divergedRegistrations)
            {
                IntegrateCustomer(customer);
                this._logger.Info("Integrated customer: " + customer.Name);
            }
        }

        private void IntegrateCustomer(CustomerTransactional customer)
        {
            FormTemplate formTemplate = this.FindFormTemplate(_onbaseSettings.FormIntegrationID);
            StoreNewUnityFormProperties onbaseStore = this.InitNewForm(formTemplate);
            MapCustomerFieldsToOnbase(customer, onbaseStore);

            var onbaseDocument = this.StoreNewUnityForm(onbaseStore);
        }

        private void MapCustomerFieldsToOnbase(CustomerTransactional customer, StoreNewUnityFormProperties onbaseStore)
        {
            if (customer.Document == null)
            {
                return;
            }


            onbaseStore.AddKeyword("mongoId", customer.MongoId);
            onbaseStore.AddKeyword("CPF", customer.Document.DocumentNumber);
            onbaseStore.AddKeyword("Nome", customer.Name);
            onbaseStore.AddKeyword("E-mail", customer.Email);
            onbaseStore.AddKeyword("Data de Nascimento", customer.BirthDate);
            onbaseStore.AddKeyword("Nacionalidade", customer.Nationality.ToString());
            onbaseStore.AddKeyword("Estado onde nasceu", customer.BirthState);
            onbaseStore.AddKeyword("Estado Civil", customer.CivilStatus.ToString());
            onbaseStore.AddKeyword("Nome do Conjuge", customer.SpouseName);
            onbaseStore.AddKeyword("CPF do Conjuge", customer.SpouseCpf);
            onbaseStore.AddKeyword("Nome da Mãe", customer.MotherName);
            onbaseStore.AddKeyword("Tipo de documento de ID", customer.Document.DocumentType);
            onbaseStore.AddKeyword("Numero do Documento", customer.Document.DocumentNumber);
            onbaseStore.AddKeyword("Data de Emissão", customer.Document.EmissionDate);
            onbaseStore.AddKeyword("Órgão emissor", customer.Document.IssuingBody);
            onbaseStore.AddKeyword("Status", customer.OnboardingStep);

            
            if (customer.Addresses.Length > 0)
            {
                var address = customer.Addresses[0];

                onbaseStore.AddField("caixadetextoCEP", address.ZipCode);
                onbaseStore.AddField("caixadetextoLogradouro", address.Address);
                onbaseStore.AddField("caixadetextoNumero", address.Number);
                onbaseStore.AddField("caixadetextoBairro", address.Neighborhood);
                onbaseStore.AddField("caixadetextoComplemento", address.Complement);
                onbaseStore.AddField("caixadetextoEstado", address.State);
                onbaseStore.AddField("caixadetextoCidade", address.City);
            }


            if (customer.Accounts.Length > 0)
            {
                var account = customer.Accounts[0];

                onbaseStore.AddField("selecionarlistaBanco", account.BankName);
                onbaseStore.AddField("caixadetextoAgencia", account.Agency);
                onbaseStore.AddField("caixadetextoNumerodaConta", account.Account);
                onbaseStore.AddField("caixadetextoDigito", account.AccountDigit);
                onbaseStore.AddField("caixadetextoTipodeConta", account.AccountType);

            }

            if(customer.Work != null)
            {
                onbaseStore.AddField("caixadetextoNomedaEmpresa", customer.Work.CompanyName);
                onbaseStore.AddField("caixadetextoCNPJ", customer.Work.Cnpj);
                onbaseStore.AddField("caixadetextoEnderecoComercial", customer.Work.CompanyAddress);
                onbaseStore.AddField("grupodebotãodeopçãoEstaTrabalhando", customer.Work.HasJob.ToString());
                onbaseStore.AddField("caixadetextoOcupacao", customer.Work.Occupation);
            }


            if (customer.Lastmodified.HasValue)
                onbaseStore.AddKeyword("Data Ultima Alteracao", customer.Lastmodified.Value);
        }
    }
}
