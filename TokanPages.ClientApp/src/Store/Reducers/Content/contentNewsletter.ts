import { Action, Reducer } from "redux";
import { ApplicationDefaults } from "../../Configuration";
import { IContentNewsletter } from "../../States";
import { 
    TKnownActions,
    RECEIVE_NEWSLETTER_CONTENT, 
    REQUEST_NEWSLETTER_CONTENT
} from "../../Actions/Content/contentNewsletter";

export const ContentNewsletter: 
    Reducer<IContentNewsletter> = (state: IContentNewsletter | undefined, incomingAction: Action): 
    IContentNewsletter => 
{
    if (state === undefined) return ApplicationDefaults.contentNewsletter;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case REQUEST_NEWSLETTER_CONTENT:
            return { 
                isLoading: true, 
                content: state.content
            };

        case RECEIVE_NEWSLETTER_CONTENT:
            return { 
                isLoading: false, 
                content: action.payload.content
            };

        default: return state;
    }
}
