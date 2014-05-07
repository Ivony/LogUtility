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
  public class TextFileLogger : TextFileLoggerBase
  {

    private LogFilenameProvider _filenameProvider;


    private string basePath;


    private TextFileLogger( ILogFilter filter, Encoding encoding )
      : base( filter, encoding )
    {
      basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
    }

    public TextFileLogger( LogFilenameProvider filenameProvider, ILogFilter filter = null, Encoding encoding = null )
      : this( filter, encoding )
    {

      if ( filenameProvider == null )
        throw new ArgumentNullException( "filenameProvider" );

      _filenameProvider = filenameProvider;

    }

    public TextFileLogger( DirectoryInfo logDirectory, ILogFilter filter = null, LogFilenameProvider cycle = null, string prefix = "", string postfix = "", string extension = ".log", Encoding encoding = null )
      : this( filter, encoding )
    {

      if ( logDirectory == null )
        throw new ArgumentNullException( "logDirectory" );

      cycle = cycle ?? LogFileCycles.Daily;

      _filenameProvider = logDirectory.FullName + Path.DirectorySeparatorChar + prefix + cycle + postfix + extension;

    }




    protected override string GetFilepath( LogEntry entry )
    {
      var path = _filenameProvider.GetName( entry );

      return Path.Combine( basePath, path );
    }

  }
}
