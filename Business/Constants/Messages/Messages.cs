using Entities.Concrete;
using Entities.Concrete.Dtos;

namespace Business.Constants.Messages
{
    public static class Messages
    {
        public static class ProductMessages
        {
            public static class SuccessMessages
            {
                public const string ProductsListed = "Products was listed";
                public const string ProductListed = "Product was listed";
                public const string ProductAdded = "Product was added";
                public const string ProductUpdated = "Product was updated";
                public const string ProductDeleted = "Product was deleted";

                public const string ProductDetailsListed = "Product details was listed";
                public const string ProductDetailListed = "Product detail was listed";
            }

            public static class ErrorMessages
            {
                public const string CategoryLimitExceeded = "Product counts can not exceed 10 in a category";
                public const string ProductNameAlreadyExists = "Product name already exists";
                public const string TotalCategoryCountExceeded =
                    "You can not add product because total category count is exceeded";
            }
        }

        public static class CategoryMessages
        {
            public static class SuccessMessages
            {
                public const string CategoriesListed = "Categories was listed";
            }

            public static class ErrorMessages
            {
            }
        }

        public static class AuthMessages
        {
            public static class SuccessMessages
            {
                public const string UserRegistered = "User registered successfully";
                public const string SuccessfulLogin = "Login is successful";
                public const string AccessTokenCreated = "Access token was created";
            }

            public static class ErrorMessages
            {
                public const string AuthorizationDenied = "Authorization denied. You have no permission on this operation";
                public const string UserNotFound = "User was not found";
                public const string InvalidPassword = "Your password is invalid";
                public const string UserAlreadyExists = "User already exists";
            }
        }

        public static class SystemMessages
        {
            public const string MaintenanceTime = "System under maintenance";
        }
    }
}