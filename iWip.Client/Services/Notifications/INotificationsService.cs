/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

using iWip.Client.Models.Notification;

namespace iWip.Client.Services;

public interface INotificationsService
{
    Task<IEnumerable<NotificationModel>> GetNotifications();
    Task<IEnumerable<NotificationModel>> GetActiveNotifications();
}