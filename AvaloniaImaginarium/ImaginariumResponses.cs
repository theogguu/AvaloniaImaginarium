using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaImaginarium
{
    public static class ImaginariumResponses
    {
        public static List<string> Responses { get; private set; } = new List<string>();

        public static void AddResponses(IEnumerable<string> responses)
        {
            Responses.AddRange(responses);
        }

        public static void AddSingleResponse(string response)
        {
            Responses.Add(response);
        }

        public static List<string> GetResponses()
        {
            return new List<string>(Responses); // Return a copy to prevent external modification
        }

        public static void Clear()
        {
            Responses.Clear();
        }
    }
}
