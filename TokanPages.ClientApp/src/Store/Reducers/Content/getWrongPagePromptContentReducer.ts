import { Action, Reducer } from "redux";
import { combinedDefaults } from "../../combinedDefaults";
import { IGetWrongPagePromptContent } from "../../States/Content/getWrongPagePromptContentState";
import { 
    TKnownActions, 
    REQUEST_WRONG_PAGE_CONTENT, 
    RECEIVE_WRONG_PAGE_CONTENT 
} from "../../Actions/Content/getWrongPagePromptContentAction";

export const GetWrongPagePromptContentReducer: Reducer<IGetWrongPagePromptContent> = (state: IGetWrongPagePromptContent | undefined, incomingAction: Action): IGetWrongPagePromptContent => 
{
    if (state === undefined) return combinedDefaults.getWrongPagePromptContent;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case REQUEST_WRONG_PAGE_CONTENT:
            return { 
                isLoading: true, 
                content: state.content
            };

        case RECEIVE_WRONG_PAGE_CONTENT:
            return { 
                isLoading: false, 
                content: action.payload.content
            };

        default: return state;
    }
}
