using Hyland.Unity;
using Hyland.Unity.UnityForm;
using SimPaulOnbase.Core.Domain;
using System;

namespace SimPaulOnbase.Infraestructure.Gateways.Forms
{
    public class CustomerTransactionalForm
    {
        private StoreNewUnityFormProperties OnbaseStore { get; set; }
        private FormTemplate FormTemplate { get; set; }

        public CustomerTransactionalForm(StoreNewUnityFormProperties onbaseStore, FormTemplate formTemplate)
        {
            OnbaseStore = onbaseStore ?? throw new ArgumentNullException(nameof(onbaseStore));
            FormTemplate = formTemplate ?? throw new ArgumentNullException(nameof(formTemplate));
        }

        public void ApplyBasicData(CustomerTransactional customer)
        {
            OnbaseStore.AddKeyword("mongoId", customer.MongoId);
            OnbaseStore.AddKeyword("CPF", customer.Document.DocumentNumber);
            OnbaseStore.AddKeyword("Nome", customer.Name);
            OnbaseStore.AddKeyword("E-mail", customer.Email);
            OnbaseStore.AddKeyword("Data de Nascimento", customer.BirthDate);
            OnbaseStore.AddKeyword("Nacionalidade", customer.Nationality.ToString());
            OnbaseStore.AddKeyword("Estado onde nasceu", customer.BirthState);
            OnbaseStore.AddKeyword("Estado Civil", customer.CivilStatus.ToString());
            OnbaseStore.AddKeyword("Nome do Conjuge", customer.SpouseName);
            OnbaseStore.AddKeyword("CPF do Conjuge", customer.SpouseCpf);
            OnbaseStore.AddKeyword("Nome da Mãe", customer.MotherName);
            OnbaseStore.AddKeyword("Tipo de documento de ID", customer.Document.DocumentType);
            OnbaseStore.AddKeyword("Numero do Documento", customer.Document.DocumentNumber);
            OnbaseStore.AddKeyword("Data de Emissão", customer.Document.EmissionDate);
            OnbaseStore.AddKeyword("Órgão emissor", customer.Document.IssuingBody);
            OnbaseStore.AddKeyword("Status", customer.OnboardingStep);
            OnbaseStore.AddField("caixadetextoTipodeCadastro", "Recadastro");
            
            if (customer.Lastmodified.HasValue) OnbaseStore.AddKeyword("Data Ultima Alteracao", customer.Lastmodified.Value);

        }

        public void ApplyWork(CustomerTransactionalWork work)
        {
            if (work != null)
            {
                OnbaseStore.AddField("caixadetextoNomedaEmpresa", work.CompanyName);
                OnbaseStore.AddField("caixadetextoCNPJ", work.Cnpj);
                OnbaseStore.AddField("caixadetextoEnderecoComercial", work.CompanyAddress);
                OnbaseStore.AddField("grupodebotãodeopçãoEstaTrabalhando", work.HasJob.ToString());
                OnbaseStore.AddField("caixadetextoOcupacao", work.Occupation);
            }
        }

        public void ApplyAddress(CustomerTransactionalAddress[] addresses)
        {
            if (addresses.Length > 0)
            {
                var address = addresses[0];

                OnbaseStore.AddField("caixadetextoCEP", address.ZipCode);
                OnbaseStore.AddField("caixadetextoLogradouro", address.Address);
                OnbaseStore.AddField("caixadetextoNumero", address.Number);
                OnbaseStore.AddField("caixadetextoBairro", address.Neighborhood);
                OnbaseStore.AddField("caixadetextoComplemento", address.Complement);
                OnbaseStore.AddField("caixadetextoEstado", address.State);
                OnbaseStore.AddField("caixadetextoCidade", address.City);
            }
        }


        public void ApplyAccounts(CustomerTransactionalAccount[] accounts)
        {
            if (accounts.Length > 0)
            {
                var account = accounts[0];

                OnbaseStore.AddField("selecionarlistaBanco", account.BankName);
                OnbaseStore.AddField("caixadetextoAgencia", account.Agency);
                OnbaseStore.AddField("caixadetextoNumerodaConta", account.Account);
                OnbaseStore.AddField("caixadetextoDigito", account.AccountDigit);
                OnbaseStore.AddField("caixadetextoTipodeConta", account.AccountType);
                OnbaseStore.AddField("grupodebotãodeopçãoContaConjunta", account.JointAccount.ToString());

                if (accounts.Length > 1)
                {
                    RepeaterDefinition contactRepeaterDefinition = FormTemplate.AllFieldDefinitions.RepeaterDefinitions.Find("seçãoderepetiçãoDadosBancarios");

                    for (int i = 1; i < accounts.Length; i++)
                    {
                        var accountItem = accounts[i];

                        EditableRepeaterItem repeater = contactRepeaterDefinition.CreateEditableRepeaterItem();
                        repeater.SetFieldValue("caixadetextoMultiAgencia", accountItem.Agency);
                        repeater.SetFieldValue("caixadetextoMultiNumerodaConta", accountItem.Account);
                        repeater.SetFieldValue("caixadetextoMultiDigito", accountItem.AccountDigit);
                        repeater.SetFieldValue("caixadetextoMultiBanco", accountItem.BankName);
                        repeater.SetFieldValue("caixadetextoMultiTipodeConta", accountItem.AccountType);
                        OnbaseStore.AddRepeaterItem(repeater);
                    }

                }
            }
        }
    }
}
