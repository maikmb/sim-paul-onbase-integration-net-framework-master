using System;

namespace SimPaulOnbase.Core.Domain
{
    public partial class CustomerOnboard
    {
        public long Id { get; set; }
        public string BirthCity { get; set; }
        public string BirthState { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string Name { get; set; }
        public string SpouseName { get; set; }
        public string SpouseCpf { get; set; }
        public string BirthCountry { get; set; }
        public string RepresentType { get; set; }
        public string RepresentCpf { get; set; }
        public string RepresentName { get; set; }
        public long? Nationality { get; set; }
        public DateTime? BirthDate { get; set; }
        public long? CivilStatus { get; set; }
        public string Schooling { get; set; }
        public string Gender { get; set; }
        public DateTime? Created { get; set; }
        public string Nickname { get; set; }
        public string DtModified { get; set; }
        public CustomerOnboardStatus ClientStatus { get; set; }
        public User User { get; set; }
        public CustomerOnboardAdvisor Advisor { get; set; }
        public CustomerOnboardWork Work { get; set; }
        public CustomerOnboardAddress[] Addresses { get; set; }
        public CustomerOnboardAccount[] Accounts { get; set; }
        public CustomerOnboardDocument Document { get; set; }
        public CustomerOnboardContacts Contacts { get; set; }
        public Profile SuitabilityClientProfile { get; set; }
        public SinacorAccount[] SinacorAccounts { get; set; }
        public Declarations declarations { get; set; }
        public Investments investments { get; set; }
        public CustomerFatca Fatca { get; set; }
    }

    public partial class CustomerOnboardAccount
    {
        public long? Id { get; set; }
        public string Account { get; set; }
        public string AccountDigit { get; set; }
        public string AccountType { get; set; }
        public string Agency { get; set; }
        public string AgencyDigit { get; set; }
        public bool? JointAccount { get; set; }
        public string CoOwnerName { get; set; }
        public string CoOwnerCpf { get; set; }
        public bool? MainAccount { get; set; }
        public DateTime? Created { get; set; }
        public long? Bank { get; set; }
        public string BankName { get; set; }
    }

    public partial class CustomerOnboardAddress
    {
        public string Id { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string Neighborhood { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public bool? MainAddress { get; set; }
    }

    public partial class CustomerOnboardAdvisor
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string CpfCnpj { get; set; }
        public DateTime? Birthday { get; set; }
        public long? IdRoleAdvisor { get; set; }
        public long? ParentIdAdvisor { get; set; }
    }

    public partial class CustomerOnboardStatus
    {
        public long? Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public partial class CustomerOnboardContacts
    {
        public long? Id { get; set; }
        public long? MainAreaCode { get; set; }
        public long? MainPhone { get; set; }
        public string MainPhoneType { get; set; }
        public long? SecondaryAreaCode { get; set; }
        public long? SecondaryPhone { get; set; }
        public string SecondaryPhoneType { get; set; }
        public DateTime? Created { get; set; }
    }

    public partial class CustomerOnboardDocument
    {
        public long? Id { get; set; }
        public string DocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime? EmissionDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string IssuingBody { get; set; }
        public string StateIssuer { get; set; }
        public DateTime? Created { get; set; }
    }

    public partial class SinacorAccount
    {
        public long? AccountCode { get; set; }
        public bool? FlDefault { get; set; }
    }

    public partial class User
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string PhoneNumber { get; set; }
    }

    public partial class CustomerOnboardWork
    {
        public long? Id { get; set; }
        public string Occupation { get; set; }
        public string Profession { get; set; }
        public string Position { get; set; }
        public string CompanyName { get; set; }
        public string ProfessionDescription { get; set; }
        public string HasJob { get; set; }
        public string Cnpj { get; set; }
        public string CompanyAddress { get; set; }
        public DateTime? Created { get; set; }
    }
    
    public partial class Investments
    {
        public string TotalAssets { get; set; }
        public string MonthlyIncome { get; set; }
        public string FinancialInvestments { get; set; }
        public ResourcesOrigin ResourcesOrigin { get; set; }
    }

    public partial class ResourcesOrigin
    {
        public string description { get; set; }
    }

    public partial class CustomerFatca
    {
        public string UsPerson { get; set; }
        public string Ssn { get; set; }
    }

}