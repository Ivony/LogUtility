using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogUtility
{
  public interface ILogWriterProvider
  {

    ILogWriter GetWriter( object obj, LogScope scope );

  }
}
