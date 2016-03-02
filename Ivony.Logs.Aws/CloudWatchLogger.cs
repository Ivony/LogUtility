using Amazon.CloudWatchLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.CloudWatchLogs.Model;

namespace Ivony.Logs.Aws
{




  public class CloudWatchLogger : Logger
  {



    public override void LogEntry( LogEntry entry )
    {
      lock ( SyncRoot )
      {
        var response = Client.PutLogEvents( CreateRequest( entry ) );
        SequenceToken = response.NextSequenceToken;
      }
    }



    protected CloudWatchLogger( AmazonCloudWatchLogsClient client, string groupName, string streamName, string sequenceToken = null )
    {
      Client = client;
      GroupName = groupName;
      StreamName = streamName;
      SequenceToken = sequenceToken;
    }


    /// <summary>
    /// 获取 AWS 客户端
    /// </summary>
    protected AmazonCloudWatchLogsClient Client { get; private set; }


    /// <summary>
    /// 日志序列标识
    /// </summary>
    protected string SequenceToken { get; private set; }


    /// <summary>
    /// 日志组名称
    /// </summary>
    protected string GroupName { get; private set; }


    /// <summary>
    /// 日志流名称
    /// </summary>
    protected string StreamName { get; private set; }


    /// <summary>
    /// 创建 LogEvent 对象
    /// </summary>
    /// <param name="entry"></param>
    /// <returns></returns>
    protected virtual InputLogEvent CreateLogEvent( LogEntry entry )
    {
      return new InputLogEvent
      {
        Message = entry.Message,
        Timestamp = entry.LogDate,
      };
    }

    /// <summary>
    /// 创建日志记录请求
    /// </summary>
    /// <param name="entry"></param>
    /// <returns></returns>
    protected virtual PutLogEventsRequest CreateRequest( LogEntry entry )
    {
      return new PutLogEventsRequest
      {
        LogEvents = new List<InputLogEvent> { CreateLogEvent( entry ) },
        LogGroupName = GroupName,
        LogStreamName = StreamName,
        SequenceToken = SequenceToken,
      };
    }




    /// <summary>
    /// 创建一个新的日志流并开始记录
    /// </summary>
    /// <param name="client">AWS 客户端</param>
    /// <param name="groupName">日志组名称</param>
    /// <param name="streamName">日志流名称</param>
    /// <returns></returns>
    public static CloudWatchLogger CreateStream( AmazonCloudWatchLogsClient client, string groupName, string streamName )
    {

      var response = client.CreateLogStream( new CreateLogStreamRequest { LogGroupName = groupName, LogStreamName = streamName } );
      return new CloudWatchLogger( client, groupName, streamName );

    }



    /// <summary>
    /// 附加到现有日志流并开始记录
    /// </summary>
    /// <param name="client">AWS 客户端</param>
    /// <param name="groupName">日志组名称</param>
    /// <param name="streamName">日志流名称</param>
    /// <returns></returns>
    public static CloudWatchLogger AppendStream( AmazonCloudWatchLogsClient client, string groupName, string streamName )
    {

      var response = client.DescribeLogStreams( new DescribeLogStreamsRequest { OrderBy = OrderBy.LogStreamName, LogGroupName = groupName, LogStreamNamePrefix = streamName, Limit = 1 } );
      var streamInfo = response.LogStreams.FirstOrDefault();

      if ( streamInfo == null || streamInfo.LogStreamName != streamName )
        return CreateStream( client, groupName, streamName );

      return new CloudWatchLogger( client, groupName, streamName, streamInfo.UploadSequenceToken );
    }
  }
}
