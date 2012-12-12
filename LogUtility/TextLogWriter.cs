using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogUtility
{
  public class TextLogWriter : ILogWriter
  {

    private TextWriter _writer;

    public TextLogWriter( TextWriter writer )
    {
      if ( writer == null )
        throw new ArgumentNullException( "writer" );

      _writer = writer;
    }


    public void Write( LogEntry entry )
    {
      _writer.WriteLine( entry.Message );
    }
  }
}
