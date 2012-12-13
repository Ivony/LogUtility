using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogUtility
{
  public class TextLogger : ILogger
  {

    public TextLogger( ILogWriter writer, IFormatProvider formatProvider = null, ILogger pipedLogger = null )
      : this( new LogWriterProvider( writer ), formatProvider, pipedLogger ) { }

    public TextLogger( ILogWriterProvider writerProvider, IFormatProvider formatProvider = null, ILogger pipedLogger = null )
    {
      _writerProvider = writerProvider;
      FormatProvider = formatProvider;
      PipedLogger = pipedLogger;
    }

    public void WriteLog( string format, params object[] args )
    {
      WriteLog( string.Format( format, args ) );
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

      var writer = GetWriter( entry );
      writer.Write( entry );
    }

    private ILogWriter GetWriter( LogEntry entry )
    {
      return _writerProvider.GetWriter( entry );
    }

    private ILogWriterProvider _writerProvider;


    private class LogWriterProvider : ILogWriterProvider
    {

      private ILogWriter _writer;

      public LogWriterProvider( ILogWriter writer )
      {
        _writer = writer;
      }

      public ILogWriter GetWriter( LogEntry entry )
      {
        return _writer;
      }
    }


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
