using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LogUtility
{
  public class FileLogger : TextLogger
  {

    private string _filepath;

    public FileLogger( string logFilepath )
    {
      if ( !Path.IsPathRooted( logFilepath ) )
      {
        var basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        logFilepath = Path.Combine( basePath, logFilepath );

      }

      _filepath = logFilepath;
    }


    protected override TextWriter GetTextWriter( LogEntry entry )
    {
      return GetWriter( _filepath );
    }


    public TextWriter GetWriter( string path )
    {
      Directory.CreateDirectory( Path.GetDirectoryName( path ) );
      return new StreamWriter( File.Open( path, FileMode.Append, FileAccess.Write ) );
    }

  }
}
