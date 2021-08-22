import { Action, Reducer } from "redux";
import { combinedDefaults } from "../../combinedDefaults";
import { IGetUserSignupContent } from "../../../Redux/States/Content/getUserSignupContentState";
import { 
    TKnownActions, 
    REQUEST_USER_SIGNUP_CONTENT, 
    RECEIVE_USER_SIGNUP_CONTENT 
} from "../../Actions/Content/getUserSignupContentAction";

const GetUserSignupContentReducer: Reducer<IGetUserSignupContent> = (state: IGetUserSignupContent | undefined, incomingAction: Action): IGetUserSignupContent => 
{
    if (state === undefined) return combinedDefaults.getUserSignupContent;

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

export default GetUserSignupContentReducer;
