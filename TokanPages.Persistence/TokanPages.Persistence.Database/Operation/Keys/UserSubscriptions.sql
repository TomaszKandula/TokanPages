ALTER TABLE [operation].[UserSubscriptions]  WITH CHECK ADD  CONSTRAINT [FK_UserSubscriptions_Users] FOREIGN KEY([UserId])
REFERENCES [operation].[Users] ([Id])

ALTER TABLE [operation].[UserSubscriptions] CHECK CONSTRAINT [FK_UserSubscriptions_Users]
