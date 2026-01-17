ALTER TABLE [operation].[UserRefreshTokens]  WITH CHECK ADD  CONSTRAINT [FK_UserRefreshTokens_Users] FOREIGN KEY([UserId])
REFERENCES [operation].[Users] ([Id])

ALTER TABLE [operation].[UserRefreshTokens] CHECK CONSTRAINT [FK_UserRefreshTokens_Users]
