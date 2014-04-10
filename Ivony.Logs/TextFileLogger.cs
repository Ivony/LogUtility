using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Ivony.Logs
{

  /// <summary>
  /// 文件日志记录器
  /// </summary>
  public class TextFileLogger : FileLoggerBase
  {

    private string _filepath;
    private Encoding _encoding;

    public TextFileLogger( string logFilepath, Encoding encoding = null )
    {

      if ( logFilepath == null )
        throw new ArgumentNullException( "logFilepath" );

      _encoding = encoding ?? Encoding.UTF8;

      if ( !Path.IsPathRooted( logFilepath ) )
      {
        var basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        logFilepath = Path.Combine( basePath, logFilepath );

      }

      _filepath = logFilepath;
    }



    public override Encoding Encoding
    {
      get { return _encoding; }
    }

    protected override string GetFilepath( LogEntry entry )
    {
      return _filepath;
    }

  }
}
