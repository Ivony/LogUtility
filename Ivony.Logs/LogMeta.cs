using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ivony.Logs
{

  /// <summary>
  /// 定义日志元数据
  /// </summary>
  public sealed class LogMeta
  {

    private object sybc = new object();

    private Dictionary<Type, object> data;



    /// <summary>
    /// 创建 LogMeta 对象
    /// </summary>
    public LogMeta()
    {
      data = new Dictionary<Type, object>();
      SetMetaData( LogType.Info );
    }


    /// <summary>
    /// 从既有的元数据创建 LogMeta 对象
    /// </summary>
    /// <param name="metaData">既有的元数据</param>
    public LogMeta( LogMeta metaData )
    {
      data = new Dictionary<Type, object>( metaData.data );
    }



    /// <summary>
    /// 获取指定类型的元数据
    /// </summary>
    /// <typeparam name="T">元数据类型</typeparam>
    /// <returns></returns>
    public T GetMetaData<T>() where T : class
    {
      var type = GetRootType( typeof( T ) );

      lock ( sybc )
      {
        if ( data.ContainsKey( type ) )
          return data[type] as T;
      }

      return null;
    }


    /// <summary>
    /// 设置一个元数据
    /// </summary>
    /// <typeparam name="T">元数据类型</typeparam>
    /// <param name="metaData">元数据对象</param>
    /// <returns>LogMeta 对象自身，便于链式调用</returns>
    public LogMeta SetMetaData<T>( T metaData ) where T : class
    {
      var type = GetRootType( typeof( T ) );
      lock ( sybc )
      {
        data[type] = metaData;
      }

      return this;
    }

    private Type GetRootType( Type type )
    {
      if ( type.BaseType != typeof( object ) )
        return GetRootType( type );

      else
        return type;
    }


    private static readonly LogMeta _blank = new LogMeta();


    /// <summary>
    /// 获取空白日志元数据
    /// </summary>
    public static LogMeta Blank
    {
      get { return _blank; }
    }


  }
}
