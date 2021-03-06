import { Action, Reducer } from "redux";
import { combinedDefaults } from "../combinedDefaults";
import { IGetContactFormContent } from "../States/getContactFormContentState";
import { 
    TKnownActions, 
    REQUEST_CONTACT_FORM_CONTENT, 
    RECEIVE_CONTACT_FORM_CONTENT 
} from "../Actions/getContactFormContentAction";

const GetContactFormContentReducer: Reducer<IGetContactFormContent> = (state: IGetContactFormContent | undefined, incomingAction: Action): IGetContactFormContent => 
{
    if (state === undefined) return combinedDefaults.getContactFormContent;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case REQUEST_CONTACT_FORM_CONTENT:
            return { 
                isLoading: true, 
                content: state.content
            };

        case RECEIVE_CONTACT_FORM_CONTENT:
            return { 
                isLoading: false, 
                content: action.payload.content
            };

        default: return state;
    }
}

export default GetContactFormContentReducer;
