using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ivony.Logs
{

  /// <summary>
  /// 定义日志源
  /// </summary>
  public abstract class LogSource
  {

    /// <summary>
    /// 日志源名称
    /// </summary>
    public abstract string Name { get; }

    public override string ToString()
    {
      return Name;
    }

  }
}
