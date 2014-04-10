using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ivony.Logs
{
  public abstract class Logger
  {

    public abstract void WriteLog( LogEntry entry );


    public static Logger operator +( Logger logger1, Logger logger2 )
    {
      return new MulticastLogger( logger1, logger2 );
    }


  }
}
