using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using TabTabGo.WebStream.Notification.Entities.Enums;

namespace TabTabGo.WebStream.Notification.DTOs;

public class NotificationMessageDto
{
    
    public static NotificationMessageDto Map(Entities.NotificationMessage message,  DateTime notifiedDateTime ,string? notifiedBy = null)
    {
        return new NotificationMessageDto(message, notifiedDateTime, notifiedBy);
    }
    public static NotificationMessageDto Map(Entities.NotificationUser notificationUser)
    {
        return new NotificationMessageDto(notificationUser);
    }
    public NotificationMessageDto(Entities.NotificationMessage message, DateTime notifiedDateTime ,string? notifiedBy = null) 
    {
        Id = message.Id;
        Status = NotificationUserStatus.Unread;
        NotifiedDateTime = notifiedDateTime;
        Message = message.Message;
        CreatedBy = notifiedBy;
        CreatedDate = DateTimeOffset.UtcNow;
    }
    public NotificationMessageDto(Entities.NotificationUser notificationUser) 
    {
        Id = notificationUser.NotificationMessage.Id;
        Status = notificationUser.Status;
        NotifiedDateTime = notificationUser.NotifiedDateTime;
        ReadDateTime = notificationUser.ReadDateTime;
        Message = notificationUser.NotificationMessage.Message;
        EventName = notificationUser.NotificationMessage.EventName;
        CreatedBy = notificationUser.NotificationMessage.CreatedBy;
        CreatedDate = notificationUser.NotificationMessage.CreatedDate;
        ExtraProperties = notificationUser.NotificationMessage.ExtraProperties;
        UpdatedBy = notificationUser.NotificationMessage.UpdatedBy;
        UpdatedDate = notificationUser.NotificationMessage.UpdatedDate;
    }

    public Guid Id { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public NotificationUserStatus Status { get; set; }

    public DateTime NotifiedDateTime { get; set; }
    public DateTime? ReadDateTime { get; set; }
    public object Message { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string EventName { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string CreatedBy { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public DateTimeOffset CreatedDate { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public IDictionary<string, object> ExtraProperties { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string UpdatedBy { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public DateTimeOffset UpdatedDate { get; set; }
}