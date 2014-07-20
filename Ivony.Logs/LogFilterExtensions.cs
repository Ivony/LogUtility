using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ivony.Logs
{

  /// <summary>
  /// 提供 LogFilter 的一系列扩展方法
  /// </summary>
  public static class LogFilterExtensions
  {

    /// <summary>
    /// 创建一个新的日志筛选器，除了目前的限制外，限制仅记录指定日志源的日志信息
    /// </summary>
    /// <param name="filter">要增加限制的日志筛选器</param>
    /// <param name="logSource">仅记录的日志源名称</param>
    /// <returns>一个新的日志筛选器，仅记录指定日志源的日志</returns>
    public static LogFilter FromSource( this LogFilter filter, string logSource )
    {
      return filter & LogFilter.BySource( logSource );
    }

  }
}
