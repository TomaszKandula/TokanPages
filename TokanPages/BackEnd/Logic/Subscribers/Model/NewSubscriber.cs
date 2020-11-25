using TokanPages.BackEnd.Shared.Models;

namespace TokanPages.BackEnd.Logic.Subscribers.Model
{

    public class NewSubscriber
    {

        public string NewId { get; set; }

        public ErrorHandler Error { get; set; }

        public NewSubscriber() 
        {
            Error = new ErrorHandler();
        }

    }

}
