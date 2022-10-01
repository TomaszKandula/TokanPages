import { AppThunkAction, ApplicationDefaults } from "../../Configuration";
import { STORY_URL } from "../../../Shared/constants";
import { IDocumentContentDto } from "../../../Api/Models";
import { GetContent } from "./Services/getContentService";

export const REQUEST_STORY_CONTENT = "REQUEST_STORY_CONTENT";
export const RECEIVE_STORY_CONTENT = "RECEIVE_STORY_CONTENT";
export interface IRequestStoryContent { type: typeof REQUEST_STORY_CONTENT }
export interface IReceiveStoryContent { type: typeof RECEIVE_STORY_CONTENT, payload: IDocumentContentDto }
export type TKnownActions = IRequestStoryContent | IReceiveStoryContent;

export const ContentStoryAction = 
{
    get: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        const isLanguageChanged = getState().applicationLanguage.id !== getState().contentStory.content.language;

        if (getState().contentStory.content !== ApplicationDefaults.contentStory.content && !isLanguageChanged) 
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