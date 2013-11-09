using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogUtility
{


  public abstract class TextLogger : ILogger
  {

    public TextLogger( IFormatProvider formatProvider = null, ILogger pipedLogger = null )
    {
      FormatProvider = formatProvider ?? CultureInfo.InvariantCulture;
      PipedLogger = pipedLogger;
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
      return new LogEntry( message, Scope );
    }


    protected void WriteLog( LogEntry entry )
    {
      if ( PipedLogger != null )
        PipedLogger.WriteLog( entry );

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
    /// 当前所在范围
    /// </summary>
    public LogScope Scope
    {
      get;
      private set;
    }

    /// <summary>
    /// 格式化提供程序
    /// </summary>
    protected IFormatProvider FormatProvider
    {
      get;
      private set;
    }

    protected ILogger PipedLogger
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
