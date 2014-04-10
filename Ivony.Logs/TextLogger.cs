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

    protected TextLogger()
    {
    }


    public override void WriteLog( LogEntry entry )
    {
      var writer = GetTextWriter( entry );

      try
      {
        Write( writer, entry );
      }
      finally
      {

        ReleaseWriter( writer );
      }
    }

    protected abstract TextWriter GetTextWriter( LogEntry entry );


    protected virtual void ReleaseWriter( TextWriter writer )
    {
      writer.Flush();
    }



    private object _sync = new object();

    /// <summary>
    /// 获取用于同步的对象
    /// </summary>
    protected object SyncRoot
    {
      get { return _sync; }
    }



    /// <summary>
    /// 写入一条日志信息
    /// </summary>
    /// <param name="entry"></param>
    public void Write( TextWriter writer, LogEntry entry )
    {
      Write( writer, GetPadding( entry ), entry.Message );
    }

    /// <summary>
    /// 使用指定的前缀写入多行日志
    /// </summary>
    /// <param name="padding">填充字符串，将会添加在每一行日志的前面</param>
    /// <param name="message">日志消息</param>
    protected virtual void Write( TextWriter writer, string padding, string message )
    {

      var messageLines = SplitMultiLine( message );

      lock ( SyncRoot )//确保所有的行会一起写入，不至于中途插入别的行
      {
        foreach ( var line in messageLines )
        {
          writer.WriteLine( padding + line );
        }


        if ( messageLines.Length > 1 )
          writer.WriteLine();//写入一个空行表示多行日志记录结束。
      };

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

      return message.Split( new[] { Environment.NewLine }, StringSplitOptions.None );
    }


    /// <summary>
    /// 获取日志消息的填充，将会自动添加在日志消息的每一行的开头
    /// </summary>
    /// <param name="entry">当前要记录的日志</param>
    /// <returns>当前日志消息的填充</returns>
    private string GetPadding( LogEntry entry )
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
