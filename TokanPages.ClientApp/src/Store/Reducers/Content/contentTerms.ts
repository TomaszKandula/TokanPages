import { Action, Reducer } from "redux";
import { ApplicationDefaults } from "../../Configuration";
import { IContentTerms } from "../../States";
import { 
    TKnownActions,
    RECEIVE_TERMS_CONTENT, 
    REQUEST_TERMS_CONTENT
} from "../../Actions/Content/contentTerms";

export const ContentTerms: 
    Reducer<IContentTerms> = (state: IContentTerms | undefined, incomingAction: Action): 
    IContentTerms => 
{
    if (state === undefined) return ApplicationDefaults.contentTerms;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case REQUEST_TERMS_CONTENT:
            return { 
                isLoading: true, 
                content: state.content
            };

        case RECEIVE_TERMS_CONTENT:
            return { 
                isLoading: false, 
                content: action.payload.content
            };

        default: return state;
    }
}
