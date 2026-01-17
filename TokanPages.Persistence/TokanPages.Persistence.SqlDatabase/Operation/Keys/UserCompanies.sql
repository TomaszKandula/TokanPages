ALTER TABLE [operation].[UserCompanies]  WITH CHECK ADD  CONSTRAINT [FK_UserCompanies_Users] FOREIGN KEY([UserId])
REFERENCES [operation].[Users] ([Id])

ALTER TABLE [operation].[UserCompanies] CHECK CONSTRAINT [FK_UserCompanies_Users]
