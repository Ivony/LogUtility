using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ivony.Logs
{

  /// <summary>
  /// 定义所有日志记录器必须实现的接口
  /// </summary>
  public interface ILogger
  {

    /// <summary>
    /// 写入一条日志
    /// </summary>
    /// <param name="entry">日志条目</param>
    void WriteLog( LogEntry entry );

  }
}
