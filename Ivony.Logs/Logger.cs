using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ivony.Logs
{
  public abstract class Logger : ILogger
  {

    protected abstract ILogFilter LogFilter { get; }
    protected abstract ILogWriterProvider LogWriterProvider { get; }

    public void WriteLog( LogEntry entry )
    {

      if ( LogWriterProvider == null )
        throw new InvalidOperationException();

      if ( LogFilter == null || LogFilter.Writable( entry ) )
      {

        var writer = LogWriterProvider.GetWriter( entry );

        writer.Write( entry );
      }
    }
  }
}
