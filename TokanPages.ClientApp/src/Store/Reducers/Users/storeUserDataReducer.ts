import { Action, Reducer } from "redux";
import { CombinedDefaults } from "../../Configuration";
import { IStoreUserData } from "../../States";
import { 
    TKnownActions,
    SHOW_USERDATA,
    CLEAR_USERDATA,
    UPDATE_USERDATA
} from "../../Actions/Users/storeUserDataAction";
import { USER_DATA } from "../../../Shared/constants";
import { DelDataFromStorage, SetDataInStorage } from "../../../Shared/Services/StorageServices";

export const StoreUserDataReducer: 
    Reducer<IStoreUserData> = (state: IStoreUserData | undefined, incomingAction: Action): 
    IStoreUserData => 
{
    if (state === undefined) return CombinedDefaults.storeUserData;

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
            return CombinedDefaults.storeUserData;

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
