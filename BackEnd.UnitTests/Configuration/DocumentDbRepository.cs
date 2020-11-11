using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Azure.Documents.Client;

namespace BackEnd.UnitTests.Configuration
{

    public class DocumentDbRepository<T> where T : class
    {

        private readonly IDocumentClient FDocumentClient;

        public DocumentDbRepository(IDocumentClient ADocumentClient)
        {
            FDocumentClient = ADocumentClient;
        }

        public IQueryable<T> GetQueryable(string AQueryString)
        {

            return FDocumentClient.CreateDocumentQuery<T>(
                UriFactory.CreateDocumentCollectionUri(string.Empty, string.Empty),
                new SqlQuerySpec 
                { 
                    QueryText = AQueryString 
                },
                new FeedOptions 
                { 
                    MaxItemCount = -1, 
                    EnableCrossPartitionQuery = true 
                }
            );

        }

        public async Task<IEnumerable<T>> ExecuteQueryAsync(IQueryable<T> AQuery)
        {

            var LDocumentQuery = AQuery.AsDocumentQuery();
            var LResults = new List<T>();

            while (LDocumentQuery.HasMoreResults)
            {
                LResults.AddRange(await LDocumentQuery.ExecuteNextAsync<T>());
            }

            return LResults;
        }

    }

}
