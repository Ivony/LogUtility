using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ivony.Logs
{
  public class LoggerWithFilter : PipedLogger
  {


    private LogFilter _filter;


    public LoggerWithFilter( Logger innerLogger, LogFilter filter )
      : base( innerLogger )
    {
      _filter = filter;
    }


    protected override LogFilter LogFilter
    {
      get { return _filter; }
    }

    protected override void WriteLog( LogEntry entry )
    {
      throw new NotImplementedException();
    }
  }
}
