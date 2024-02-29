using System.Collections.Generic;

namespace TranslationManagement.Api.ViewModels
{
    public class TranslationJobRequestViewModel
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string OriginalContent { get; set; }
        public int? TranslatorId { get; set; }
    }

    public class TranslationJobResponseViewModel
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Status { get; set; }
        public double Price { get; set; }
        public LinkedEntity Translator { get; set; }
    }
}
