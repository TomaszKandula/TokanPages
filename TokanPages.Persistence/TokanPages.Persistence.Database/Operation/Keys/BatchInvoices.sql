ALTER TABLE [operation].[BatchInvoices]  WITH CHECK ADD  CONSTRAINT [FK_BatchInvoices_BatchInvoicesProcessing] FOREIGN KEY([ProcessBatchKey])
REFERENCES [operation].[BatchInvoicesProcessing] ([Id])

ALTER TABLE [operation].[BatchInvoices] CHECK CONSTRAINT [FK_BatchInvoices_BatchInvoicesProcessing]

ALTER TABLE [operation].[BatchInvoices]  WITH CHECK ADD  CONSTRAINT [FK_BatchInvoices_UserBankAccount] FOREIGN KEY([UserBankAccountId])
REFERENCES [operation].[UserBankAccounts] ([Id])

ALTER TABLE [operation].[BatchInvoices] CHECK CONSTRAINT [FK_BatchInvoices_UserBankAccount]

ALTER TABLE [operation].[BatchInvoices]  WITH CHECK ADD  CONSTRAINT [FK_BatchInvoices_UserCompanies] FOREIGN KEY([UserCompanyId])
REFERENCES [operation].[UserCompanies] ([Id])

ALTER TABLE [operation].[BatchInvoices] CHECK CONSTRAINT [FK_BatchInvoices_UserCompanies]

ALTER TABLE [operation].[BatchInvoices]  WITH CHECK ADD  CONSTRAINT [FK_BatchInvoices_Users] FOREIGN KEY([UserId])
REFERENCES [operation].[Users] ([Id])

ALTER TABLE [operation].[BatchInvoices] CHECK CONSTRAINT [FK_BatchInvoices_Users]
