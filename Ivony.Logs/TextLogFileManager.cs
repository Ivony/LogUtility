using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Ivony.Logs
{

  /// <summary>
  /// 提供文本日志文件创建、编写等服务的类
  /// </summary>
  public static class TextLogFileManager
  {


    private static object _sync;
    private static SynchronizedFileStreamCollection _collection;


    private static bool _autoFlush = true;

    /// <summary>
    /// 获取或设置一个值，该值指示是否在每次写入文本日志后，将立即刷新日志流。
    /// </summary>
    public static bool AutoFlush
    {
      get { return _autoFlush; }
      set
      {
        lock ( _sync )
        {
          _autoFlush = value;

          if ( value )
            Flush();
        }
      }
    }


    static TextLogFileManager()
    {
      _sync = new object();
      _collection = new SynchronizedFileStreamCollection( _sync );

    }


    /// <summary>
    /// 对指定的文本日志文件写入一段文本
    /// </summary>
    /// <param name="filepath">日志文件路径</param>
    /// <param name="content">文本内容</param>
    /// <param name="encoding">文本编码</param>
    /// <param name="flush">在写入后是否立即刷新日志流</param>
    public static void WriteText( string filepath, string content, Encoding encoding, bool flush = false )
    {
      encoding = encoding ?? DefaultEncoding;

      var stream = GetFileStream( filepath );
      WriteText( stream, content, encoding, flush );
    }




    private static SynchronizedFileStream GetFileStream( string filepath )
    {
      lock ( _sync )
      {
        if ( _collection.Contains( filepath ) )
          return _collection[filepath];

        else
        {
          Directory.CreateDirectory( Path.GetDirectoryName( filepath ) );
          var stream = SynchronizedFileStream.OpenFile( filepath );
          _collection.Add( stream );
          return stream;
        }
      }
    }


    private static void WriteText( SynchronizedFileStream stream, string content, Encoding encoding, bool flush )
    {
      lock ( stream.SyncRoot )
      {
        stream.GetWriter( encoding ).Write( content );
        if ( AutoFlush || flush )
          stream.Flush();
      }
    }


    /// <summary>
    /// 刷新所有日志文本流，确保所有修改已经写入。
    /// </summary>
    public static void Flush()
    {
      lock ( _sync )
      {

        foreach ( var stream in _collection )
        {
          lock ( stream.SyncRoot )
          {
            stream.Flush();
          }
        }

      }
    }



    /// <summary>
    /// 关闭指定路径的文件，将所有修改写入磁盘。
    /// </summary>
    /// <param name="filepath">文件路径</param>
    public static void Close( string filepath )
    {
      lock ( _sync )
      {

        if ( _collection.Contains( filepath ) )
        {
          var item = _collection[filepath];
          item.Dispose();
          _collection.Remove( filepath );
        }

      }
    }


    /// <summary>
    /// 获取编写日志时默认所需要采用的编码
    /// </summary>
    public static Encoding DefaultEncoding
    {
      get { return Encoding.UTF8; }
    }




    private sealed class SynchronizedFileStream : IDisposable
    {
      private SynchronizedFileStream()
      {
      }


      public string Filepath { get; private set; }

      public object SyncRoot { get; private set; }

      internal FileStream FileStream { get; private set; }


      private StreamWriter writer;

      public TextWriter GetWriter( Encoding encoding )
      {

        if ( writer != null )
        {
          if ( writer.Encoding == encoding )
            return writer;

          writer.Flush();
        }

        return writer = new StreamWriter( FileStream, encoding );
      }


      public void Flush()
      {
        if ( writer != null )
          writer.Flush();
      }


      public static SynchronizedFileStream OpenFile( string filepath )
      {
        var stream = File.Open( filepath, FileMode.Append, FileAccess.Write, FileShare.Read );

        return new SynchronizedFileStream
        {
          SyncRoot = new object(),
          Filepath = filepath,
          FileStream = stream
        };
      }


      public void Dispose()
      {
        writer.Dispose();
        writer = null;
        FileStream = null;
        Filepath = null;
      }


      ~SynchronizedFileStream()
      {
        if ( writer != null )
          writer.Flush();
      }
    }


    private sealed class SynchronizedFileStreamCollection : SynchronizedKeyedCollection<string, SynchronizedFileStream>
    {

      public SynchronizedFileStreamCollection( object syncRoot ) : base( syncRoot, StringComparer.OrdinalIgnoreCase ) { }




      protected override string GetKeyForItem( SynchronizedFileStream item )
      {
        return item.Filepath;
      }
    }

  }
}
