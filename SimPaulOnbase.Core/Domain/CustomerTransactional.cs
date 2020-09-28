using System;

namespace SimPaulOnbase.Core.Domain
{
    public partial class CustomerTransactional
    {
        public string MongoId { get; set; }
        public string OnboardingStep { get; set; }
        public string Cpf { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public long Nationality { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthCountry { get; set; }
        public string BirthCity { get; set; }
        public string BirthState { get; set; }
        public string MotherName { get; set; }
        public long CivilStatus { get; set; }
        public string SpouseName { get; set; }
        public string SpouseCpf { get; set; }
        public string Status { get; set; }
        public string SinacorAccount { get; set; }
        public CustomerTransactionalWork Work { get; set; }
        public CustomerTransactionalAddress[] Addresses { get; set; }
        public CustomerTransactionalInvestments Investments { get; set; }
        public CustomerTransactionalDocument Document { get; set; }
        public CustomerTransactionalAccount[] Accounts { get; set; }
        public CustomerTransactionalFatca Fatca { get; set; }
        public CustomerTransactionalDeclarations Declarations { get; set; }
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

    public partial class CustomerTransactionalDeclarations
    {
        public bool HasAdvisor { get; set; }
        public bool IsPersonLinkedSimpaul { get; set; }
        public bool PoliticalPerson { get; set; }
        public bool AllowAttorney { get; set; }
        public bool AcceptTerms { get; set; }
    }

    public partial class CustomerTransactionalDocument
    {
        public string DocumentType { get; set; }
        public String DocumentNumber { get; set; }

        public DateTime EmissionDate { get; set; }
        public string IssuingBody { get; set; }
    }

    public partial class CustomerTransactionalFatca
    {
        public bool UsPerson { get; set; }
        public long Ssn { get; set; }
    }

    public partial class CustomerTransactionalInvestments
    {
        public long TotalAssets { get; set; }
        public long MonthlyIncome { get; set; }
        public long FinancialInvestments { get; set; }
        public long ResourcesOrigin { get; set; }
    }

    public partial class CustomerTransactionalWork
    {
        public bool HasJob { get; set; }
        public string Occupation { get; set; }
        public string CompanyName { get; set; }
        public string Cnpj { get; set; }
        public string CompanyAddress { get; set; }
    }
}
