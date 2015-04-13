using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Ivony.Logs
{

  /// <summary>
  /// 定义日志范畴对象
  /// </summary>
  public class LogScope : IDisposable
  {

    protected LogScope( string name )
    {
      if ( name == null )
        throw new ArgumentNullException( "name" );

      if ( name == "" )
        throw new ArgumentException( "范畴名称不能为空", "name" );

      if ( name.Contains( '/' ) )
        throw new ArgumentException( "范畴名称不能包含 \"/\" 字符", "name" );
    }

    private LogScope()
    {

    }




    private static string logScopeContextName = "log-scope";

    /// <summary>
    /// 获取当前范畴对象
    /// </summary>
    public static LogScope CurrentScope
    {
      get { return CallContext.LogicalGetData( logScopeContextName ) as LogScope ?? RootScope; }
      private set { CallContext.LogicalSetData( logScopeContextName, value ); }
    }


    private static LogScope _root = new LogScope();


    /// <summary>
    /// 根范畴
    /// </summary>
    public static LogScope RootScope
    {
      get { return _root; }
    }


    /// <summary>
    /// 父级范畴
    /// </summary>
    public LogScope Parent
    {
      get;
      private set;
    }


    /// <summary>
    /// 创建并进入一个日志范畴
    /// </summary>
    /// <param name="name">范畴名称</param>
    /// <returns></returns>
    public static LogScope EnterScope( string name )
    {
      return EnterScope( new LogScope( name ) );
    }



    /// <summary>
    /// 进入一个日志范畴
    /// </summary>
    /// <param name="scope">要进入的日志范畴</param>
    /// <returns></returns>
    public static LogScope EnterScope( LogScope scope )
    {

      if ( scope == null )
        throw new ArgumentNullException( "scope" );

      if ( scope.Parent != null )
        throw new InvalidOperationException( "无法进入这个范畴，因为这个范畴已经被使用" );

      scope.Parent = CurrentScope;
      return CurrentScope = scope;
    }



    /// <summary>
    /// 离开指定的范畴，若指定范畴在当前上下文不存在，则不进行任何操作，并返回 null 。
    /// </summary>
    /// <param name="scope">要离开的范畴</param>
    /// <returns></returns>
    public static LogScope LeaveScope( LogScope scope )
    {
      var current = CurrentScope;

      if ( scope == RootScope )
        return CurrentScope = RootScope;

      while ( current != scope && current != RootScope )
        current = current.Parent;


      if ( current == RootScope )
        return null;

      else
        return CurrentScope = current.Parent;
    }


    public void Dispose()
    {
      LeaveScope( this );
    }
  }
}
