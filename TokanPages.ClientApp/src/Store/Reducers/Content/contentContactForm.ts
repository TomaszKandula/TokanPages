import { Action, Reducer } from "redux";
import { ApplicationDefaults } from "../../Configuration";
import { IContentContactForm } from "../../States";
import { 
    TKnownActions, 
    REQUEST_CONTACT_FORM_CONTENT, 
    RECEIVE_CONTACT_FORM_CONTENT 
} from "../../Actions/Content/getContactFormContentAction";

export const ContentContactForm: 
    Reducer<IContentContactForm> = (state: IContentContactForm | undefined, incomingAction: Action): 
    IContentContactForm => 
{
    if (state === undefined) return ApplicationDefaults.contentContactForm;

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
