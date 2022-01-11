using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTYS_PROJE.Core.LogManager
{
    public class LogManager : ILogManager
    {
        public void logMessage(string message)
        {
            Console.WriteLine(message.ToString());
        }

        public void logEntity<T>(T entity, string messageFirst = null, string messageLast = null)
        {
            if (messageFirst != null) Console.WriteLine(messageFirst);
            foreach (var prop in entity.GetType().GetProperties().Where(y => y.Name != "id"))
            {
                object o = prop.GetValue(entity, null);
                if (o == null) continue;

                object val = o.ToString();
                string name = prop.Name;

                Console.Write(name + ": " + val + " --- ");
            }
            Console.WriteLine("\n");
            if (messageLast != null) Console.WriteLine(messageLast);
        }

        public void logList<T>(List<T> logs, string messageFirst = null, string messageLast = null)
        {
            try
            {
                if (messageFirst != null) Console.WriteLine(messageFirst);
                foreach (var u in logs)
                {
                    foreach (var prop in u.GetType().GetProperties().Where(y => y.Name != "id"))
                    {
                        object o = prop.GetValue(u, null);
                        if (o == null) continue;

                        object val = o.ToString();
                        string name = prop.Name;

                        Console.Write(name + ": " + val + " --- ");
                    }
                    Console.WriteLine("\n");
                }

                if (messageLast != null) Console.WriteLine(messageLast);


            }
            catch (Exception e)
            {

            }
        }
    }
}
