using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Ivony.Logs
{


  /// <summary>
  /// 定义按照日志记录源名称进行限制的日志筛选器
  /// </summary>
  [EditorBrowsable( EditorBrowsableState.Advanced )]
  public sealed class LogSourceNameRestrictFilter : LogFilter
  {


    public LogSourceNameRestrictFilter( string logSource )
    {

      if ( logSource == null )
        throw new ArgumentNullException( "logSource" );

      LogSourceName = logSource;
    }


    public string LogSourceName
    {
      get;
      private set;
    }


    public override bool Writable( LogEntry entry )
    {
      var source = entry.MetaData.Source;

      if ( source == null )
        return false;

      return string.Equals( LogSourceName, source.Name, StringComparison.OrdinalIgnoreCase );
    }
  }
}
