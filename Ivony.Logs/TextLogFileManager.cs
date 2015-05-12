using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    public static Task WriteTextAsync( string filepath, string content, Encoding encoding, bool flush = false )
    {
      encoding = encoding ?? DefaultEncoding;

      return GetFileStream( filepath ).WriteTextAsync( content, encoding, AutoFlush || flush );
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
          var stream = new SynchronizedFileStream( filepath );
          _collection.Add( stream );
          return stream;
        }
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
      public SynchronizedFileStream( string filepath )
      {
        Filepath = filepath;
        FileStream = OpenFile( filepath );
        SyncRoot = new object();
      }

      public string Filepath { get; private set; }

      public object SyncRoot { get; private set; }

      internal FileStream FileStream { get; private set; }


      private StreamWriter writer;


      private Task task;


      public async Task WriteTextAsync( string content, Encoding encoding, bool flush )
      {


      begin:

        if ( task != null )
          await task;

        lock ( SyncRoot )
        {
          if ( task != null )
            goto begin;

          var writer = GetWriter( encoding );
          task = WriteTextAsync( writer, content, flush );
        }


        await task;
      }

      private async Task WriteTextAsync( TextWriter writer, string content, bool flush )
      {
        await writer.WriteAsync( content );
        if ( flush )
          await writer.FlushAsync();
      }



      private TextWriter GetWriter( Encoding encoding )
      {

        lock ( SyncRoot )
        {
          if ( writer != null )
          {
            if ( writer.Encoding.Equals( encoding ) )
              return writer;

            writer.Dispose();
            FileStream = OpenFile( Filepath );
          }

          return writer = new StreamWriter( FileStream, encoding, 1024, true );
        }
      }


      private static FileStream OpenFile( string filepath )
      {
        return File.Open( filepath, FileMode.Append, FileAccess.Write, FileShare.Read );
      }


      public void Flush()
      {
        if ( writer != null )
          writer.Flush();
      }


      public void Dispose()
      {
        writer.Dispose();
        writer = null;
        FileStream = null;
        Filepath = null;
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
