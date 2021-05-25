import { Action, Reducer } from "redux";
import { combinedDefaults } from "../combinedDefaults";
import { IGetSigninFormContent } from "../../Redux/States/getSigninFormContentState";
import { 
    TKnownActions, 
    REQUEST_SIGNIN_FORM_CONTENT, 
    RECEIVE_SIGNIN_FORM_CONTENT 
} from "../../Redux/Actions/getSigninFormContentAction";

const GetSigninFormContentReducer: Reducer<IGetSigninFormContent> = (state: IGetSigninFormContent | undefined, incomingAction: Action): IGetSigninFormContent => 
{
    if (state === undefined) return combinedDefaults.getSigninFormContent;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case REQUEST_SIGNIN_FORM_CONTENT:
            return { 
                isLoading: true, 
                content: state.content
            };

        case RECEIVE_SIGNIN_FORM_CONTENT:
            return { 
                isLoading: false, 
                content: action.payload.content
            };

        default: return state;
    }
}

export default GetSigninFormContentReducer;
