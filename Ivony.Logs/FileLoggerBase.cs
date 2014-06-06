using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Ivony.Logs
{

  /// <summary>
  /// 文本文件日志记录器基类
  /// </summary>
  public abstract class TextFileLoggerBase : TextLogger
  {



    protected TextFileLoggerBase( LogFilter filter = null, Encoding encoding = null )
      : base( filter )
    {
      _encoding = encoding ?? Encoding.UTF8;
    }

    protected abstract string GetFilepath( LogEntry entry );


    private Encoding _encoding;

    /// <summary>
    /// 获取写入文件所用的编码
    /// </summary>
    protected virtual Encoding Encoding
    {
      get { return _encoding; }
    }




    protected override void WriteLogMessage( LogEntry entry, string[] lines )
    {
      var path = GetFilepath( entry );
      Directory.CreateDirectory( Path.GetDirectoryName( path ) );
      File.AppendAllLines( path, lines );
    }


  }
}
