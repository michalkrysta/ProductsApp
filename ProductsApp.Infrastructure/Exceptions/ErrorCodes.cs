namespace ProductsApp.Infrastructure.Exceptions
{
    public static class ErrorCodes
    {
        public static string ProductNotFound => "product_not_found";
        public static string ProductNotExists => "product_not_exists";
        public static string ProductAlreadyExists => "product_already_exists";
    }
}