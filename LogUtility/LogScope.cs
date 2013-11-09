using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogUtility
{


  /// <summary>
  /// 表示当前所处的范畴
  /// </summary>
  public class LogScope
  {
    /// <summary>
    /// 创建 LogScope 对象
    /// </summary>
    /// <param name="name">范畴名称</param>
    public LogScope( string name ) : this( name, null ) { }

    /// <summary>
    /// 创建 LogScope 对象
    /// </summary>
    /// <param name="name">范畴名称</param>
    /// <param name="parent">父级范畴</param>
    public LogScope( string name, LogScope parent )
    {
      Name = name;
      ParentScope = parent;
    }

    public LogScope ParentScope
    {
      get;
      private set;
    }

    public string Name
    {
      get;
      private set;
    }
  }
}
