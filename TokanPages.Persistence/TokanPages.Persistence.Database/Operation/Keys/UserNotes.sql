ALTER TABLE [operation].[UserNotes]  WITH CHECK ADD  CONSTRAINT [FK_UserNote_Users] FOREIGN KEY([UserId])
REFERENCES [operation].[Users] ([Id])

ALTER TABLE [operation].[UserNotes] CHECK CONSTRAINT [FK_UserNote_Users]
