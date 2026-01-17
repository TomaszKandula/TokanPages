ALTER TABLE [operation].[CategoryNames]  WITH CHECK ADD  CONSTRAINT [FK_CategoryName_ArticleCategory] FOREIGN KEY([ArticleCategoryId])
REFERENCES [operation].[ArticleCategories] ([Id])

ALTER TABLE [operation].[CategoryNames] CHECK CONSTRAINT [FK_CategoryName_ArticleCategory]

ALTER TABLE [operation].[CategoryNames]  WITH CHECK ADD  CONSTRAINT [FK_CategoryName_Languages] FOREIGN KEY([LanguageId])
REFERENCES [operation].[Languages] ([Id])

ALTER TABLE [operation].[CategoryNames] CHECK CONSTRAINT [FK_CategoryName_Languages]
