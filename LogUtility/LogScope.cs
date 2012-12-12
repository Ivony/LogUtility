using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogUtility
{
  public class LogScope
  {
    public LogScope( string name ) : this( name, null ) { }

    public LogScope( string name, LogScope parent )
    {
      Name = name;
      ParentScope = parent;
    }

    public LogScope ParentScope
    {
      get;
      private set;
    }

    public string Name
    {
      get;
      private set;
    }
  }
}
