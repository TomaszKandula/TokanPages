ALTER TABLE [operation].[UserPermissions]  WITH CHECK ADD  CONSTRAINT [FK_UserPermissions_Permissions] FOREIGN KEY([PermissionId])
REFERENCES [operation].[Permissions] ([Id])

ALTER TABLE [operation].[UserPermissions] CHECK CONSTRAINT [FK_UserPermissions_Permissions]

ALTER TABLE [operation].[UserPermissions]  WITH CHECK ADD  CONSTRAINT [FK_UserPermissions_Users] FOREIGN KEY([UserId])
REFERENCES [operation].[Users] ([Id])

ALTER TABLE [operation].[UserPermissions] CHECK CONSTRAINT [FK_UserPermissions_Users]
