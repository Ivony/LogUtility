using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ivony.Logs
{

  /// <summary>
  /// 协助写入文本日志的日志写入器
  /// </summary>
  public class TextFileLogWriter
  {

    private string _filepath;


    /// <summary>
    /// 创建 TextFileLogWriter 对象
    /// </summary>
    /// <param name="filepath"></param>
    public TextFileLogWriter( string filepath )
    {
      _filepath = filepath;
    }

    /// <summary>
    /// 确保文件存在
    /// </summary>
    public void EnsureFile()
    {

    }




  }
}
