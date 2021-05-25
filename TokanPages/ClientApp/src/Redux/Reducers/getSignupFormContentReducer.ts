import { Action, Reducer } from "redux";
import { combinedDefaults } from "../combinedDefaults";
import { IGetSignupFormContent } from "../../Redux/States/getSignupFormContentState";
import { 
    TKnownActions, 
    REQUEST_SIGNUP_FORM_CONTENT, 
    RECEIVE_SIGNUP_FORM_CONTENT 
} from "../../Redux/Actions/getSignupFormContentAction";

const GetSignupFormContentReducer: Reducer<IGetSignupFormContent> = (state: IGetSignupFormContent | undefined, incomingAction: Action): IGetSignupFormContent => 
{
    if (state === undefined) return combinedDefaults.getSignupFormContent;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case REQUEST_SIGNUP_FORM_CONTENT:
            return { 
                isLoading: true, 
                content: state.content
            };

        case RECEIVE_SIGNUP_FORM_CONTENT:
            return { 
                isLoading: false, 
                content: action.payload.content
            };

        default: return state;
    }
}

export default GetSignupFormContentReducer;
