import { UserDataStoreState } from "../../States";

export const UserDataStore: UserDataStoreState = 
{
    isShown: false,
    userData: 
    {
        userId: "",
        aliasName: "",
        avatarName: "",
        firstName: "",
        lastName: "",
        email: "",
        shortBio: "",
        registered: "",
        tokenExpires: "",
        refreshTokenExpires: "",
        userToken: "",
        refreshToken: "",
        roles: [],
        permissions: []
    }
}
