using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ivony.Logs
{


  /// <summary>
  /// 文本日志记录器基类，将日志信息以文本形式记录的所有记录器的基类
  /// </summary>
  public abstract class TextLogger : ILogger
  {

    /// <summary>
    /// 创建 TextLogger 对象
    /// </summary>
    /// <param name="formatProvider">格式化控制对象提供程序</param>
    protected TextLogger( IFormatProvider formatProvider = null )
    {
      FormatProvider = formatProvider ?? CultureInfo.InvariantCulture;
    }

    public void WriteLog( string format, params object[] args )
    {
      WriteLog( string.Format( FormatProvider, format, args ) );
    }

    public void WriteLog( string message )
    {
      var entry = CreateEntry( message );

      WriteLog( entry );
    }

    protected virtual LogEntry CreateEntry( string message )
    {
      return new LogEntry( message );
    }


    protected void WriteLog( LogEntry entry )
    {
      using ( var writer = GetWriter( entry ) )
      {
        writer.Write( entry );
      }
    }

    protected ILogWriter GetWriter( LogEntry entry )
    {
      return new TextLogWriter( GetTextWriter( entry ) );
    }

    protected abstract TextWriter GetTextWriter( LogEntry entry );


    /// <summary>
    /// 格式化提供程序
    /// </summary>
    protected IFormatProvider FormatProvider
    {
      get;
      private set;
    }


    void ILogger.WriteLog( LogEntry entry )
    {
      WriteLog( entry );
    }
  }
}
