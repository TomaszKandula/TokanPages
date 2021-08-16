import { Action, Reducer } from "redux";
import { combinedDefaults } from "../combinedDefaults";
import { IUpdateUserData } from "../../Redux/States/updateUserDataState";
import { 
    TKnownActions,
    CLEAR_USERDATA,
    UPDATE_USERDATA
} from "../Actions/updateUserDataAction";
import { USER_TOKEN } from "../../Shared/constants";

const UpdateUserDataReducer: Reducer<IUpdateUserData> = (state: IUpdateUserData | undefined, incomingAction: Action): IUpdateUserData => 
{
    if (state === undefined) return combinedDefaults.updateUserData;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case CLEAR_USERDATA:
            localStorage.removeItem(USER_TOKEN);
            return { 
                userData: state.userData
            };

        case UPDATE_USERDATA:
            localStorage.setItem(USER_TOKEN, action.payload.userToken);
            return { 
                userData: {
                    userId: action.payload.userId,
                    aliasName: action.payload.aliasName,
                    avatarName: action.payload.avatarName,
                    firstName: action.payload.firstName,
                    lastName: action.payload.lastName,
                    shortBio: action.payload.shortBio,
                    registered: action.payload.registered
                }
            };

        default: return state;
    }
};

export default UpdateUserDataReducer;
