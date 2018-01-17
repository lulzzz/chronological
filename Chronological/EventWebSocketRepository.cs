using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chronological.QueryResults.Events;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Chronological
{

    internal interface IEventWebSocketRepository
    {

        Task<IEnumerable<T>> Execute<T>(string query);
    }

;   internal class EventWebSocketRepository : IEventWebSocketRepository
    {
        private readonly IWebSocketRepository _webSocketRepository;

        internal EventWebSocketRepository(IWebSocketRepository webSocketRepository)
        {
            _webSocketRepository = webSocketRepository;
        }

        async Task<IEnumerable<T>> IEventWebSocketRepository.Execute<T>(string query)
        {
            var executionResults = new List<T>();

            var results = await _webSocketRepository.ReadWebSocketResponseAsync(query, "events");

            // According to samples here: https://github.com/Azure-Samples/Azure-Time-Series-Insights/blob/master/C-%20Hello%20World%20App%20Sample/Program.cs
            // Events should combine all results recevied
            var jArray = new JArray(results.SelectMany(x => (JArray)x["events"]));

            var result = results.Last();            

            //var eventQueryResult = 

            //var eventQueryResult = JsonConvert.DeserializeObject<EventQueryResult>(results.First());
            //    return new EventQueryResultToTypeMapper().Map<T>(eventQueryResult);
            
            //TODO: all the necessary concat for event results
            return executionResults;
        }

        

    }
}
