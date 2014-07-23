using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ivony.Logs
{
  internal class LoggerWithSource : Logger, ILogMetaProvider
  {


    public LoggerWithSource( Logger logger, LogSource source )
    {
      InnerLogger = logger;
      Source = source;

    }

    public Logger InnerLogger { get; private set; }

    public LogSource Source { get; private set; }




    protected override void WriteLog( LogEntry entry )
    {
      InnerLogger.LogEntry( entry );
    }

    public LogMeta GetLogMeta( LogType type )
    {
      return new LogMeta()
      {
        Source = Source,
        Type = type
      };
    }

  }
}
