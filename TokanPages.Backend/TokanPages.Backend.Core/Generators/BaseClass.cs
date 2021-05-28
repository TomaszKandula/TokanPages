using System;

namespace TokanPages.Backend.Core.Generators
{
    public class BaseClass
    {
        protected BaseClass() { }

        protected static Random Random { get; } = new();
    }
}