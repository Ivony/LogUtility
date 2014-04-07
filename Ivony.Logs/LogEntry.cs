using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ivony.Logs
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
    public LogEntry( string message, LogMeta meta = null, object raw = null )
    {
      LogDate = DateTime.UtcNow;
      MetaData = meta ?? LogMeta.Blank;

      Message = message;
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
    public LogMeta MetaData
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

    /// <summary>
    /// 产生日志的原始对象
    /// </summary>
    public object RawObject
    {
      get;
      private set;
    }

  }
}
