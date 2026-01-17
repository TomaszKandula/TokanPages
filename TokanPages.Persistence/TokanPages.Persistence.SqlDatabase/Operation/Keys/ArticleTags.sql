ALTER TABLE [operation].[ArticleTags]  WITH CHECK ADD  CONSTRAINT [FK_ArticleTags_Articles] FOREIGN KEY([ArticleId])
REFERENCES [operation].[Articles] ([Id])

ALTER TABLE [operation].[ArticleTags] CHECK CONSTRAINT [FK_ArticleTags_Articles]
