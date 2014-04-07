using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ivony.Logs
{
  public class TextLogWriter : ILogWriter, IDisposable
  {

    private TextWriter _writer;

    public TextLogWriter( TextWriter writer )
    {
      if ( writer == null )
        throw new ArgumentNullException( "writer" );

      _writer = writer;
    }



    private object _sync = new object();

    protected object SyncRoot
    {
      get { return _sync; }
    }



    /// <summary>
    /// 写入一条日志信息
    /// </summary>
    /// <param name="entry"></param>
    public void Write( LogEntry entry )
    {

      var prefix = GetPrefix( entry ) + " " + entry.LogDate.ToString( DateTimeFormatString ) + " ";

      Write( prefix, entry.Message );
    }



    /// <summary>
    /// 使用指定的前缀写入多行日志
    /// </summary>
    /// <param name="prefix">前缀</param>
    /// <param name="message">日志消息</param>
    protected virtual void Write( string prefix, string message )
    {
      var messageLines = SplitMultiLine( message );

      lock ( SyncRoot )//确保所有的行会一起写入，不至于中途插入别的行
      {
        foreach ( var line in messageLines )
        {
          _writer.WriteLine( prefix + line );
        }


        if ( messageLines.Length > 1 )
          _writer.WriteLine();//写入一个空行表示多行日志记录结束。
      };

    }


    protected virtual string[] SplitMultiLine( string message )
    {
      return message.Split( new[] { Environment.NewLine }, StringSplitOptions.None );
    }


    protected virtual string GetPrefix( LogEntry entry )
    {

      if ( entry.MetaData.Type.Serverity == 0 )
        return "??";

      else if ( entry.MetaData.Type.Serverity <= 500 )
        return " #";

      else if ( entry.MetaData.Type == LogType.Info || entry.MetaData.Type.Serverity <= 1000 )
        return "##";

      else if ( entry.MetaData.Type == LogType.Warning || entry.MetaData.Type.Serverity <= 2000 )
        return "#!";

      else if ( entry.MetaData.Type == LogType.Error || entry.MetaData.Type.Serverity <= 3000 )
        return "@!";

      else if ( entry.MetaData.Type == LogType.Exception || entry.MetaData.Type.Serverity <= 4000 )
        return "E!";

      else if ( entry.MetaData.Type == LogType.FatalError || entry.MetaData.Type.Serverity <= 5000 )
        return "F!";

      else if ( entry.MetaData.Type == LogType.CrashError || entry.MetaData.Type.Serverity <= 10000 )
        return "!!";

      else
        return "?!!";
    }


    public virtual string DateTimeFormatString
    {
      get
      {
        return "yyyy-MM-dd HH:mm:ss";
      }
    }

    public void Dispose()
    {
      _writer.Dispose();
    }

  }
}
