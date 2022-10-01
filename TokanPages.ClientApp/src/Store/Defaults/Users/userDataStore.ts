import { IUserDataStore } from "../../States";

export const UserDataStore: IUserDataStore = 
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
        userToken: "",
        refreshToken: "",
        roles: [],
        permissions: []
    }
}
