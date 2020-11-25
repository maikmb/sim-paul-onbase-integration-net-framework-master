using Hyland.Unity;
using Hyland.Unity.UnityForm;
using SimPaulOnbase.Core.Gateways;
using SimPaulOnbase.Core.Domain;
using System.Collections.Generic;
using SimPaulOnbase.Core.Repositories;
using System.Threading.Tasks;

namespace SimPaulOnbase.Infraestructure.Gateways
{
    /// <summary>
    /// CustomerOnbaseIntegration class
    /// </summary>
    public class CustomerOnboardOnbaseService : OnbaseServiceBase, ICustomerOnboardOnbaseService
    {
        private readonly OnbaseSettings _onbaseSettings;
        private readonly ILogger _logger;
        private readonly ICustomerRepository _customerRepository;

        public CustomerOnboardOnbaseService(OnbaseSettings onbaseSettings, IOnbaseConector _onbaseConector, ILogger logger, ICustomerRepository customerRepository) : base(onbaseSettings, _onbaseConector)
        {
            this._onbaseSettings = onbaseSettings;
            this._logger = logger;
            this._customerRepository = customerRepository;
        }

        public async Task Handle(List<CustomerOnboard> divergedRegistrations)
        {
            this.GetConector();

            foreach (var customer in divergedRegistrations)
            {
                await IntegrateCustomer(customer);
                this._logger.Info("Integrated customer: " + customer.Name);
            }
        }

        private async Task IntegrateCustomer(CustomerOnboard customer)
        {
            FormTemplate formTemplate = this.FindFormTemplate(_onbaseSettings.FormIntegrationID);
            StoreNewUnityFormProperties onbaseStore = this.InitNewForm(formTemplate);            

            this.StoreCustomer(customer, onbaseStore, formTemplate);
            await this.StoreCustomerSuitability(customer, onbaseStore);

            this.StoreNewUnityForm(onbaseStore);
        }

        private async Task StoreCustomerSuitability(CustomerOnboard customer, StoreNewUnityFormProperties onbaseStore)
        {
            Suitability suitability = await this._customerRepository.GetCustomerSuitability(customer);

            if (suitability != null)
            {
                onbaseStore.AddField("grupodebotãodeopçãoQualSeuObjetivoaoInvestir", suitability.GetSutiabilityAlternativeByQuestionId(3)?.Alternative);
                onbaseStore.AddField("grupodebotãodeopçãoPorQuantoTempoPretendeInvestir", suitability.GetSutiabilityAlternativeByQuestionId(1)?.Alternative);
                onbaseStore.AddField("grupodebotãodeopçãoConhecimentoSobreInvestimentos", suitability.GetSutiabilityAlternativeByQuestionId(8)?.Alternative);
                onbaseStore.AddField("grupodebotãodeopçãoOqueFariaSeTivessePerdaDe10", suitability.GetSutiabilityAlternativeByQuestionId(2)?.Alternative);
                onbaseStore.AddField("grupodebotãodeopçãoQuantasVezesMovimentaInvestimentos", suitability.GetSutiabilityAlternativeByQuestionId(7)?.Alternative);
                onbaseStore.AddField("grupodebotãodeopçãoQualOvalorTotaldeInvestimentos", suitability.GetSutiabilityAlternativeByQuestionId(5)?.Alternative);
                onbaseStore.AddField("grupodebotãodeopçãoRendaMensal", suitability.GetSutiabilityAlternativeByQuestionId(4)?.Alternative);

                if (suitability.HasForManySutiabilityAlternative(6, 17))
                    onbaseStore.AddField("caixadeseleçãoAcoesFundosCreditoPrivado", suitability.HasForManySutiabilityAlternative(6, 17).ToString());

                if (suitability.HasForManySutiabilityAlternative(6, 16))
                    onbaseStore.AddField("caixadeseleçãoRendaFixaTesouroCDBPoupanca", suitability.HasForManySutiabilityAlternative(6, 16).ToString());

                if (suitability.HasForManySutiabilityAlternative(6, 18))
                    onbaseStore.AddField("caixadeseleçãoDerivativos", suitability.HasForManySutiabilityAlternative(6, 18).ToString());
            }
        }

