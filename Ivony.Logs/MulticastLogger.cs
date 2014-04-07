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
  public class MulticastLogger : ILogger
  {



    public MulticastLogger( params ILogger[] loggers ) : this( false, loggers ) { }

    public MulticastLogger( bool throwExceptions, params ILogger[] loggers )
    {
      ThrowExceptions = throwExceptions;
      Loggers = new ReadOnlyCollection<ILogger>( loggers.SelectMany( ExpandMulticast ).ToArray() );
    }

    private IEnumerable<ILogger> ExpandMulticast( ILogger logger )
    {
      var multi = logger as MulticastLogger;
      if ( multi != null )
        return multi.Loggers;

      else
        return new[] { logger };
    }


    public ICollection<ILogger> Loggers { get; private set; }

    public bool ThrowExceptions { get; private set; }




    /*
    public static ILogger operator +( ILogger logger1, ILogger logger2 )
    {
      return new MulticastLogger( logger1, logger2 );
    }
    */

    /// <summary>
    /// 对所有的日志记录器同时写入日志条目
    /// </summary>
    /// <param name="entry">要记录的日志条目</param>
    public void WriteLog( LogEntry entry )
    {

      List<Exception> exceptions = new List<Exception>();

      foreach ( var logger in Loggers )
      {
        try
        {
          logger.WriteLog( entry );
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
