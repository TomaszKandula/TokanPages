ALTER TABLE [operation].[UserBankAccounts]  WITH CHECK ADD  CONSTRAINT [FK_UserBankAccounts_Users] FOREIGN KEY([UserId])
REFERENCES [operation].[Users] ([Id])

ALTER TABLE [operation].[UserBankAccounts] CHECK CONSTRAINT [FK_UserBankAccounts_Users]
