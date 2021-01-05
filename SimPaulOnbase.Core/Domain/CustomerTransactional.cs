using System;
using System.Linq;

namespace SimPaulOnbase.Core.Domain
{
    public partial class CustomerTransactional
    {
        public string MongoId { get; set; }
        public string OnboardingStep { get; set; }
        public string Cpf { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Nationality { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthCountry { get; set; }
        public string BirthCity { get; set; }
        public string BirthState { get; set; }
        public string MotherName { get; set; }
        public string CivilStatus { get; set; }
        public string SpouseName { get; set; }
        public string SpouseCpf { get; set; }
        public string Status { get; set; }
        public string SinacorAccount { get; set; }
        public CustomerTransactionalWork Work { get; set; }
        public CustomerTransactionalAddress[] Addresses { get; set; }
        public CustomerTransactionalInvestments Investments { get; set; }
        public CustomerTransactionalDocument Document { get; set; }
        public CustomerTransactionalAccount[] Accounts { get; set; }
        
        public CustomerTransactionalSuitability Suitability { get; set; }
        public CustomerFatca Fatca { get; set; }
        public Declarations Declarations { get; set; }
        public DateTime? Lastmodified { get; set; }
    }

    public partial class CustomerTransactionalAccount
    {
        public string AccountType { get; set; }
        public string Bank { get; set; }
        public string BankName { get; set;  }
        public string  Agency { get; set; }
        public string Account { get; set; }
        public string AccountDigit { get; set; }
        public bool JointAccount { get; set; }
    }

    public partial class CustomerTransactionalInvestments
    {
        public string TotalAssets { get; set; }
        public string MonthlyIncome { get; set; }
        public string FinancialInvestments { get; set; }
        public string ResourcesOrigin { get; set; }
    }

    public partial class CustomerTransactionalAddress
    {
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public bool MainAddress { get; set; }
    }

    

    public partial class CustomerTransactionalDocument
    {
        public string DocumentType { get; set; }
        public String DocumentNumber { get; set; }

        public DateTime? EmissionDate { get; set; }
        public string IssuingBody { get; set; }
    }

    public partial class CustomerTransactionalWork
    {
        public bool HasJob { get; set; }
        public string Occupation { get; set; }
        public string CompanyName { get; set; }
        public string Cnpj { get; set; }
        public string CompanyAddress { get; set; }
    }
    public partial class CustomerTransactionalSuitability
    {
        public Profile Profile { get; set; }
        public Answer[] Answers { get; set; }

        public SuitabilityAlternative GetSutiabilityAlternativeByQuestionId(long questionId)
        {
            try
            {
                
                var suitabilityAlternative = this.Answers.First(args => args.QuestionAlternative?.SuitabilityQuestion?.Id == questionId);
                return suitabilityAlternative.QuestionAlternative.SuitabilityAlternative;
            }
            catch
            {
                return null;
            }
        }

        public bool HasForManySutiabilityAlternative(long questionId, long suitabilityAnternative)
        {
            var suitabilityAlternative = this.Answers.Any(args => args.QuestionAlternative?.SuitabilityQuestion?.Id == questionId &&
                args.QuestionAlternative?.SuitabilityAlternative?.Id == suitabilityAnternative);
            return suitabilityAlternative;
        }

        public SuitabilityAlternative GetForManySutiabilityAlternativeByQuestionId(long questionId, long suitabilityAnternative)
        {
            try
            {
                var suitabilityAlternative = this.Answers.First(args => args.QuestionAlternative?.SuitabilityQuestion?.Id == questionId &&
                    args.QuestionAlternative?.SuitabilityAlternative?.Id == suitabilityAnternative);

                return suitabilityAlternative.QuestionAlternative.SuitabilityAlternative;
            }
            catch
            {
                throw new Exception("Question not found");
            }
        }
    }
}
