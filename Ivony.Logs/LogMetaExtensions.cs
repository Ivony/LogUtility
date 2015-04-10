using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ivony.Logs
{


  /// <summary>
  /// 提供 LogMeta 的一些帮助方法
  /// </summary>
  public static class LogMetaExtensions
  {

    public static LogSource LogSource( this LogEntry entry )
    {
      if ( entry == null )
        return null;

      return LogSource( entry.MetaData );
    }

    public static LogSource LogSource( this LogMeta metaData )
    {
      if ( metaData == null )
        return null;

      return metaData.GetMetaData<LogSource>();
    }


    public static LogType LogType( this LogEntry entry )
    {
      if ( entry == null )
        return null;

      return LogType( entry.MetaData );
    }

    public static LogType LogType( this LogMeta metaData )
    {
      if ( metaData == null )
        return null;

      return metaData.GetMetaData<LogType>();
    }


  }
}
