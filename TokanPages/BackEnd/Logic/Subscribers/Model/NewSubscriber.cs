using System;
using TokanPages.BackEnd.Shared.Models;

namespace TokanPages.BackEnd.Logic.Subscribers.Model
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
