using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ivony.Logs
{

  /// <summary>
  /// 定义日志元数据
  /// </summary>
  public class LogMeta
  {

    /// <summary>
    /// 日志来源
    /// </summary>
    public LogSource Source { get; set; }

    /// <summary>
    /// 日志类型
    /// </summary>
    public LogType Type { get; set; }



    private static readonly LogMeta _blank = new LogMeta()
    {
      Source = null,
      Type = LogType.Info
    };


    /// <summary>
    /// 获取空白日志元数据
    /// </summary>
    public static LogMeta Blank
    {
      get { return _blank; }
    }


  }
}
