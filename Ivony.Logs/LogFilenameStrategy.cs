using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ivony.Logs
{

  /// <summary>
  /// 定义日志文件名提供程序
  /// </summary>
  public abstract class LogFilenameStrategy
  {

    /// <summary>
    /// 获取名称
    /// </summary>
    /// <param name="entry">日志记录</param>
    /// <returns>当前日志条目应当分配的名称</returns>
    public abstract string GetName( LogEntry entry );



    public static implicit operator LogFilenameStrategy( string literal )
    {
      return new Literal( literal );
    }


    public static LogFilenameStrategy operator +( LogFilenameStrategy strategy1, LogFilenameStrategy strategy2 )
    {
      return Series.Concat( strategy1, strategy2 );
    }


    private class Series : LogFilenameStrategy
    {

      public static LogFilenameStrategy Concat( LogFilenameStrategy strategy1, LogFilenameStrategy strategy2 )
      {
        var literal1 = strategy1 as Literal;
        if ( literal1 != null )
        {
          var literal2 = strategy2 as Literal;
          if ( literal2 != null )
            return new Literal( literal1.Text + literal2.Text );
        }

        return new Series( strategy1, strategy2 );
      }


      private readonly LogFilenameStrategy[] _strategies;


      public Series( LogFilenameStrategy strategy1, LogFilenameStrategy strategy2 )
      {


        var providers = new List<LogFilenameStrategy>();


        {
          var series = strategy1 as Series;
          if ( series != null )
            providers.AddRange( series._strategies );

          else
            providers.Add( strategy1 );
        }

        {
          var series = strategy2 as Series;
          if ( series != null )
            providers.AddRange( series._strategies );

          else
            providers.Add( strategy2 );
        }

        _strategies = providers.ToArray();

      }



      public override string GetName( LogEntry entry )
      {
        return string.Concat( _strategies.Select( s => s.GetName( entry ) ) );
      }

    }




    private class Literal : LogFilenameStrategy
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
