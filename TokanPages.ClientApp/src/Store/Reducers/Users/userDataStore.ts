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
    SHOW_USERDATA,
    CLEAR_USERDATA,
    UPDATE_USERDATA
} from "../../Actions/Users/userDataStore";

export const UserDataStore: 
    Reducer<IUserDataStore> = (state: IUserDataStore | undefined, incomingAction: Action): 
    IUserDataStore => 
{
    if (state === undefined) return ApplicationDefault.userDataStore;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case SHOW_USERDATA:
            return {
                isShown: action.payload,
                userData: state.userData
            };

        case CLEAR_USERDATA:
            DelDataFromStorage({ key: USER_DATA });
            return ApplicationDefault.userDataStore;

        case UPDATE_USERDATA:
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
                    userToken: state.userData.userToken,
                    refreshToken: state.userData.refreshToken,
                    roles: action.payload.roles,
                    permissions: action.payload.permissions
                }
            };

        default: return state;
    }
};
