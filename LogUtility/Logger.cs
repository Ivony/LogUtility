using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogUtility
{
  public class Logger
  {


    public object Log( object obj )
    {
      if ( obj == null )
        throw new ArgumentNullException( "obj" );

      if ( PipeLogger != null )
        return LogInternal( PipeLogger.LogInternal( obj ) );

      else
        return LogInternal( obj );
    }


    private object LogInternal( object obj )
    {
      var converted = Converter.Convert( obj );

      if ( converted == null )
        return null;

      try
      {
        var message = Format( converted, null );
        Writer.Write( message );
      }
      catch ( Exception e )
      {
        return e;
      }

      return converted;
    }

    private string Format( object obj, string format )
    {
      var formatter = FormatProvider.GetFormat( obj.GetType() );

      var customFormatter = formatter as ICustomFormatter;
      if ( customFormatter != null )
        return customFormatter.Format( format, obj, FormatProvider );

      var formattable = obj as IFormattable;
      if ( formattable != null )
        return formattable.ToString( format, FormatProvider );

      return obj.ToString();
    }


    public Logger PipeLogger
    {
      get;
      private set;
    }

    public IFormatProvider FormatProvider
    {
      get;
      private set;
    }

    public ILogWriter Writer
    {
      get;
      private set;
    }

    public IObjectConverter Converter
    {
      get;
      private set;
    }
  }
}
