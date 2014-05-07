using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ivony.Logs
{

  /// <summary>
  /// 定义日志文件名提供程序
  /// </summary>
  public abstract class LogFilenameProvider
  {

    /// <summary>
    /// 获取名称
    /// </summary>
    /// <param name="entry">日志记录</param>
    /// <returns>当前日志条目应当分配的名称</returns>
    public abstract string GetName( LogEntry entry );



    public static implicit operator LogFilenameProvider( string literal )
    {
      return new Literal( literal );
    }


    public static LogFilenameProvider operator +( LogFilenameProvider provider1, LogFilenameProvider provider2 )
    {
      return Series.Concat( provider1, provider2 );
    }


    private class Series : LogFilenameProvider
    {

      public static LogFilenameProvider Concat( LogFilenameProvider provider1, LogFilenameProvider provider2 )
      {
        var literal1 = provider1 as Literal;
        if ( literal1 != null )
        {
          var literal2 = provider2 as Literal;
          if ( literal2 != null )
            return new Literal( literal1.Text + literal2.Text );
        }

        return new Series( provider1, provider2 );
      }


      private readonly LogFilenameProvider[] _providers;


      public Series( LogFilenameProvider provider1, LogFilenameProvider provider2 )
      {


        var providers = new List<LogFilenameProvider>();


        {
          var series = provider1 as Series;
          if ( series != null )
            providers.AddRange( series._providers );

          else
            providers.Add( provider1 );
        }

        {
          var series = provider2 as Series;
          if ( series != null )
            providers.AddRange( series._providers );

          else
            providers.Add( provider2 );
        }

        _providers = providers.ToArray();

      }



      public override string GetName( LogEntry entry )
      {
        return string.Concat( _providers.Select( p => p.GetName( entry ) ) );
      }

    }




    private class Literal : LogFilenameProvider
    {

      private readonly string _text;


      public string Text { get { return _text; } }

      public Literal( string text )
      {
        _text = text;
      }


      public override string GetName( LogEntry entry )
      {
        return _text;
      }
    }


  }
}
