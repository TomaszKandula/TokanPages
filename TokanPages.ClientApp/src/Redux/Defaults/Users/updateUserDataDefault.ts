import { IUpdateUserData } from "../../States/Users/updateUserDataState";

export const UpdateUserDataDefault: IUpdateUserData = 
{
    isShown: false,
    userData: 
    {
        userId: "",
        aliasName: "",
        avatarName: "",
        firstName: "",
        lastName: "",
        shortBio: "",
        registered: "",
        userToken: "",
        roles: [],
        permissions: []
    }
}
