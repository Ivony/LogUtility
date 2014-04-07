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

    public LogSource Source { get; set; }

    public LogType Type { get; set; }



    private static readonly LogMeta _blank = new LogMeta()
    {
      Source = null,
      Type = LogType.Info
    };

    public static LogMeta Blank
    {
      get { return _blank; }
    }


  }
}
