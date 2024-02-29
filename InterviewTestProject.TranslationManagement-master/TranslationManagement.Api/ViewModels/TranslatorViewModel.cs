using System.Collections.Generic;
using TranslationManagement.Api.Models;

namespace TranslationManagement.Api.ViewModels
{
    public class TranslatorRequestViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string HourlyRate { get; set; }
        public TranslatorStatus Status { get; set; }
        public string CreditCardNumber { get; set; }

    }

    public class TranslatorResponseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string HourlyRate { get; set; }
        public TranslatorStatus Status { get; set; }
        public string CreditCardNumber { get; set; }
        public List<LinkedEntity> Jobs { get; set; }
    }
}
