using System.Collections.Concurrent;
using System.Collections.Generic;

namespace RestaurantApp.API.Jwt.TokenStorage
{
    public class InMemoryTokenStorage : ITokenStorage
    {
        private static ConcurrentDictionary<string, bool> Tokens { get; }

        static InMemoryTokenStorage()
        {
            Tokens = new ConcurrentDictionary<string, bool>();
        }

        public void AddToken(string id)
        {
            Tokens.TryAdd(id, true);
        }

        public bool TokenExists(string id)
        {
            bool exists = Tokens.ContainsKey(id);

            if (!exists)
            {
                return false;
            }

            //bool value = false;
            //return value;
            return Tokens[id];
            //Tokens.TryGetValue(id, out value);
        }

        public void InvalidateToken(string id)
        {
            bool value = false;
            Tokens.Remove(id, out value);
        }
    }
}
