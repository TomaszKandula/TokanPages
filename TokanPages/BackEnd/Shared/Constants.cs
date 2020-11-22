namespace TokanPages.BackEnd.Shared
{

    /// <summary>
    /// This class is responsible only for providing constants to all classes/methods etc. accross the application.
    /// It can be a partial class if necessary; and if so, then put the module in the root folder and additional 
    /// partials in other project folders.
    /// </summary>    
    public static class Constants
    {

        public static class Emails 
        {

            public static class Templates
            {
                public const string Newsletter  = "/content/templates/newsletter.html";
                public const string ContactForm = "/content/templates/contactform.html";
            }

            public static class Addresses 
            {
                public const string Mailer   = "mailer@tomkandula.com";
                public const string Contact  = "contact@tomkandula.com";
                public const string Personal = "tom@tomkandula.com";
                public const string Admin    = "admin@tomkandula.com";
                public const string Private  = "tomasz.kandula@gmail.com";
            }

        }

        public static class Errors 
        {

            internal class EmptyList
            {
                public const string ErrorCode = "empty_list";
                public const string ErrorDesc = "There are no items on the list.";
            }

            internal class NoSuchItem
            {
                public const string ErrorCode = "no_such_item";
                public const string ErrorDesc = "There is no such item.";
            }

            internal class UnableToPost
            {
                public const string ErrorCode = "unable_to_post";
                public const string ErrorDesc = "Cannot insert data into database container.";
            }

            internal class UnableToModify
            {
                public const string ErrorCode = "unable_to_modify";
                public const string ErrorDesc = "Cannot update data in database container.";
            }

            internal class UnableToRemove
            {
                public const string ErrorCode = "unable_to_remove";
                public const string ErrorDesc = "Cannot delete data from database container.";
            }

        }

    }

}
