using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogUtility
{
  public class ConsoleLogWriter : ILogWriter
  {
    public void Write( LogEntry entry )
    {
      Console.WriteLine( entry.Message );
    }
  }
}
