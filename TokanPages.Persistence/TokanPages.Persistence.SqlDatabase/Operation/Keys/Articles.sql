ALTER TABLE [operation].[Articles]  WITH CHECK ADD  CONSTRAINT [FK_Articles_ArticleCategory] FOREIGN KEY([CategoryId])
REFERENCES [operation].[ArticleCategories] ([Id])

ALTER TABLE [operation].[Articles] CHECK CONSTRAINT [FK_Articles_ArticleCategory]

ALTER TABLE [operation].[Articles]  WITH CHECK ADD  CONSTRAINT [FK_Articles_Users] FOREIGN KEY([UserId])
REFERENCES [operation].[Users] ([Id])

ALTER TABLE [operation].[Articles] CHECK CONSTRAINT [FK_Articles_Users]
