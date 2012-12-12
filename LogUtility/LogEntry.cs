using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogUtility
{
  public class LogEntry
  {

    public LogEntry( string message, LogScope scope, object raw = null )
    {
      LogDate = DateTime.UtcNow;
      Message = message;
      Scope = scope;
      RawObject = raw;
    }


    public DateTime LogDate
    {
      get;
      private set;
    }

    public LogScope Scope
    {
      get;
      private set;
    }

    public string Message
    {
      get;
      private set;
    }

    public object RawObject
    {
      get;
      private set;
    }

  }
}
