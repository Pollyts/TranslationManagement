using System.Collections.Generic;
using TranslationManagement.Api.Models;

namespace TranslationManagement.Api.Services.Interfaces
{
    public interface ITranslatorService
    {
        IEnumerable<Translator> GetByName(string name);
        void UpdateStatus(int translatorId, TranslatorStatus status);
        void AssignTranslator(int translatorId, int jobId);
        //IEnumerable<Translator> GetAll();
        //Translator Get(int id);
        //int Create(Translator create);
        //void Update(Translator edit);
        //void Delete(int id);
    }
}
