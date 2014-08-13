using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ivony.Logs
{
  internal class LoggerWithSource : PipedLogger, ILogMetaProvider
  {


    public LoggerWithSource( Logger logger, LogSource source )
      : base( logger )
    {
      Source = source;
    }


    public LogSource Source { get; private set; }




    protected override void WriteLog( LogEntry entry ) { }



    public LogMeta GetLogMeta( LogType type = null, LogSource source = null )
    {
      return new LogMeta()
      {
        Source = Source,
        Type = type
      };
    }
  }
}
