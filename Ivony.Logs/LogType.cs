using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ivony.Logs
{

  /// <summary>
  /// 日志类别
  /// </summary>
  public abstract class LogType
  {

    /// <summary>
    /// 显示名称
    /// </summary>
    public abstract string DisplayName { get; }

    /// <summary>
    /// 严重程度
    /// </summary>
    public abstract int Serverity { get; }


    private class BuiltInLogType : LogType
    {
      public BuiltInLogType( string name, int servertity )
      {
        _name = name;
        _serverity = servertity;
      }



      private string _name;

      public override string DisplayName
      {
        get { return _name; }
      }

      private int _serverity;
      public override int Serverity
      {
        get { return _serverity; }
      }
    }


    public static readonly LogType Info       = new BuiltInLogType( "Infomation", 1000 );
    public static readonly LogType Warning    = new BuiltInLogType( "Warnning", 2000 );
    public static readonly LogType Error      = new BuiltInLogType( "Error", 3000 );
    public static readonly LogType Exception  = new BuiltInLogType( "Exception", 4000 );
    public static readonly LogType FatalError = new BuiltInLogType( "Fatal", 5000 );
    public static readonly LogType CrashError = new BuiltInLogType( "Crash", 10000 );
  }
}
