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
  public abstract class FileLoggerBase : TextLogger
  {


    protected abstract string GetFilepath( LogEntry entry );

    public abstract Encoding Encoding { get; }




    protected override void WriteLogeMessage( LogEntry entry, string[] lines )
    {
      var path = GetFilepath( entry );
      Directory.CreateDirectory( Path.GetDirectoryName( path ) );
      File.AppendAllLines( path, lines );
    }


  }
}
