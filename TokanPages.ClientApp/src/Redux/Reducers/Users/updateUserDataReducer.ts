import { Action, Reducer } from "redux";
import { combinedDefaults } from "../../combinedDefaults";
import { IUpdateUserData } from "../../../Redux/States/Users/updateUserDataState";
import { 
    TKnownActions,
    SHOW_USERDATA,
    CLEAR_USERDATA,
    UPDATE_USERDATA
} from "../../Actions/Users/updateUserDataAction";
import { USER_DATA } from "../../../Shared/constants";
import { DelDataFromStorage, SetDataInStorage } from "../../../Shared/helpers";

const UpdateUserDataReducer: Reducer<IUpdateUserData> = (state: IUpdateUserData | undefined, incomingAction: Action): IUpdateUserData => 
{
    if (state === undefined) return combinedDefaults.updateUserData;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case SHOW_USERDATA:
            return {
                isShown: action.payload,
                userData: state.userData
            };

        case CLEAR_USERDATA:
            DelDataFromStorage(USER_DATA);
            return combinedDefaults.updateUserData;

        case UPDATE_USERDATA:
            SetDataInStorage(action.payload, USER_DATA);
            return { 
                isShown: state.isShown,
                userData: {
                    userId: action.payload.userId,
                    aliasName: action.payload.aliasName,
                    avatarName: action.payload.avatarName,
                    firstName: action.payload.firstName,
                    lastName: action.payload.lastName,
                    shortBio: action.payload.shortBio,
                    registered: action.payload.registered,
                    roles: action.payload.roles,
                    permissions: action.payload.permissions
                }
            };

        default: return state;
    }
};

export default UpdateUserDataReducer;
