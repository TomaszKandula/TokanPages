ALTER TABLE [operation].[DefaultPermissions]  WITH CHECK ADD  CONSTRAINT [FK_DefaultPermissions_Permissions] FOREIGN KEY([PermissionId])
REFERENCES [operation].[Permissions] ([Id])

ALTER TABLE [operation].[DefaultPermissions] CHECK CONSTRAINT [FK_DefaultPermissions_Permissions]

ALTER TABLE [operation].[DefaultPermissions]  WITH CHECK ADD  CONSTRAINT [FK_DefaultPermissions_Roles] FOREIGN KEY([RoleId])
REFERENCES [operation].[Roles] ([Id])

ALTER TABLE [operation].[DefaultPermissions] CHECK CONSTRAINT [FK_DefaultPermissions_Roles]
