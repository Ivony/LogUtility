using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogUtility
{
  public class ConsoleLogger : TextLogger
  {

    public ConsoleLogger( IFormatProvider formatProvider = null, ILogger pipedLogger = null ) : base( new TextLogWriter( Console.Out ), formatProvider, pipedLogger ) { }

  }

}
