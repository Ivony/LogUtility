using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogUtility
{


  /// <summary>
  /// 表示一条日志记录
  /// </summary>
  public class LogEntry
  {

    /// <summary>
    /// 创建一条日志记录
    /// </summary>
    /// <param name="message">日志消息</param>
    /// <param name="scope">当前所处的范畴</param>
    /// <param name="raw">日志记录的原始对象</param>
    public LogEntry( string message, LogScope scope, object raw = null )
    {
      LogDate = DateTime.UtcNow;
      Message = message;
      Scope = scope;
      RawObject = raw;
    }



    /// <summary>
    /// 获取日志产生的时间，该时间以 UTC 时间表示
    /// </summary>
    public DateTime LogDate
    {
      get;
      private set;
    }

    /// <summary>
    /// 获取日志所属的范畴，范畴可以表示日志类型或是来源
    /// </summary>
    public LogScope Scope
    {
      get;
      private set;
    }

    /// <summary>
    /// 日志消息
    /// </summary>
    public string Message
    {
      get;
      private set;
    }

    public object RawObject
    {
      get;
      private set;
    }

  }
}
