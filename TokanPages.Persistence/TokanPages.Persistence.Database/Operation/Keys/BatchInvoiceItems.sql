ALTER TABLE [operation].[BatchInvoiceItems]  WITH CHECK ADD  CONSTRAINT [FK_BatchInvoiceItems_BatchInvoices] FOREIGN KEY([BatchInvoiceId])
REFERENCES [operation].[BatchInvoices] ([Id])

ALTER TABLE [operation].[BatchInvoiceItems] CHECK CONSTRAINT [FK_BatchInvoiceItems_BatchInvoices]
