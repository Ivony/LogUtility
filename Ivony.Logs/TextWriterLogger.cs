using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ivony.Logs
{
  public class TextWriterLogger : TextLogger
  {
    public TextWriterLogger( TextWriter writer )
    {
      if ( writer == null )
        throw new ArgumentNullException( "writer" );

      Writer = writer;
    }


    protected TextWriter Writer
    {
      get;
      private set;
    }

    protected override void WriteLogMessage( LogEntry entry, string[] contents )
    {
      foreach ( var line in contents )
        Writer.WriteLine( line );

      if ( TextLogFileManager.AutoFlush )
        Writer.Flush();
    }
  }


  public sealed class StringWriterLogger : TextWriterLogger
  {

    public StringWriterLogger() : base( new StringWriter() ) { }





  }

}
