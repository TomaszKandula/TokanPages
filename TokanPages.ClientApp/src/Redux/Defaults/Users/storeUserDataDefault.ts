import { IStoreUserData } from "../../States/Users/storeUserDataState";

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
