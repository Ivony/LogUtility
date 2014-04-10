using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ivony.Logs
{
  public abstract class Logger
  {



    /// <summary>
    /// 用于筛选要记录的日志条目的日志筛选器
    /// </summary>
    protected virtual ILogFilter Filter
    {
      get { return null; }
    }


    /// <summary>
    /// 记录一条日志
    /// </summary>
    /// <param name="entry">要记录的日志条目</param>
    public void LogEntry( LogEntry entry )
    {
      if ( Filter != null && Filter.Writable( entry ) )
        WriteLog( entry );
    }

    /// <summary>
    /// 由派生类实现，编写一条日志
    /// </summary>
    /// <param name="entry">要编写的日志条目</param>
    protected abstract void WriteLog( LogEntry entry );





    public static Logger operator +( Logger logger1, Logger logger2 )
    {
      return new MulticastLogger( logger1, logger2 );
    }


  }
}
