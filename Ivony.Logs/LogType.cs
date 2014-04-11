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


    /// <summary>
    /// 用于唯一标识该日志类别的 GUID。
    /// </summary>
    public abstract Guid Guid { get; }


    public override bool Equals( object obj )
    {
      var type = obj as LogType;
      if ( type == null )
        return false;

      else
        return type.Guid.Equals( Guid );
    }


    public override int GetHashCode()
    {
      return Guid.GetHashCode();
    }





    public static bool operator ==( LogType type1, LogType type2 )
    {
      return type1.Guid.Equals( type2.Guid );
    }


    public static bool operator !=( LogType type1, LogType type2 )
    {
      return !type1.Guid.Equals( type2.Guid );
    }



    private class BuiltInLogType : LogType
    {
      public BuiltInLogType( string name, int servertity, Guid guid )
      {
        _name = name;
        _serverity = servertity;
        _guid = guid;
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


      private Guid _guid;
      public override Guid Guid
      {
        get { return _guid; }
      }

    }


    public static readonly LogType Info       = new BuiltInLogType( "Infomation", 1000, new Guid( "{185D55AC-9E39-4331-9988-1EE8D3804E83}" ) );
    public static readonly LogType Warning    = new BuiltInLogType( "Warnning", 2000, new Guid( "{3E35B201-01FD-481E-9BB8-62286137AF76}" ) );
    public static readonly LogType Error      = new BuiltInLogType( "Error", 3000, new Guid( "{93C31802-5A3E-4E6F-A2CB-0C8DDAEA2D65}" ) );
    public static readonly LogType Exception  = new BuiltInLogType( "Exception", 4000, new Guid( "{83C1A0D8-C0ED-4A9C-848E-26BD7E8600A4}" ) );
    public static readonly LogType FatalError = new BuiltInLogType( "Fatal", 5000, new Guid( "{1D7001EB-50B9-4788-BFE5-86A641DB439F}" ) );
    public static readonly LogType CrashError = new BuiltInLogType( "Crash", 10000, new Guid( "{45FFE3F2-E96A-460A-ACC2-3047D98C3DEF}" ) );
  }
}
