using System;

namespace TranslationManagement.Api.Extentions
{
    public class ClientException : Exception
    {
        public ClientException()
        {
        }

        public ClientException(string message)
            : base(message)
        {
        }
    }
}
