using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmyMichalowice.Dto_s
{
    public class LogDto
    {
        //public LogLevelEnum Level { get; set; }
        public string Timestamp { get; set; }
        public string FileName { get; set; }
        public string LineNumber { get; set; }
        public  string Message { get; set; }
        public string UserId { get; set; }
    }

    public class MessageFromLog
    {
        public Headers Headers { get; set; }
        public string Message { get; set; }
        public string UserId { get; set; }

        public string ComponentName { get; set; }
        public int Status { get; set; }
        public string StatusText { get; set; }
        public string Url { get; set; }
        public bool Ok { get; set; }
        public string Name { get; set; }
        public Error Error { get; set; }
       

    }

    public class Error
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public string TraceId { get; set; }
    }

    public class Headers
    {
        public NormalizedNames  NormalizedNames{ get; set; }
        public string LazyUpdate { get; set; }
    }

    public class NormalizedNames
    {
        
    }
}
