using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogUtility
{

  /// <summary>
  /// 将日志信息输出到控制台的日志记录器
  /// </summary>
  public class ConsoleLogger : TextLogger
  {


    /// <summary>
    /// 创建 ConsoleLogger 实例
    /// </summary>
    /// <param name="formatProvider">格式化提供程序</param>
    /// <param name="pipedLogger">管道日志记录器</param>
    public ConsoleLogger( IFormatProvider formatProvider = null, ILogger pipedLogger = null ) : base( formatProvider, pipedLogger ) { }

    protected override TextWriter GetTextWriter( LogEntry entry )
    {
      return Console.Out;
    }

  }

}
