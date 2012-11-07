using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogUtility
{
  public interface ILogWriter
  {
    public string Write( string message, LogScope scope );
  }


  public class ConsoleLogWriter : ILogWriter
  {

    public string Write( string message, LogScope scope )
    {
      Console.WriteLine( message );
      return message;
    }
  }

}
