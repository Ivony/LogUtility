using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ivony.Logs
{

  /// <summary>
  /// 定义日志编写器
  /// </summary>
  public interface ILogWriter : IDisposable
  {
    void Write( LogEntry entry );
  }
}
