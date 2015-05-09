using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ivony.Logs
{

  /// <summary>
  /// 所有异步日志记录器需要继承的基类
  /// </summary>
  public abstract class AsyncLogger : Logger
  {
    public override void LogEntry( LogEntry entry )
    {
      Task.Run( () => LogEntryAsync( entry ) ).Wait();
    }


    /// <summary>
    /// 异步记录日志
    /// </summary>
    /// <param name="entry">要记录的日志条目</param>
    /// <returns>用于等待异步任务完成的 Task 对象</returns>
    public abstract Task LogEntryAsync( LogEntry entry );
  }
}
