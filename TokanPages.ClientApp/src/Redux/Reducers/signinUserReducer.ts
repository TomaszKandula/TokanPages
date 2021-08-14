import { Action, Reducer } from "redux";
import { combinedDefaults } from "../combinedDefaults";
import { ISigninUser } from "../States/signinUserState";
import { OperationStatus } from "../../Shared/enums";
import { 
    TKnownActions,
    SIGNIN_USER,
    SIGNIN_USER_RESPONSE
} from "../Actions/signinUserAction";

const SiginUserReducer: Reducer<ISigninUser> = (state: ISigninUser | undefined, incomingAction: Action): ISigninUser => 
{
    if (state === undefined) return combinedDefaults.signinUser;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case SIGNIN_USER:
            return { 
                operationStatus: OperationStatus.inProgress, 
                attachedErrorObject: state.attachedErrorObject,
                userData: state.userData
            };

        case SIGNIN_USER_RESPONSE:
            localStorage.setItem("userToken", action.payload.userToken);
            return { 
                operationStatus: OperationStatus.hasFinished, 
                attachedErrorObject: { },
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

export default SiginUserReducer;
