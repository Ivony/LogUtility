using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ivony.Logs
{
  public class LogFilter
  {


    static LogFilter()
    {
      Info = new ServerityBasedFilter( LogType.Info.Serverity, true );
      Warning = new ServerityBasedFilter( LogType.Warning.Serverity, true );
      Error = new ServerityBasedFilter( LogType.Error.Serverity, true );
      Exception = new ServerityBasedFilter( LogType.Exception.Serverity, true );
      FatalError = new ServerityBasedFilter( LogType.FatalError.Serverity, true );

      OnlyException = new LogTypeBasedFilter( LogType.Exception );
    }


    /// <summary>
    /// 指定记录一般性消息以及更严重的消息的日志筛选器
    /// </summary>
    public static ILogFilter Info
    {
      get;
      private set;
    }

    /// <summary>
    /// 指定记录警告消息以及更严重的消息的日志筛选器
    /// </summary>
    public static ILogFilter Warning
    {
      get;
      private set;
    }

    /// <summary>
    /// 指定记录错误消息以及更严重的消息的日志筛选器
    /// </summary>
    public static ILogFilter Error
    {
      get;
      private set;
    }

    /// <summary>
    /// 指定记录异常消息以及更严重的消息的日志筛选器
    /// </summary>
    public static ILogFilter Exception
    {
      get;
      private set;
    }

    /// <summary>
    /// 指定只记录异常消息的日志筛选器
    /// </summary>
    public static ILogFilter OnlyException
    {
      get;
      private set;
    }

    /// <summary>
    /// 指定记录致命错误消息以及更严重的消息的日志筛选器
    /// </summary>
    public static ILogFilter FatalError
    {
      get;
      private set;
    }


    private class ServerityBasedFilter : ILogFilter
    {

      public ServerityBasedFilter( int serverity, bool allowHigher )
      {
        Serverity = serverity;
        AllowHigher = allowHigher;
      }

      public int Serverity
      {
        get;
        private set;
      }


      public bool AllowHigher
      {
        get;
        private set;
      }

      bool ILogFilter.Writable( LogEntry entry )
      {
        var serverity = entry.MetaData.Type.Serverity;

        if ( AllowHigher )
          return serverity >= Serverity;

        else
          return serverity == Serverity;
      }
    }


    private class LogTypeBasedFilter : ILogFilter
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


      bool ILogFilter.Writable( LogEntry entry )
      {
        return LogType.Equals( entry.MetaData.Type );
      }
    }



  }
}
