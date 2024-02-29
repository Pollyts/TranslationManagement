using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using System;
using TranslationManagement.Api.Models;
using TranslationManagement.Api.Repositories.Interfaces;

namespace TranslationManagement.Api.Repositories
{
    public class TranslatorRepository: BaseRepository<Translator>, ITranslatorRepository
    {
        public TranslatorRepository(AppDbContext context)
            : base(context)
        {
        }
    }
}
