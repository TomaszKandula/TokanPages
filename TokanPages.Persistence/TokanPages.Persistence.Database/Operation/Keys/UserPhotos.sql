ALTER TABLE [operation].[UserPhotos]  WITH CHECK ADD  CONSTRAINT [FK_UserPhotos_PhotoCategories] FOREIGN KEY([PhotoCategoryId])
REFERENCES [operation].[PhotoCategories] ([Id])

ALTER TABLE [operation].[UserPhotos] CHECK CONSTRAINT [FK_UserPhotos_PhotoCategories]

ALTER TABLE [operation].[UserPhotos]  WITH CHECK ADD  CONSTRAINT [FK_UserPhotos_PhotoGears] FOREIGN KEY([PhotoGearId])
REFERENCES [operation].[PhotoGears] ([Id])

ALTER TABLE [operation].[UserPhotos] CHECK CONSTRAINT [FK_UserPhotos_PhotoGears]

ALTER TABLE [operation].[UserPhotos]  WITH CHECK ADD  CONSTRAINT [FK_UserPhotos_Users] FOREIGN KEY([UserId])
REFERENCES [operation].[Users] ([Id])

ALTER TABLE [operation].[UserPhotos] CHECK CONSTRAINT [FK_UserPhotos_Users]
