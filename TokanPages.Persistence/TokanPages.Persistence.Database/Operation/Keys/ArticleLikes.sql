ALTER TABLE [operation].[ArticleLikes]  WITH CHECK ADD  CONSTRAINT [FK_ArticleLikes_Articles] FOREIGN KEY([ArticleId])
REFERENCES [operation].[Articles] ([Id])

ALTER TABLE [operation].[ArticleLikes] CHECK CONSTRAINT [FK_ArticleLikes_Articles]

ALTER TABLE [operation].[ArticleLikes]  WITH CHECK ADD  CONSTRAINT [FK_ArticleLikes_Users] FOREIGN KEY([UserId])
REFERENCES [operation].[Users] ([Id])

ALTER TABLE [operation].[ArticleLikes] CHECK CONSTRAINT [FK_ArticleLikes_Users]
