using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ivony.Logs
{

  /// <summary>
  /// 定义日志文件周期性枚举
  /// </summary>
  public static class LogFileCycles
  {


    private class DailyCycle : LogFilenameProvider
    {
      public override string GetName( LogEntry entry )
      {
        return entry.LogDate.ToString( "yyyyMMdd" );
      }
    }

    private class HourlyCycle : LogFilenameProvider
    {
      public override string GetName( LogEntry entry )
      {
        return entry.LogDate.ToString( "yyyyMMdd-HH" );
      }
    }

    private class MinutelyCycle : LogFilenameProvider
    {
      public override string GetName( LogEntry entry )
      {
        return entry.LogDate.ToString( "yyyyMMdd.HHmm" );
      }
    }

    private class MonthlyCycle : LogFilenameProvider
    {
      public override string GetName( LogEntry entry )
      {
        return entry.LogDate.ToString( "yyyy-MM" );
      }
    }


    private static LogFilenameProvider _daily = new DailyCycle();
    private static LogFilenameProvider _hourly = new HourlyCycle();
    private static LogFilenameProvider _minutely = new MinutelyCycle();
    private static LogFilenameProvider _monthly = new MonthlyCycle();


    public static LogFilenameProvider Daily { get { return _daily; } }
    public static LogFilenameProvider Hourly { get { return _hourly; } }
    public static LogFilenameProvider Minutely { get { return _minutely; } }
    public static LogFilenameProvider Monthly { get { return _monthly; } }


  }
}
