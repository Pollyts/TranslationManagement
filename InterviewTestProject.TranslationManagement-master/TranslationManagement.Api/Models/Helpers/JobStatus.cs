using System.ComponentModel;

namespace TranslationManagement.Api.Models
{
    public enum JobStatus
    {
        [Description("New")]
        New,
        [Description("InProgress")]
        InProgress,
        [Description("Completed")]
        Completed,
    }
}
