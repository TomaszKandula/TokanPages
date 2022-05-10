import { Action, Reducer } from "redux";
import { combinedDefaults } from "../../combinedDefaults";
import { IGetCookiesPromptContent } from "../../States/Content/getCookiesPromptContentState";
import { 
    TKnownActions, 
    REQUEST_COOKIES_PROMPT_CONTENT, 
    RECEIVE_COOKIES_PROMPT_CONTENT 
} from "../../Actions/Content/getCookiesPromptContentAction";

export const GetCookiesPromptContentReducer: Reducer<IGetCookiesPromptContent> = (state: IGetCookiesPromptContent | undefined, incomingAction: Action): IGetCookiesPromptContent => 
{
    if (state === undefined) return combinedDefaults.getCookiesPromptContent;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case REQUEST_COOKIES_PROMPT_CONTENT:
            return { 
                isLoading: true, 
                content: state.content
            };

        case RECEIVE_COOKIES_PROMPT_CONTENT:
            return { 
                isLoading: false, 
                content: action.payload.content
            };

        default: return state;
    }
}
