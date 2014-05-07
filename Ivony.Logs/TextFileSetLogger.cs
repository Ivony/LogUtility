using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Ivony.Logs
{

  /// <summary>
  /// 按照日期自动记录在多个文本文件中的日志记录器。
  /// </summary>
  public class TextFileSetLogger : TextFileLoggerBase
  {

    public TextFileSetLogger( string logDirectory, ILogFilter filter = null, LogFileCycle cycle = null, string prefix = "", string postfix = "", string extension = ".log", Encoding encoding = null )
      : base( filter, encoding )
    {
      LogDirectory = logDirectory;

      LogFileCycle = cycle ?? LogFileCycle.Daily;

      Prefix = prefix;
      Postfix = postfix;
      ExtensionName = extension;
    }

    protected override string GetFilepath( LogEntry entry )
    {
      return Path.Combine( LogDirectory, Prefix + LogFileCycle.GetDatetimeString( entry ) + Postfix + ExtensionName );
    }

    public string LogDirectory
    {
      get;
      private set;
    }



    public LogFileCycle LogFileCycle { get; private set; }

    public string Prefix { get; private set; }
    public string Postfix { get; private set; }
    public string ExtensionName { get; private set; }

    protected override Encoding Encoding
    {
      get { throw new NotImplementedException(); }
    }
  }
}
