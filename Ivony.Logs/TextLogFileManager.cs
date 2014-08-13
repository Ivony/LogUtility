using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Ivony.Logs
{
  public static class TextLogFileManager
  {


    private static object _sync;
    private static SynchronizedFileStreamCollection _collection;

    static TextLogFileManager()
    {
      _sync = new object();
      _collection = new SynchronizedFileStreamCollection( _sync );

    }


    public static void WriteText( string filepath, string content, Encoding encoding = null )
    {
      encoding = encoding ?? DefaultEncoding;

      var stream = GetFileStream( filepath );

      WriteText( stream, content, encoding );
    }

    private static SynchronizedFileStream GetFileStream( string filepath )
    {
      lock ( _sync )
      {
        if ( _collection.Contains( filepath ) )
          return _collection[filepath];

        else
        {
          var stream = SynchronizedFileStream.OpenFile( filepath );
          _collection.Add( stream );
          return stream;
        }
      }
    }


    private static void WriteText( SynchronizedFileStream stream, string content, Encoding encoding )
    {
      lock ( stream.SyncRoot )
      {
        stream.GetWriter( encoding ).Write( content );
      }
    }


    /// <summary>
    /// 刷新所有文件，确保所有修改写入磁盘
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
