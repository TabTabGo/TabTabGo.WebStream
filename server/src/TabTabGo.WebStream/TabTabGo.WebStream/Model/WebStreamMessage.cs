using System;
using System.Collections.Generic;
using System.Text;

namespace TabTabGo.WebStream.Model
{
    public class WebStreamMessage
    {
        public string EventName { get; set; } 
        public object Data { get; set; }
    }
}
