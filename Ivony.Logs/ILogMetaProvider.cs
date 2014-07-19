using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ivony.Logs
{
  public interface ILogMetaProvider
  {


    LogMeta GetLogMeta( LogType type );
  }
}
