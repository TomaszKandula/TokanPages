ALTER TABLE [operation].[PushNotificationTags]  WITH CHECK ADD  CONSTRAINT [FK_PushNotificationTags_PushNotifications] FOREIGN KEY([PushNotificationId])
REFERENCES [operation].[PushNotifications] ([Id])

ALTER TABLE [operation].[PushNotificationTags] CHECK CONSTRAINT [FK_PushNotificationTags_PushNotifications]
