ALTER TABLE [operation].[Albums]  WITH CHECK ADD  CONSTRAINT [FK_Albums_UserPhotos] FOREIGN KEY([UserPhotoId])
REFERENCES [operation].[UserPhotos] ([Id])

ALTER TABLE [operation].[Albums] CHECK CONSTRAINT [FK_Albums_UserPhotos]

ALTER TABLE [operation].[Albums]  WITH CHECK ADD  CONSTRAINT [FK_Albums_Users] FOREIGN KEY([UserId])
REFERENCES [operation].[Users] ([Id])

ALTER TABLE [operation].[Albums] CHECK CONSTRAINT [FK_Albums_Users]
