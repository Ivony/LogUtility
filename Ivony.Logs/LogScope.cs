using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ivony.Logs
{
  public class LogScope : IDisposable
  {

    private LogScope _parent;

    public LogScope( LogSource source )
    {

      Push( source );

    }

    protected void Push( LogSource source )
    {
      throw new NotImplementedException();
    }

    public void Dispose()
    {
      Pop();
    }

    private void Pop()
    {
      throw new NotImplementedException();
    }
  }
}
