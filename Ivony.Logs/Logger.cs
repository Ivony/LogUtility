using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ivony.Logs
{


  /// <summary>
  /// 定义所有日志记录器的基类型
  /// </summary>
  public abstract class Logger : IDisposable
  {


    private LogFilter _filter;

    /// <summary>
    /// 创建 Logger 对象
    /// </summary>
    protected Logger() : this( null ) { }

    /// <summary>
    /// 创建 Logger 对象
    /// </summary>
    /// <param name="filter">日志筛选器</param>
    protected Logger( LogFilter filter )
    {
      _filter = filter;
    }


    /// <summary>
    /// 用于筛选要记录的日志条目的日志筛选器
    /// </summary>
    protected virtual LogFilter LogFilter
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




    /// <summary>
    /// 派生类重写此方法以释放资源
    /// </summary>
    public virtual void Dispose()
    {
    }
  }
}
