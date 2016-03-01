using Amazon.CloudWatchLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.CloudWatchLogs.Model;

namespace Ivony.Logs.Aws
{
  public abstract class CloudWatchLoggerBase : Logger
  {

    public override void LogEntry( LogEntry entry )
    {

      Client.PutLogEvents( CreateRequest( entry ) );

    }


    protected abstract AmazonCloudWatchLogsClient Client { get; }


    protected virtual PutLogEventsRequest CreateRequest( LogEntry entry )
    {
      return new PutLogEventsRequest
      {
        LogEvents = new List<InputLogEvent> { CreateLogEvent( entry ) },
        LogGroupName = GetGroupName( entry ),
        LogStreamName = GetStreamName( entry ),
      };
    }

    protected abstract string GetStreamName( LogEntry entry );

    protected abstract string GetGroupName( LogEntry entry );

    protected virtual InputLogEvent CreateLogEvent( LogEntry entry )
    {
      return new InputLogEvent
      {
        Message = entry.Message,
        Timestamp = entry.LogDate,
      };
    }







  }
}
