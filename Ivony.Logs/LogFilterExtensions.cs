using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ivony.Logs
{
  public static class LogFilterExtensions
  {

    public static LogFilter FromSource( this LogFilter filter, string logSource )
    {
      return filter & LogFilter.BySource( logSource );
    }

  }
}