        private void StoreCustomer(CustomerOnboard customer, StoreNewUnityFormProperties onbaseStore, FormTemplate formTemplate)
        {
            onbaseStore.AddKeyword("CPF", customer.Cpf);
            onbaseStore.AddKeyword("Nome", customer.Name);
            onbaseStore.AddKeyword("E-mail", customer.Email);
            onbaseStore.AddKeyword("Celular", customer.User.PhoneNumber.ToString());
            onbaseStore.AddField("caixadetextoTipodeCadastro", "Cadastro");


            if (customer.BirthDate.HasValue)
                onbaseStore.AddKeyword("Data de Nascimento", customer.BirthDate.Value);

            onbaseStore.AddKeyword("Nacionalidade", customer.Nationality.ToString());
            onbaseStore.AddKeyword("Estado onde nasceu", customer.BirthState);
            onbaseStore.AddKeyword("Cidade onde nasceu", customer.BirthCity);
            onbaseStore.AddKeyword("Estado Civil", customer.CivilStatus.ToString());
            onbaseStore.AddKeyword("Nome do Conjuge", customer.SpouseName);
            onbaseStore.AddKeyword("CPF do Conjuge", customer.SpouseCpf);
            onbaseStore.AddKeyword("Nome da Mãe", customer.MotherName);

            if (customer.Document != null)
            {
                onbaseStore.AddKeyword("Tipo de documento de ID", customer.Document.DocumentType);
                onbaseStore.AddKeyword("Numero do Documento", customer.Document.DocumentNumber);


                if (customer.Document.EmissionDate.HasValue)
                    onbaseStore.AddKeyword("Data de Emissão", customer.Document.EmissionDate.Value);

                onbaseStore.AddKeyword("Órgão emissor", customer.Document.IssuingBody);

            }

            if (customer.ClientStatus != null)
            {
                onbaseStore.AddKeyword("Status", customer.ClientStatus.Description);
                onbaseStore.AddKeyword("Status OnBoarding Cadastro", customer.ClientStatus.Code);
            }


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
                onbaseStore.AddField("grupodebotãodeopçãoContaConjunta", account.JointAccount.ToString());

                if (customer.Accounts.Length > 1)
                {
                    RepeaterDefinition contactRepeaterDefinition = formTemplate.AllFieldDefinitions.RepeaterDefinitions.Find("seçãoderepetiçãoDadosBancarios");

                    for (int i = 1; i < customer.Accounts.Length; i++)
                    {
                        var accountItem = customer.Accounts[i];

                        EditableRepeaterItem repeater = contactRepeaterDefinition.CreateEditableRepeaterItem();
                        repeater.SetFieldValue("caixadetextoMultiAgencia", accountItem.Agency);
                        repeater.SetFieldValue("caixadetextoMultiNumerodaConta", accountItem.Account);
                        repeater.SetFieldValue("caixadetextoMultiDigito", accountItem.AccountDigit);
                        repeater.SetFieldValue("caixadetextoMultiBanco", accountItem.BankName);
                        repeater.SetFieldValue("caixadetextoMultiTipodeConta", accountItem.AccountType);
                        onbaseStore.AddRepeaterItem(repeater);
                    }
                    
                }
            }

            if (customer.Work != null)
            {
                onbaseStore.AddField("grupodebotãodeopçãoEstaTrabalhando", customer.Work.HasJob);
                onbaseStore.AddField("caixadetextoNomedaEmpresa", customer.Work.CompanyName);
                onbaseStore.AddField("caixadetextoCNPJ", customer.Work.Cnpj);
                onbaseStore.AddField("caixadetextoEnderecoComercial", customer.Work.CompanyAddress);
                onbaseStore.AddField("caixadetextoOcupacao", customer.Work.Occupation);
            }
            if (customer.SuitabilityClientProfile != null)
            {
                onbaseStore.AddField("caixadetextoScore", customer.SuitabilityClientProfile.SuitabilityProfile.Score.ToString());
                onbaseStore.AddField("caixadetextoProfile", customer.SuitabilityClientProfile.SuitabilityProfile.Profile);
                onbaseStore.AddField("caixadetextoID", customer.SuitabilityClientProfile.Id.ToString());
                onbaseStore.AddField("caixadetextoIDProfile", customer.SuitabilityClientProfile.SuitabilityProfile.IdProfile.ToString());
                onbaseStore.AddField("caixadetextoPontuacaoTotal", customer.SuitabilityClientProfile.TotalScore.ToString());
                onbaseStore.AddField("caixadetextodemultilinhasDescricao", customer.SuitabilityClientProfile.SuitabilityProfile.Description);
                onbaseStore.AddField("caixadetextoDataCriacao", customer.SuitabilityClientProfile.Created.ToString());
                onbaseStore.AddField("caixadetextoDatadeExpiracao", customer.SuitabilityClientProfile.SuitabilityProfile.DtExpiration.ToString());

            }
            if (customer.declarations != null)
            {
                onbaseStore.AddField("grupodebotãodeopçãoHasAssessordeInvestimentos", customer.declarations.HasAdvisor);
                onbaseStore.AddField("grupodebotãodeopçãoPessoaExposta", customer.declarations.PoliticalPerson);
                onbaseStore.AddField("grupodebotãodeopçãoVoceTrabalhaNaSimpaul", customer.declarations.IsPersonLinkedSimpaul);
                onbaseStore.AddField("grupodebotãodeopçãoAutorizaumProcurador", customer.declarations.AllowAttorney);
                onbaseStore.AddField("caixadeseleçãoLieAceito", customer.declarations.AcceptTerms);
                onbaseStore.AddField("caixadetextoNomedoProcurador", customer.declarations.attorneyName);
                onbaseStore.AddField("cdes", customer.declarations.attorneyCpf);
            }
            if (customer.investments != null)
            {
                if (customer.investments.TotalAssets != null)
                {
                    onbaseStore.AddField("caixadetextoTotaldoPatrimonio", customer.investments.TotalAssets);
                }
                else
                {
                    onbaseStore.AddField("caixadetextoTotaldoPatrimonio", "0");
                }
                onbaseStore.AddField("caixadetextoRendaMensal", customer.investments.MonthlyIncome);
                onbaseStore.AddField("caixadetextoOrigemdosRecursos", customer.investments.ResourcesOrigin.description);
                onbaseStore.AddField("caixadetextoTotalemAplicacoesFinanceiras", customer.investments.FinancialInvestments);
            }
            if (customer.Fatca != null)
            {
                onbaseStore.AddField("grupodebotãodeopçãoDeclaraaoGovernoEUA", customer.Fatca.UsPerson);
            }
        }
    }
}