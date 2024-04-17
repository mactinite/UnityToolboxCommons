using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mactinite.ToolboxCommons
{
    [System.Serializable]
    public class GenericDictionary
    {
        private Dictionary<string, object> _headers;

        public GenericDictionary()
        {
            _headers = new Dictionary<string, object>();
        }

        public bool TryGetValue<T>(string dataID, out T data) where T : struct
        {
            data = default(T);
            if (_headers.TryGetValue(dataID, out object headerValue))
            {
                data = (T)headerValue;
                return true;
            }
            return false;
        }

        public T? GetHeaderValue<T>(string header) where T : struct
        {
            object headerValue = null;
            if (_headers.TryGetValue(header, out headerValue))
            {
                return (T)headerValue;
            }

            return null;
        }

        public object GetHeaderValue(string header)
        {
            object headerValue = null;
            if (_headers.TryGetValue(header, out headerValue))
            {
                return headerValue;
            }

            return headerValue;
        }

        public void SetHeaderValue(string header, object value)
        {
            if (_headers.ContainsValue(header))
            {
                _headers[header] = value;
            }
            else
            {
                _headers.Add(header, value);
            }
        }
    }
}
