using System.Linq;
using Microsoft.Azure.Documents.Linq;

namespace BackEnd.UnitTests.Configuration
{

    public interface IFakeDocumentQuery<T> : IDocumentQuery<T>, IOrderedQueryable<T>
    {
    }

}
