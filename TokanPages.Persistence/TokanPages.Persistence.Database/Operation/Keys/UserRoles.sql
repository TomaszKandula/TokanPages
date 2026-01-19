ALTER TABLE [operation].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Roles] FOREIGN KEY([RoleId])
REFERENCES [operation].[Roles] ([Id])

ALTER TABLE [operation].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Roles]

ALTER TABLE [operation].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Users] FOREIGN KEY([UserId])
REFERENCES [operation].[Users] ([Id])

ALTER TABLE [operation].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Users]
