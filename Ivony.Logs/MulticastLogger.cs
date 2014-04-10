using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Ivony.Logs
{

  /// <summary>
  /// 多播日志记录器
  /// </summary>
  public class MulticastLogger : Logger
  {



    public MulticastLogger( params Logger[] loggers ) : this( true, loggers ) { }

    public MulticastLogger( bool throwExceptions, params Logger[] loggers )
    {
      ThrowExceptions = throwExceptions;
      Loggers = new ReadOnlyCollection<Logger>( loggers.SelectMany( ExpandMulticast ).ToArray() );
    }

    private IEnumerable<Logger> ExpandMulticast( Logger logger )
    {
      var multi = logger as MulticastLogger;
      if ( multi != null )
        return multi.Loggers;

      else
        return new[] { logger };
    }


    public ICollection<Logger> Loggers { get; private set; }

    public bool ThrowExceptions { get; private set; }




    /// <summary>
    /// 对所有的日志记录器同时写入日志条目
    /// </summary>
    /// <param name="entry">要记录的日志条目</param>
    protected override void WriteLog( LogEntry entry )
    {

      List<Exception> exceptions = new List<Exception>();

      foreach ( var logger in Loggers )
      {
        try
        {
          logger.LogEntry( entry );
        }
        catch ( Exception e )
        {

          if ( ThrowExceptions )
            exceptions.Add( e );
        }
      }

      if ( exceptions.Any() )
        throw new AggregateException( exceptions.ToArray() );
    }
  }
}
