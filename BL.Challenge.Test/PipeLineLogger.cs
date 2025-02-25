using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Challenge.Test
{
    public class PipelineLogger<T> : ILogger<T>, IDisposable
    {
        private readonly List<PipelineLogMessage> _messages = new List<PipelineLogMessage>();
        private readonly string _buildId;
        private readonly string _taskId;

        public PipelineLogger()
        {
            _buildId = Environment.GetEnvironmentVariable("BUILD_BUILDID") ?? "local";
            _taskId = Environment.GetEnvironmentVariable("SYSTEM_TASKINSTANCEID") ?? "unknown";
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var message = formatter(state, exception);

            // Format specifically for Azure Pipelines
            var formattedMessage = $"##vso[task.log detail.id={_taskId};filename={typeof(T).Name}] {message}";

            if (_buildId != "local")
                Console.WriteLine(formattedMessage);
            else
                Debug.WriteLine(formattedMessage);
            _messages.Add(new PipelineLogMessage
            {
                Level = logLevel,
                Message = message,
                BuildId = _buildId,
                TaskId = _taskId,
                Timestamp = DateTimeOffset.UtcNow
            });
        }

        public bool IsEnabled(LogLevel logLevel) => true;
        public IDisposable BeginScope<TState>(TState state) => this;
        public void Dispose() { }
    }

    public class PipelineLogMessage
    {
        public LogLevel Level { get; set; }
        public string Message { get; set; }
        public string BuildId { get; set; }
        public string TaskId { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }
}