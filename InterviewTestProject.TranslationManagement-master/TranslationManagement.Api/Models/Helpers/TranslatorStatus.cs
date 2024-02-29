using System.ComponentModel;

namespace TranslationManagement.Api.Models
{
    public enum TranslatorStatus
    {
        [Description("Applicant")]
        Applicant,
        [Description("Certified")]
        Certified,
        [Description("Deleted")]
        Deleted,
    }
}
