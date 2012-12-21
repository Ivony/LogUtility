using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogUtility
{
  public class ConsoleLogger : TextLogger
  {

    public ConsoleLogger( IFormatProvider formatProvider = null, ILogger pipedLogger = null ) : base( formatProvider, pipedLogger ) { }

    protected override TextWriter GetTextWriter( LogEntry entry )
    {
      return Console.Out;
    }

  }

}
