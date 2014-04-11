using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ivony.Logs
{

  /// <summary>
  /// 定义日志文件周期性枚举
  /// </summary>
  public abstract class LogFileCycle
  {

    /// <summary>
    /// 获取日期字符串
    /// </summary>
    /// <param name="entry">日志条目</param>
    /// <returns>当前日志条目应当分配的日期字符串</returns>
    public abstract string GetDatetimeString( LogEntry entry );



    private class DailyCycle : LogFileCycle
    {
      public override string GetDatetimeString( LogEntry entry )
      {
        return entry.LogDate.ToString( "yyyyMMdd" );
      }
    }

    private class HourlyCycle : LogFileCycle
    {
      public override string GetDatetimeString( LogEntry entry )
      {
        return entry.LogDate.ToString( "yyyyMMdd-HH" );
      }
    }

    private class MinutelyCycle : LogFileCycle
    {
      public override string GetDatetimeString( LogEntry entry )
      {
        return entry.LogDate.ToString( "yyyyMMdd.HHmm" );
      }
    }

    private class MonthlyCycle : LogFileCycle
    {
      public override string GetDatetimeString( LogEntry entry )
      {
        return entry.LogDate.ToString( "yyyy-MM" );
      }
    }


    private static LogFileCycle _daily = new DailyCycle();
    private static LogFileCycle _hourly = new HourlyCycle();
    private static LogFileCycle _minutely = new MinutelyCycle();
    private static LogFileCycle _monthly = new MonthlyCycle();


    public static LogFileCycle Daily { get { return _daily; } }
    public static LogFileCycle Hourly { get { return _hourly; } }
    public static LogFileCycle Minutely { get { return _minutely; } }
    public static LogFileCycle Monthly { get { return _monthly; } }

  }
}
