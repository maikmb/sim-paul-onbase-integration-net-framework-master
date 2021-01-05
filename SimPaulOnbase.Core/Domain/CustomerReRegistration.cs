using System;
using System.Linq;

namespace SimPaulOnbase.Core.Domain
{
    public partial class CustomerReRegistration
    {
        public string MongoId { get; set; }
        public string IdClient { get; set; }
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

        public CustomerFatca Fatca { get; set; }
        public Declarations Declarations { get; set; }
        public DateTime? Lastmodified { get; set; }
        public Profile SuitabilityClientProfile { get; set; }
        public SinacorAccount[] SinacorAccounts { get; set; }

    }
}
