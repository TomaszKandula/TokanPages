ALTER TABLE [operation].[UserInformation]  WITH CHECK ADD  CONSTRAINT [FK_UserInfo_Users] FOREIGN KEY([UserId])
REFERENCES [operation].[Users] ([Id])

ALTER TABLE [operation].[UserInformation] CHECK CONSTRAINT [FK_UserInfo_Users]
