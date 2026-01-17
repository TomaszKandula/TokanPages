ALTER TABLE [operation].[ArticleCounts]  WITH CHECK ADD  CONSTRAINT [FK_ArticleCounts_Articles] FOREIGN KEY([ArticleId])
REFERENCES [operation].[Articles] ([Id])

ALTER TABLE [operation].[ArticleCounts] CHECK CONSTRAINT [FK_ArticleCounts_Articles]

ALTER TABLE [operation].[ArticleCounts]  WITH CHECK ADD  CONSTRAINT [FK_ArticleCounts_Users] FOREIGN KEY([UserId])
REFERENCES [operation].[Users] ([Id])

ALTER TABLE [operation].[ArticleCounts] CHECK CONSTRAINT [FK_ArticleCounts_Users]
