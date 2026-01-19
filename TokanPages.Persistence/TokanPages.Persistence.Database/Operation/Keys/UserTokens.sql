ALTER TABLE [operation].[UserTokens]  WITH CHECK ADD  CONSTRAINT [FK_UserTokens_Users] FOREIGN KEY([UserId])
REFERENCES [operation].[Users] ([Id])

ALTER TABLE [operation].[UserTokens] CHECK CONSTRAINT [FK_UserTokens_Users]
