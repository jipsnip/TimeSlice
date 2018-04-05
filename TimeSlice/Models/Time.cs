using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSlice.Models
{
    public class Time
    {
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public int pgId { get; set; }
        public string justification { get; set; }
        public int guId { get; set; }
        public int cpId { get; set; }

        public Time(DateTime _startTime, DateTime _endTime, int _pgId, String _justification, int _guId, int _cpId)
        {
            startTime = _startTime;
            endTime = _endTime;
            pgId = _pgId;
            justification = _justification;
            guId = _guId;
            cpId = _cpId;
        }
    }
}
