using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ivony.Logs
{

  /// <summary>
  /// 提供记录日志的一系列扩展方法
  /// </summary>
  public static class LogExtensions
  {


    /// <summary>
    /// 记录一个信息性日志
    /// </summary>
    /// <param name="logger">日志记录器</param>
    /// <param name="message">日志消息</param>
    /// <param name="args">日志消息参数</param>
    public static void LogInfo( this Logger logger, string message, params object[] args )
    {
      Contract.Assert( logger != null );
      Contract.Assert( message != null );

      LogWithCustomType( logger, LogType.Info, message, args );
    }

    /// <summary>
    /// 记录一个信息性日志
    /// </summary>
    /// <param name="logger">日志记录器</param>
    /// <param name="message">日志消息</param>
    /// <param name="args">日志消息参数</param>
    public static Task LogInfoAsync( this AsyncLogger logger, string message, params object[] args )
    {
      Contract.Assert( logger != null );
      Contract.Assert( message != null );

      return LogWithCustomTypeAsync( logger, LogType.Info, message, args );
    }


    /// <summary>
    /// 记录一个警告信息
    /// </summary>
    /// <param name="logger">日志记录器</param>
    /// <param name="message">日志消息</param>
    /// <param name="args">日志消息参数</param>
    public static void LogWarning( this Logger logger, string message, params object[] args )
    {
      Contract.Assert( logger != null );
      Contract.Assert( message != null );

      LogWithCustomType( logger, LogType.Warning, message, args );
    }

    /// <summary>
    /// 记录一个警告信息
    /// </summary>
    /// <param name="logger">日志记录器</param>
    /// <param name="message">日志消息</param>
    /// <param name="args">日志消息参数</param>
    public static Task LogWarningAsync( this AsyncLogger logger, string message, params object[] args )
    {
      Contract.Assert( logger != null );
      Contract.Assert( message != null );

      return LogWithCustomTypeAsync( logger, LogType.Warning, message, args );
    }



    /// <summary>
    /// 记录一个错误信息
    /// </summary>
    /// <param name="logger">日志记录器</param>
    /// <param name="message">日志消息</param>
    /// <param name="args">日志消息参数</param>
    public static void LogError( this Logger logger, string message, params object[] args )
    {
      Contract.Assert( logger != null );
      Contract.Assert( message != null );

      LogWithCustomType( logger, LogType.Error, message, args );
    }

    /// <summary>
    /// 记录一个错误信息
    /// </summary>
    /// <param name="logger">日志记录器</param>
    /// <param name="message">日志消息</param>
    /// <param name="args">日志消息参数</param>
    public static Task LogError( this AsyncLogger logger, string message, params object[] args )
    {
      Contract.Assert( logger != null );
      Contract.Assert( message != null );

      return LogWithCustomTypeAsync( logger, LogType.Error, message, args );
    }


    /// <summary>
    /// 记录一个异常信息
    /// </summary>
    /// <param name="logger">日志记录器</param>
    /// <param name="exception">异常对象</param>
    public static void LogException( this Logger logger, Exception exception )
    {
      Contract.Assert( logger != null );

      if ( exception == null )
        throw new ArgumentNullException( "exception" );

      LogWithCustomType( logger, LogType.Exception, exception.ToString() );
    }

    /// <summary>
    /// 记录一个异常信息
    /// </summary>
    /// <param name="logger">日志记录器</param>
    /// <param name="exception">异常对象</param>
    public static Task LogExceptionAsync( this AsyncLogger logger, Exception exception )
    {
      Contract.Assert( logger != null );

      if ( exception == null )
        throw new ArgumentNullException( "exception" );

      return LogWithCustomTypeAsync( logger, LogType.Exception, exception.ToString() );
    }


    /// <summary>
    /// 记录一个致命错误信息
    /// </summary>
    /// <param name="logger">日志记录器</param>
    /// <param name="message">日志消息</param>
    /// <param name="args">日志消息参数</param>
    public static void LogFatalError( this Logger logger, string message, params object[] args )
    {
      Contract.Assert( logger != null );
      Contract.Assert( message != null );

      LogWithCustomType( logger, LogType.FatalError, message, args );
    }

    /// <summary>
    /// 记录一个致命错误信息
    /// </summary>
    /// <param name="logger">日志记录器</param>
    /// <param name="message">日志消息</param>
    /// <param name="args">日志消息参数</param>
    public static Task LogFatalErrorAsync( this AsyncLogger logger, string message, params object[] args )
    {
      Contract.Assert( logger != null );
      Contract.Assert( message != null );

      return LogWithCustomTypeAsync( logger, LogType.FatalError, message, args );
    }


    /// <summary>
    /// 记录一个引起系统崩溃无法恢复的错误信息
    /// </summary>
    /// <param name="logger">日志记录器</param>
    /// <param name="message">日志消息</param>
    /// <param name="args">日志消息参数</param>
    public static void LogCrashError( this Logger logger, string message, params object[] args )
    {
      Contract.Assert( logger != null );
      Contract.Assert( message != null );

      LogWithCustomType( logger, LogType.CrashError, message, args );
    }

    /// <summary>
    /// 记录一个引起系统崩溃无法恢复的错误信息
    /// </summary>
    /// <param name="logger">日志记录器</param>
    /// <param name="message">日志消息</param>
    /// <param name="args">日志消息参数</param>
    public static Task LogCrashErrorAsync( this AsyncLogger logger, string message, params object[] args )
    {
      Contract.Assert( logger != null );
      Contract.Assert( message != null );

      return LogWithCustomTypeAsync( logger, LogType.CrashError, message, args );
    }



    private static void LogWithCustomType( Logger logger, LogType type, string message, params object[] args )
    {
      Contract.Assert( logger != null );
      Contract.Assert( type != null );

      var meta = GetLogMeta( logger, type );

      if ( args.Any() )
        message = string.Format( message, args );

      logger.LogEntry( new LogEntry( message, meta ) );
    }

    private static Task LogWithCustomTypeAsync( AsyncLogger logger, LogType type, string message, params object[] args )
    {
      Contract.Assert( logger != null );
      Contract.Assert( type != null );

      var meta = GetLogMeta( logger, type );

      if ( args.Any() )
        message = string.Format( message, args );

      return logger.LogEntryAsync( new LogEntry( message, meta ) );
    }



    private static LogMeta GetLogMeta( Logger logger, LogType type )
    {
      Contract.Assert( logger != null );

      var meta = LogMeta.GetDefaultMeta().SetMetaData( type );


      var provider = logger as ILogMetaProvider;
      if ( provider != null )
        meta = provider.GetLogMeta( meta );

      return meta;
    }

  }
}
