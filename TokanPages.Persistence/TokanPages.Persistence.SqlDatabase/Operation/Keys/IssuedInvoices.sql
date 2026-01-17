ALTER TABLE [operation].[IssuedInvoices]  WITH CHECK ADD  CONSTRAINT [FK_IssuedInvoices_Users] FOREIGN KEY([UserId])
REFERENCES [operation].[Users] ([Id])

ALTER TABLE [operation].[IssuedInvoices] CHECK CONSTRAINT [FK_IssuedInvoices_Users]
