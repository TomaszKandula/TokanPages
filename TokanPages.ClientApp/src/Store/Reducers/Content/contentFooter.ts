import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { IContentFooter } from "../../States";
import { 
    TKnownActions,
    RECEIVE_FOOTER_CONTENT, 
    REQUEST_FOOTER_CONTENT
} from "../../Actions/Content/contentFooter";

export const ContentFooter: 
    Reducer<IContentFooter> = (state: IContentFooter | undefined, incomingAction: Action): 
    IContentFooter => 
{
    if (state === undefined) return ApplicationDefault.contentFooter;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case REQUEST_FOOTER_CONTENT:
            return { 
                isLoading: true, 
                content: state.content
            };

        case RECEIVE_FOOTER_CONTENT:
            return { 
                isLoading: false, 
                content: action.payload.content
            };

        default: return state;
    }
}
