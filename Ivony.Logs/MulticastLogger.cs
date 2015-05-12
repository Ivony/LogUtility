using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ivony.Logs
{

  /// <summary>
  /// 多播日志记录器
  /// </summary>
  public class MulticastLogger : AsyncLogger
  {


    /// <summary>
    /// 创建一个多播日志记录器
    /// </summary>
    /// <param name="loggers">需要记录日志的日志记录器</param>
    public MulticastLogger( params Logger[] loggers )
    {
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


    /// <summary>
    /// 需要被转发的日志记录器
    /// </summary>
    public ICollection<Logger> Loggers { get; private set; }




    /// <summary>
    /// 对所有的日志记录器同时写入日志条目
    /// </summary>
    /// <param name="entry">要记录的日志条目</param>
    public override void LogEntry( LogEntry entry )
    {
    }


    /// <summary>
    /// 重写 Dispose 方法，释放所有日志记录器的资源
    /// </summary>
    public override void Dispose()
    {
      foreach ( var logger in Loggers )
      {
        logger.Dispose();
      }
    }

    /// <summary>
    /// 异步执行日志记录
    /// </summary>
    /// <param name="entry">要记录的日志条目</param>
    /// <returns>用于等待异步执行完成的 Task 对象</returns>
    public override Task LogEntryAsync( LogEntry entry )
    {
      return SequentialLogEntry( entry );
    }


    private async Task SequentialLogEntry( LogEntry entry )
    {
      List<Exception> exceptions = new List<Exception>();

      foreach ( var logger in Loggers )
      {
        try
        {
          var asyncLogger = logger as AsyncLogger;

          if ( asyncLogger == null )
            logger.LogEntry( entry );

          else
            await asyncLogger.LogEntryAsync( entry );
        }
        catch ( Exception e )
        {
          exceptions.Add( e );
        }
      }

      if ( exceptions.Any() )
        throw new AggregateException( exceptions.ToArray() );
    }
  }
}
