import { Action, Reducer } from "redux";
import { combinedDefaults } from "../../combinedDefaults";
import { IGetNewsletterContent } from "../../../Redux/States/Content/getNewsletterContentState";
import { 
    TKnownActions,
    RECEIVE_NEWSLETTER_CONTENT, 
    REQUEST_NEWSLETTER_CONTENT
} from "../../../Redux/Actions/Content/getNewsletterContentAction";

export const GetNewsletterContentReducer: Reducer<IGetNewsletterContent> = (state: IGetNewsletterContent | undefined, incomingAction: Action): IGetNewsletterContent => 
{
    if (state === undefined) return combinedDefaults.getNewsletterContent;

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
