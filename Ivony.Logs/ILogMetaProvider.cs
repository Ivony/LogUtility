using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ivony.Logs
{

  /// <summary>
  /// 定义 LogMeta 数据提供程序
  /// </summary>
  public interface ILogMetaProvider
  {


    /// <summary>
    /// 获取 LogMeta 数据
    /// </summary>
    /// <param name="type">参考的日志类型</param>
    /// <param name="source">参考的日志来源</param>
    /// <returns>LogMeta 数据</returns>
    LogMeta GetLogMeta( LogType type = null, LogSource source = null );
  }
}
