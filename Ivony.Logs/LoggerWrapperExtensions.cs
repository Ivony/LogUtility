using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ivony.Logs
{

  /// <summary>
  /// 定义 Logger 的各种包装辅助方法
  /// </summary>
  public static class LoggerWrapperExtensions
  {

    private class LoggerWithFilter : Logger
    {


      private LogFilter _filter;

      public LoggerWithFilter( Logger logger, LogFilter filter )
      {
        Contract.Assert( logger != null );
        Contract.Assert( filter != null );

        _filter = filter;

        InnerLogger = logger;
      }


      public Logger InnerLogger
      {
        get;
        private set;
      }


      public override void LogEntry( LogEntry entry )
      {
        if ( _filter.Writable( entry ) )
          InnerLogger.LogEntry( entry );
      }
    }




    public static Logger WithFilter( this Logger logger, LogFilter filter )
    {
      if ( logger == null )
        return null;

      if ( filter == null )
        throw new ArgumentNullException( "filter" );

      return new LoggerWithFilter( logger, filter );
    }






    private class LoggerWithSource : Logger
    {


      public LoggerWithSource( Logger logger, LogSource source )
      {
        Contract.Assert( logger != null );
        Contract.Assert( source != null );
        Source = source;
      }


      public LogSource Source { get; private set; }

      public Logger InnerLogger { get; private set; }




      public override void LogEntry( LogEntry entry )
      {
        entry.MetaData.SetMetaData( Source );
        InnerLogger.LogEntry( entry );
      }
    }


    public static Logger WithSource( this Logger logger, LogSource source )
    {
      if ( logger == null )
        return null;

      if ( source == null )
        throw new ArgumentNullException( "source" );

      return new LoggerWithSource( logger, source );

    }

  }
}
