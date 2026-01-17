ALTER TABLE [operation].[UserPaymentsHistory]  WITH CHECK ADD  CONSTRAINT [FK_UserPaymentsHistory_Users] FOREIGN KEY([UserId])
REFERENCES [operation].[Users] ([Id])

ALTER TABLE [operation].[UserPaymentsHistory] CHECK CONSTRAINT [FK_UserPaymentsHistory_Users]
