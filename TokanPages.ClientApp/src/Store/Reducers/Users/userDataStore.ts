import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { IUserDataStore } from "../../States";
import { USER_DATA } from "../../../Shared/constants";

import { 
    DelDataFromStorage, 
    SetDataInStorage 
} from "../../../Shared/Services/StorageServices";

import { 
    TKnownActions,
    SHOW,
    CLEAR,
    UPDATE
} from "../../Actions/Users/userDataStore";

export const UserDataStore: 
    Reducer<IUserDataStore> = (state: IUserDataStore | undefined, incomingAction: Action): 
    IUserDataStore => 
{
    if (state === undefined) return ApplicationDefault.userDataStore;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case SHOW:
            return {
                isShown: action.payload,
                userData: state.userData
            };

        case CLEAR:
            DelDataFromStorage({ key: USER_DATA });
            return ApplicationDefault.userDataStore;

        case UPDATE:
            SetDataInStorage({ key: USER_DATA, selection: action.payload }); 
            return { 
                isShown: state.isShown,
                userData: {
                    userId: action.payload.userId,
                    aliasName: action.payload.aliasName,
                    avatarName: action.payload.avatarName,
                    firstName: action.payload.firstName,
                    lastName: action.payload.lastName,
                    email: action.payload.email,
                    shortBio: action.payload.shortBio,
                    registered: action.payload.registered,
                    tokenExpires: action.payload.tokenExpires,
                    refreshTokenExpires: action.payload.refreshTokenExpires,
                    userToken: action.payload.userToken,
                    refreshToken: action.payload.refreshToken,
                    roles: action.payload.roles,
                    permissions: action.payload.permissions
                }
            };

        default: return state;
    }
};
