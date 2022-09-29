import { IStoreUserData } from "../../States";

export const StoreUserDataDefault: IStoreUserData = 
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
