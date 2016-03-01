using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.CloudWatchLogs;

namespace Ivony.Logs.Aws
{
  public class CloudWatchLogger : CloudWatchLoggerBase
  {



    public CloudWatchLogger( AmazonCloudWatchLogsClient client, string groupName, string streamName )
    {
      _client = client;
      GroupName = groupName;
      StreamName = streamName;
    }


    public string GroupName { get; private set; }

    public string StreamName { get; private set; }



    private AmazonCloudWatchLogsClient _client;

    protected override AmazonCloudWatchLogsClient Client { get { return _client; } }


    protected override string GetGroupName( LogEntry entry )
    {
      return GroupName;
    }

    protected override string GetStreamName( LogEntry entry )
    {
      return StreamName;
    }
  }
}
