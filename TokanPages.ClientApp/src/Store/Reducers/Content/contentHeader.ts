import { Action, Reducer } from "redux";
import { ApplicationDefaults } from "../../Configuration";
import { IContentHeader } from "../../States";
import { 
    TKnownActions,
    RECEIVE_HEADER_CONTENT, 
    REQUEST_HEADER_CONTENT
} from "../../Actions/Content/contentHeader";

export const ContentHeader: 
    Reducer<IContentHeader> = (state: IContentHeader | undefined, incomingAction: Action): 
    IContentHeader => 
{
    if (state === undefined) return ApplicationDefaults.contentHeader;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case REQUEST_HEADER_CONTENT:
            return { 
                isLoading: true, 
                content: state.content
            };

        case RECEIVE_HEADER_CONTENT:
            return { 
                isLoading: false, 
                content: action.payload.content
            };

        default: return state;
    }
}
