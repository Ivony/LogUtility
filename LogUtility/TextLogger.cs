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
    {
      _writer = writer;
      FormatProvider = formatProvider;
      PipedLogger = pipedLogger;
    }

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
      if ( _writerProvider != null )
        return _writerProvider.GetWriter( entry );

      else if ( _writer != null )
        return _writer;
      
      throw new InvalidOperationException();
    }

    private ILogWriter _writer;
    private ILogWriterProvider _writerProvider;


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
