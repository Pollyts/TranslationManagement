using System.Collections.Generic;

namespace TranslationManagement.Api.Models
{
    public class Translator: IEntityDb
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string HourlyRate { get; set; }
        public TranslatorStatus Status { get; set; }
        public string CreditCardNumber { get; set; }

        #region navigation
        public virtual List<TranslationJob> TranslationJobs { get; set; }
        #endregion
    }
}
