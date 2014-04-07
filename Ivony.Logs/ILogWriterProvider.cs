using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ivony.Logs
{
  public interface ILogWriterProvider
  {

    ILogWriter GetWriter( LogEntry entry );

  }
}
