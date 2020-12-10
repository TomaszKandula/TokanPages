using System;
using TokanPages.Backend.Shared.Models;

namespace TokanPages.Logic.Subscribers.Model
{

    public class NewSubscriber
    {

        public Guid NewId { get; set; }

        public ErrorHandler Error { get; set; }

        public NewSubscriber() 
        {
            Error = new ErrorHandler();
        }

    }

}
