import { UserDataStoreState } from "../../States";

export const UserDataStore: UserDataStoreState = {
    isShown: false,
    userData: {
        userId: "",
        isVerified: false,
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
        permissions: [],
    },
};
