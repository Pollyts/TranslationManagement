﻿namespace TranslationManagement.Api.Models
{
    public class TranslationJob: IEntityDb
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public JobStatus Status { get; set; }
        public string OriginalContent { get; set; }
        public string TranslatedContent { get; set; }
        public double Price { get; set; }

        #region navigation
        public int? TranslatorId { get; set; }
        public virtual Translator Translator { get; set; }

        #endregion
    }
}
