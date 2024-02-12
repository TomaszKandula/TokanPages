export const AuthorName = (firstName: string, lastName: string, aliasName: string) => {
    const fullNameWithAlias = `${firstName} '${aliasName}' ${lastName}`;
    return firstName && lastName ? fullNameWithAlias : aliasName;
};
