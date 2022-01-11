using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTYS_PROJE.Core.LogManager
{
    public interface ILogManager
    {
        public void logMessage(string message);
        public void logEntity<T>(T entity, string messageFirst = null, string messageLast = null);
        public void logList<T>(List<T> logList, string messageFirst = null, string messageLast = null);
    }
}
