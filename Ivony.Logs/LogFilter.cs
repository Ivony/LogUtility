using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ivony.Logs
{
  public abstract class LogFilter
  {


    /// <summary>
    /// 确定指定的日志条目是否需要记录
    /// </summary>
    /// <param name="entry">日志条目</param>
    /// <returns>是否需要记录</returns>
    public abstract bool Writable( LogEntry entry );




    public static LogFilter operator +( LogFilter filter1, LogFilter filter2 )
    {
      var unionFilter1 = filter1 as UnionFilter;
      var unionFilter2 = filter2 as UnionFilter;

      var filters = new List<LogFilter>();

      if ( unionFilter1 != null )
        filters.AddRange( unionFilter1.Filters );
      else
        filters.Add( filter1 );

      if ( unionFilter2 != null )
        filters.AddRange( unionFilter2.Filters );
      else
        filters.Add( filter2 );

      return new UnionFilter( filters );
    }


    private class UnionFilter : LogFilter
    {


      public UnionFilter( IEnumerable<LogFilter> filters )
      {
        Filters = filters.ToArray();
      }


      public LogFilter[] Filters
      {
        get;
        private set;
      }


      public override bool Writable( LogEntry entry )
      {
        return Filters.Any( filter => filter.Writable( entry ) );
      }
    }






    static LogFilter()
    {
      Info = new ServerityBasedFilter( LogType.Info.Serverity, int.MaxValue );
      Warning = new ServerityBasedFilter( LogType.Warning.Serverity, int.MaxValue );
      Error = new ServerityBasedFilter( LogType.Error.Serverity, int.MaxValue );
      Exception = new ServerityBasedFilter( LogType.Exception.Serverity, int.MaxValue );
      FatalError = new ServerityBasedFilter( LogType.FatalError.Serverity, int.MaxValue );

      InfoOnly = new LogTypeBasedFilter( LogType.Info );
      WarningOnly = new LogTypeBasedFilter( LogType.Warning );
      ErrorOnly = new LogTypeBasedFilter( LogType.Error );
      ExceptionOnly = new LogTypeBasedFilter( LogType.Exception );
    }


    /// <summary>
    /// 指定记录一般性消息以及更严重的消息的日志筛选器
    /// </summary>
    public static LogFilter Info
    {
      get;
      private set;
    }

    /// <summary>
    /// 指定只记录一般性消息的日志筛选器
    /// </summary>
    public static LogFilter InfoOnly
    {
      get;
      private set;
    }

    /// <summary>
    /// 指定记录警告消息以及更严重的消息的日志筛选器
    /// </summary>
    public static LogFilter Warning
    {
      get;
      private set;
    }

    /// <summary>
    /// 指定只记录警告消息的日志筛选器
    /// </summary>
    public static LogFilter WarningOnly
    {
      get;
      private set;
    }

    /// <summary>
    /// 指定记录错误消息以及更严重的消息的日志筛选器
    /// </summary>
    public static LogFilter Error
    {
      get;
      private set;
    }

    /// <summary>
    /// 指定只记录错误消息的日志筛选器
    /// </summary>
    public static LogFilter ErrorOnly
    {
      get;
      private set;
    }

    /// <summary>
    /// 指定记录异常消息以及更严重的消息的日志筛选器
    /// </summary>
    public static LogFilter Exception
    {
      get;
      private set;
    }

    /// <summary>
    /// 指定只记录异常消息的日志筛选器
    /// </summary>
    public static LogFilter ExceptionOnly
    {
      get;
      private set;
    }

    /// <summary>
    /// 指定记录致命错误消息以及更严重的消息的日志筛选器
    /// </summary>
    public static LogFilter FatalError
    {
      get;
      private set;
    }


    private class ServerityBasedFilter : LogFilter
    {

      public ServerityBasedFilter( int minServerity, int maxServerity )
      {
        MinServerity = minServerity;
        MaxServerity = maxServerity;
      }

      public int MinServerity
      {
        get;
        private set;
      }

      public int MaxServerity
      {
        get;
        private set;
      }


      public override bool Writable( LogEntry entry )
      {
        var serverity = entry.MetaData.Type.Serverity;

        return serverity >= MinServerity && serverity < MaxServerity;

      }
    }


    private class LogTypeBasedFilter : LogFilter
    {
      public LogTypeBasedFilter( LogType type )
      {
        LogType = type;
      }


      public LogType LogType
      {
        get;
        private set;
      }


      public override bool Writable( LogEntry entry )
      {
        return LogType.Equals( entry.MetaData.Type );
      }
    }



  }
}
