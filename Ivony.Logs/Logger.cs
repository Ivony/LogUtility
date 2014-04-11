using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ivony.Logs
{
  public abstract class Logger
  {


    private ILogFilter _filter;

    protected Logger() : this( null ) { }

    protected Logger( ILogFilter filter )
    {
      _filter = filter;
    }


    /// <summary>
    /// 用于筛选要记录的日志条目的日志筛选器
    /// </summary>
    protected virtual ILogFilter LogFilter
    {
      get { return _filter; }
    }


    /// <summary>
    /// 记录一条日志
    /// </summary>
    /// <param name="entry">要记录的日志条目</param>
    public virtual void LogEntry( LogEntry entry )
    {
      lock ( SyncRoot )
      {
        if ( LogFilter == null || LogFilter.Writable( entry ) )
          WriteLog( entry );
      }
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



    private object _sync = new object();

    /// <summary>
    /// 获取用于同步的对象
    /// </summary>
    public object SyncRoot
    {
      get { return _sync; }
    }



  }
}
