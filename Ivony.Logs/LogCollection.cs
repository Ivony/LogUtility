using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ivony.Logs
{

  /// <summary>
  /// 定义一个可以将日志保存起来以供检索的容器
  /// </summary>
  public sealed class LogCollection : Logger, IEnumerable<LogEntry>
  {


    public LogCollection( LogFilter filter = null )
      : base( filter )
    {
      SyncRoot = new object();
    }



    private List<LogEntry> _list = new List<LogEntry>();

    /// <summary>
    /// 获取用于同步的对象
    /// </summary>
    public object SyncRoot
    {
      get;
      private set;
    }


    /// <summary>
    /// 重写 WriteLog 方法记录日志
    /// </summary>
    /// <param name="entry">要记录的日志条目</param>
    protected override void WriteLog( LogEntry entry )
    {
      lock ( SyncRoot )
      {
        _list.Add( entry );
      }
    }



    /// <summary>
    /// 清空日志容器
    /// </summary>
    public void Clear()
    {
      lock ( SyncRoot )
      {
        _list.Clear();
      }
    }

    IEnumerator<LogEntry> IEnumerable<LogEntry>.GetEnumerator()
    {
      return _list.GetEnumerator();
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return _list.GetEnumerator();
    }
  }
}
