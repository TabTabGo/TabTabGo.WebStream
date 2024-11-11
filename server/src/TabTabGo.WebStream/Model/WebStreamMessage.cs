using System;

namespace TabTabGo.WebStream.Model
{
    public class WebStreamMessage : ICloneable
    {

        public Guid Id { set; get; }= Guid.NewGuid(); 
        public WebStreamMessage(string eventName, object data)
        {
            this.EventName = eventName;
            this.Data = data;
        }
        public string EventName { get; private set; }
        public object Data { get; private set; }

        /// <summary>
        /// clone this message and cjange id and eventName
        /// <br/> this will return new message without change current message
        /// </summary> 
        public WebStreamMessage CreateNewEventFromData(string eventName)
        {
            return new WebStreamMessage(eventName, Data);
        }
        /// <summary>
        /// clone this message and change id and data
        /// <br/> this will return new message without change current message
        /// </summary> 
        public WebStreamMessage CreateMessageWithData(object data)
        {
            return new WebStreamMessage(EventName, data);
        }

        /// <summary>
        /// clone  this message with new Id
        /// </summary> 
        public WebStreamMessage Clone()
        {
            return new WebStreamMessage(EventName, Data);
        }
        /// <summary>
        /// clone this message with new Id
        /// </summary>
        object ICloneable.Clone()
        {
            return new WebStreamMessage(EventName, Data);
        }
    }
}
