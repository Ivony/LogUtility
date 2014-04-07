using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ivony.Logs
{

  /// <summary>
  /// 日志筛选器，用于筛选出需要记录的日志条目
  /// </summary>
  public interface ILogFilter
  {

    /// <summary>
    /// 确定指定的日志条目是否需要记录
    /// </summary>
    /// <param name="entry">日志条目</param>
    /// <returns>是否需要记录</returns>
    bool Writable( LogEntry entry );

  }
}
