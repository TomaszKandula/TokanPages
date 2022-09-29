import { Action, Reducer } from "redux";
import { CombinedDefaults } from "../../Configuration";
import { IGetContactFormContent } from "../../States";
import { 
    TKnownActions, 
    REQUEST_CONTACT_FORM_CONTENT, 
    RECEIVE_CONTACT_FORM_CONTENT 
} from "../../Actions/Content/getContactFormContentAction";

export const GetContactFormContentReducer: 
    Reducer<IGetContactFormContent> = (state: IGetContactFormContent | undefined, incomingAction: Action): 
    IGetContactFormContent => 
{
    if (state === undefined) return CombinedDefaults.getContactFormContent;

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
