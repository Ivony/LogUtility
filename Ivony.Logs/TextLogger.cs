using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ivony.Logs
{


  /// <summary>
  /// 文本日志记录器基类，将日志信息以文本形式记录的所有记录器的基类
  /// </summary>
  public abstract class TextLogger : Logger
  {


    protected TextLogger( ILogFilter filter = null ) : base( filter ) { }




    /// <summary>
    /// 写入一条日志信息
    /// </summary>
    /// <param name="entry"></param>
    protected override void WriteLog( LogEntry entry )
    {
      Write( entry, GetPadding( entry ), entry.Message );
    }


    protected virtual void ReleaseWriter( TextWriter writer )
    {
      writer.Flush();
    }




    /// <summary>
    /// 派生类实现此方法写入日志
    /// </summary>
    /// <param name="contents">日志内容行</param>
    protected abstract void WriteLogeMessage( LogEntry entry, string[] contents );






    /// <summary>
    /// 使用指定的前缀写入多行日志
    /// </summary>
    /// <param name="padding">填充字符串，将会添加在每一行日志的前面</param>
    /// <param name="message">日志消息</param>
    protected virtual void Write( LogEntry entry, string padding, string message )
    {


      var messageLines = SplitMultiLine( message );



      if ( messageLines.Length == 1 )
      {
        WriteLogeMessage( entry, new[] { padding + " " + messageLines[0] } );
        return;
      }

      for ( int i = 0; i < messageLines.Length; i++ )
      {
        if ( i == 0 )
          messageLines[i] = padding + "/" + messageLines[i];
        else if ( i == messageLines.Length - 1 )
          messageLines[i] = padding + "\\" + messageLines[i];
        else
          messageLines[i] = padding + "|" + messageLines[i];
      }

      WriteLogeMessage( entry, messageLines );
    }


    /// <summary>
    /// 将多行消息按照换行符拆分成多个字符串
    /// </summary>
    /// <param name="message">多行消息</param>
    /// <returns>拆分后的结果</returns>
    protected virtual string[] SplitMultiLine( string message )
    {
      if ( message == null )
        return null;

      return message.Split( new[] { Environment.NewLine, "\r\n", "\r", "\n" }, StringSplitOptions.None );
    }


    /// <summary>
    /// 获取日志消息的填充，将会自动添加在日志消息的每一行的开头
    /// </summary>
    /// <param name="entry">当前要记录的日志</param>
    /// <returns>当前日志消息的填充</returns>
    protected virtual string GetPadding( LogEntry entry )
    {
      return GetTypePrefix( entry.MetaData.Type ) + " " + entry.LogDate.ToString( DateTimeFormatString ) + " ";
    }



    /// <summary>
    /// 获取日志消息的类型前缀，将会自动添加在每一行消息以标识这个消息的类型
    /// </summary>
    /// <param name="type">当前要记录的日志</param>
    /// <returns>当前日志消息的前缀</returns>
    protected virtual string GetTypePrefix( LogType type )
    {

      if ( type == null || type.Serverity == 0 )
        return "??";

      else if ( type.Serverity <= 500 )
        return " #";

      else if ( type == LogType.Info || type.Serverity <= 1000 )
        return "##";

      else if ( type == LogType.Warning || type.Serverity <= 2000 )
        return "#!";

      else if ( type == LogType.Error || type.Serverity <= 3000 )
        return "@!";

      else if ( type == LogType.Exception || type.Serverity <= 4000 )
        return "E!";

      else if ( type == LogType.FatalError || type.Serverity <= 5000 )
        return "F!";

      else if ( type == LogType.CrashError || type.Serverity <= 10000 )
        return "!!";

      else
        return "?!!";
    }


    protected virtual string DateTimeFormatString
    {
      get { return "yyyy-MM-dd HH:mm:ss"; }
    }



    /// <summary>
    /// 格式化提供程序
    /// </summary>
    protected IFormatProvider FormatProvider
    {
      get;
      private set;
    }

  }
}
