namespace BookStore.Common.Exceptions
{
    public static class ExceptionErrorCode
    {
        // Client
        public const int TechnicalError = 7000000;

        // WebApi
        public const int InternalWebApiClientError = 6000000;

        // Server
        public const int InternalServerError = 5000000;
        public const int AuthenticationError = 5010001;
    }
}
