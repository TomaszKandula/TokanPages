ALTER TABLE [operation].[UserPayments]  WITH CHECK ADD  CONSTRAINT [FK_UserPayments_Users] FOREIGN KEY([UserId])
REFERENCES [operation].[Users] ([Id])

ALTER TABLE [operation].[UserPayments] CHECK CONSTRAINT [FK_UserPayments_Users]
