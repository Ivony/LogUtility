using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LogUtility
{
  public class FileLogger : TextLogger, ILogWriterProvider
  {

    private string _filepath;

    public FileLogger( string logFilepath )
    {
      _filepath = logFilepath;
    }


    protected override TextWriter GetTextWriter( LogEntry entry )
    {
      return GetWriter( _filepath );
    }


    public TextWriter GetWriter( string path )
    {
      return new StreamWriter( File.Open( path, FileMode.Append, FileAccess.Write ) );
    }

  }
}
