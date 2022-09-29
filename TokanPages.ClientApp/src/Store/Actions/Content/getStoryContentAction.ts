import { AppThunkAction, CombinedDefaults } from "../../Configuration";
import { STORY_URL } from "../../../Shared/constants";
import { TErrorActions } from "../raiseErrorAction";
import { IDocumentContentDto } from "../../../Api/Models";
import { GetContent } from "./Services/getContentService";

export const REQUEST_STORY_CONTENT = "REQUEST_STORY_CONTENT";
export const RECEIVE_STORY_CONTENT = "RECEIVE_STORY_CONTENT";
export interface IRequestStoryContent { type: typeof REQUEST_STORY_CONTENT }
export interface IReceiveStoryContent { type: typeof RECEIVE_STORY_CONTENT, payload: IDocumentContentDto }
export type TKnownActions = IRequestStoryContent | IReceiveStoryContent | TErrorActions;

export const ActionCreators = 
{
    getStoryContent: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        const isLanguageChanged = getState().userLanguage.id !== getState().getStoryContent.content.language;

        if (getState().getStoryContent.content !== CombinedDefaults.getStoryContent.content && !isLanguageChanged) 
        {
            return;
        }

        GetContent(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST_STORY_CONTENT, 
            receive: RECEIVE_STORY_CONTENT, 
            url: STORY_URL 
        });
    }
}