using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WOM3.Models
{
    public class Mapiranje
    {
        public string Connection { get; set; }
        public string Source { get;set; }

    }
    public class ConnectionMapping<T>
    {
        private readonly Dictionary<T, HashSet<Mapiranje>> _connections =
           new Dictionary<T, HashSet<Mapiranje>>();

        private readonly static ConnectionMapping<string> _connectionss =
               new ConnectionMapping<string>();

        public static ConnectionMapping<string> GetConection
        {
            get { return _connectionss; }
        }

        public int Count
        {
            get
            {
                return _connections.Count;
            }
        }

        public void Add(T key, Mapiranje connectionId)
        {
            lock (_connections)
            {
                HashSet<Mapiranje> connections;
                if (!_connections.TryGetValue(key, out connections))
                {
                    connections = new HashSet<Mapiranje>();
                    _connections.Add(key, connections);
                }

                lock (connections)
                {
                    connections.Add(connectionId);
                }
            }
        }

        public IEnumerable<Mapiranje> GetConnections(T key)
        {
            HashSet<Mapiranje> connections;
            if (_connections.TryGetValue(key, out connections))
            {
                return connections;
            }

            return Enumerable.Empty<Mapiranje>();
        }

        public void Remove(T key, Mapiranje connectionId)
        {
            lock (_connections)
            {
                HashSet<Mapiranje> connections;
                if (!_connections.TryGetValue(key, out connections))
                {
                    return;
                }

                lock (connections)
                {
                    //connections.Remove(connectionId);
                    connections.RemoveWhere(x => x.Connection == connectionId.Connection);

                    if (connections.Count == 0)
                    {
                        _connections.Remove(key);
                    }
                }
            }
        }
    }
}
