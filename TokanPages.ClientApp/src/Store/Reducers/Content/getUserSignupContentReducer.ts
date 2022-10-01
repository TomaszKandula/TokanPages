import { Action, Reducer } from "redux";
import { ApplicationDefaults } from "../../Configuration";
import { IGetUserSignupContent } from "../../States";
import { 
    TKnownActions, 
    REQUEST_USER_SIGNUP_CONTENT, 
    RECEIVE_USER_SIGNUP_CONTENT 
} from "../../Actions/Content/getUserSignupContentAction";

export const GetUserSignupContentReducer: 
    Reducer<IGetUserSignupContent> = (state: IGetUserSignupContent | undefined, incomingAction: Action): 
    IGetUserSignupContent => 
{
    if (state === undefined) return ApplicationDefaults.contentUserSignup;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case REQUEST_USER_SIGNUP_CONTENT:
            return { 
                isLoading: true, 
                content: state.content
            };

        case RECEIVE_USER_SIGNUP_CONTENT:
            return { 
                isLoading: false, 
                content: action.payload.content
            };

        default: return state;
    }
}
