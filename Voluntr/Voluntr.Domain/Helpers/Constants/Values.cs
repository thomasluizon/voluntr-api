namespace Voluntr.Domain.Helpers.Constants
{
    public static class Values
    {
        public static class Message
        {
            public const string Unauthorized = "Você não tem permissão para executar esta ação";
            public const string DefaultError = "Nossos servidores estão indisponíveis no momento. Por favor, tente mais tarde.";
            public const string InvalidGuid = "O parâmetro informado não é válido, por favor informe um valor de padrão UUID";
            public const string UserRequestNotFound = "O usuário informado na requisição não foi encontrado, realize o login novamente";
        }

        public static class BlobPath
        {
        }

        public static class CacheKey
        {
            public const string DeactivatedTokens = "tokens:deactivated:{0}";
        }
    }
}
