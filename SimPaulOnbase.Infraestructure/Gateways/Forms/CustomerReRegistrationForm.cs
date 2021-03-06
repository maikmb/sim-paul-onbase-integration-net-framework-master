﻿using Hyland.Unity;
using Hyland.Unity.UnityForm;
using SimPaulOnbase.Core.Domain;
using System;

namespace SimPaulOnbase.Infraestructure.Gateways.Forms
{
    public class CustomerReRegistrationForm
    {
        private StoreNewUnityFormProperties OnbaseStore { get; set; }
        private FormTemplate FormTemplate { get; set; }

        public CustomerReRegistrationForm(StoreNewUnityFormProperties onbaseStore, FormTemplate formTemplate)
        {
            OnbaseStore = onbaseStore ?? throw new ArgumentNullException(nameof(onbaseStore));
            FormTemplate = formTemplate ?? throw new ArgumentNullException(nameof(formTemplate));
        }


        public void SuitabilityData(Suitability suitability)
        {
            if (suitability != null)
            {
                if(suitability.Profile != null)
                {
                    OnbaseStore.AddField("caixadetextoID", suitability.Profile.Id);
                    OnbaseStore.AddField("caixadetextoIDProfile", suitability.Profile.SuitabilityProfile.IdProfile);
                    OnbaseStore.AddField("caixadetextoPontuacaoTotal", suitability.Profile.TotalScore);
                    OnbaseStore.AddField("caixadetextoProfile", suitability.Profile.SuitabilityProfile.Profile);
                    OnbaseStore.AddField("caixadetextoScore", suitability.Profile.SuitabilityProfile.Score);
                    OnbaseStore.AddField("caixadetextodemultilinhasDescricao", suitability.Profile.SuitabilityProfile.Description);
                    OnbaseStore.AddField("caixadetextoDataCriacao", suitability.Profile.SuitabilityProfile.Created);
                    OnbaseStore.AddField("caixadetextoDatadeExpiracao", suitability.Profile.SuitabilityProfile.DtExpiration);

                }

                OnbaseStore.AddField("grupodebotãodeopçãoQualSeuObjetivoaoInvestir", suitability.GetSutiabilityAlternativeByQuestionId(3)?.Alternative);
                OnbaseStore.AddField("grupodebotãodeopçãoPorQuantoTempoPretendeInvestir", suitability.GetSutiabilityAlternativeByQuestionId(1)?.Alternative);
                OnbaseStore.AddField("grupodebotãodeopçãoConhecimentoSobreInvestimentos", suitability.GetSutiabilityAlternativeByQuestionId(8)?.Alternative);
                OnbaseStore.AddField("grupodebotãodeopçãoOqueFariaSeTivessePerdaDe10", suitability.GetSutiabilityAlternativeByQuestionId(2)?.Alternative);
                OnbaseStore.AddField("grupodebotãodeopçãoQuantasVezesMovimentaInvestimentos", suitability.GetSutiabilityAlternativeByQuestionId(7)?.Alternative);
                OnbaseStore.AddField("grupodebotãodeopçãoQualOvalorTotaldeInvestimentos", suitability.GetSutiabilityAlternativeByQuestionId(5)?.Alternative);
                OnbaseStore.AddField("grupodebotãodeopçãoRendaMensal", suitability.GetSutiabilityAlternativeByQuestionId(4)?.Alternative);

                if (suitability.HasForManySutiabilityAlternative(6, 17))
                    OnbaseStore.AddField("caixadeseleçãoAcoesFundosCreditoPrivado", suitability.HasForManySutiabilityAlternative(6, 17).ToString());

                if (suitability.HasForManySutiabilityAlternative(6, 16))
                    OnbaseStore.AddField("caixadeseleçãoRendaFixaTesouroCDBPoupanca", suitability.HasForManySutiabilityAlternative(6, 16).ToString());

                if (suitability.HasForManySutiabilityAlternative(6, 18))
                    OnbaseStore.AddField("caixadeseleçãoDerivativos", suitability.HasForManySutiabilityAlternative(6, 18).ToString());
            }
        }
        public void ApplyBasicData(CustomerReRegistration customer)
        {
            OnbaseStore.AddKeyword("mongoId", customer.MongoId);
            OnbaseStore.AddKeyword("CPF", customer.Cpf);
            OnbaseStore.AddKeyword("Nome", customer.Name);
            OnbaseStore.AddKeyword("E-mail", customer.Email);
            OnbaseStore.AddKeyword("Data de Nascimento", customer.BirthDate);
            OnbaseStore.AddKeyword("Nacionalidade", customer.Nationality.ToString());
            OnbaseStore.AddKeyword("Estado onde nasceu", customer.BirthState);
            OnbaseStore.AddKeyword("Cidade onde nasceu", customer.BirthCity);
            OnbaseStore.AddKeyword("Estado Civil", customer.CivilStatus.ToString());
            OnbaseStore.AddKeyword("Nome do Conjuge", customer.SpouseName);
            OnbaseStore.AddKeyword("CPF do Conjuge", customer.SpouseCpf);
            OnbaseStore.AddKeyword("Nome da Mãe", customer.MotherName);            
            OnbaseStore.AddKeyword("Status", customer.OnboardingStep);
            OnbaseStore.AddKeyword("Tipo de Cadastro", "Recadastro");
            
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

        public void ApplyDocument(CustomerTransactionalDocument document)
        {
            if (document != null)
            {
                OnbaseStore.AddKeyword("Tipo de documento de ID", document.DocumentType);
                OnbaseStore.AddKeyword("Numero do Documento", document.DocumentNumber);
                OnbaseStore.AddKeyword("Órgão emissor", document.IssuingBody);
                if (document.EmissionDate.HasValue) OnbaseStore.AddKeyword("Data de Emissão", document.EmissionDate.Value);
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

        public void ApplyDeclarations(Declarations declaration)
        {
            if (declaration != null)
            {
                OnbaseStore.AddField("grupodebotãodeopçãoHasAssessordeInvestimentos", declaration.HasAdvisor);
                OnbaseStore.AddField("grupodebotãodeopçãoPessoaExposta", declaration.PoliticalPerson);
                OnbaseStore.AddField("grupodebotãodeopçãoVoceTrabalhaNaSimpaul", declaration.IsPersonLinkedSimpaul);
                OnbaseStore.AddField("grupodebotãodeopçãoAutorizaumProcurador", declaration.AllowAttorney);
                OnbaseStore.AddField("caixadeseleçãoLieAceito", declaration.AcceptTerms);
                OnbaseStore.AddField("caixadetextoNomedoProcurador", declaration.attorneyName);
                OnbaseStore.AddField("cdes", declaration.attorneyCpf);
            }
        }

        public void ApplyInvestiments(CustomerTransactionalInvestments investments)
        {
            if (investments != null)
            {
                if (investments.TotalAssets != null)
                {
                    OnbaseStore.AddField("caixadetextoTotaldoPatrimonio", investments.TotalAssets);
                }
                else
                {
                    OnbaseStore.AddField("caixadetextoTotaldoPatrimonio", "0");
                }
                OnbaseStore.AddField("caixadetextoRendaMensal", investments.MonthlyIncome);
                OnbaseStore.AddField("caixadetextoTotalemAplicacoesFinanceiras", investments.FinancialInvestments);
                OnbaseStore.AddField("caixadetextoOrigemdosRecursos", investments.ResourcesOrigin);
            }
            
        }

        public void ApplyFatca(CustomerFatca fatca)
        {
            if (fatca != null)
            {
                OnbaseStore.AddField("grupodebotãodeopçãoDeclaraaoGovernoEUA", fatca.UsPerson);
            }
        }
        public void ApplyDocument(CustomerOnboardDocument document)
        {
            if (document != null)
            {
                OnbaseStore.AddKeyword("Tipo de documento de ID", document.DocumentType);
                OnbaseStore.AddKeyword("Numero do Documento", document.DocumentNumber);


                if (document.EmissionDate.HasValue)
                    OnbaseStore.AddKeyword("Data de Emissão", document.EmissionDate.Value);

                OnbaseStore.AddKeyword("Órgão emissor", document.IssuingBody);

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
