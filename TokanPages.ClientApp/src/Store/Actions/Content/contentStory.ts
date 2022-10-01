import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { STORY_URL } from "../../../Shared/constants";
import { IDocumentContentDto } from "../../../Api/Models";
import { GetContentService } from "./Services/getContentService";

export const REQUEST_STORY_CONTENT = "REQUEST_STORY_CONTENT";
export const RECEIVE_STORY_CONTENT = "RECEIVE_STORY_CONTENT";
export interface IRequestStoryContent { type: typeof REQUEST_STORY_CONTENT }
export interface IReceiveStoryContent { type: typeof RECEIVE_STORY_CONTENT, payload: IDocumentContentDto }
export type TKnownActions = IRequestStoryContent | IReceiveStoryContent;

export const ContentStoryAction = 
{
    get: (): IApplicationAction<TKnownActions> => (dispatch, getState) =>
    {
        const content = getState().contentStory.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentStory.content;
        const isLanguageChanged = languageId !== content.language;

        if (isContentChanged && !isLanguageChanged) 
        {
            return;
        }

        GetContentService(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST_STORY_CONTENT, 
            receive: RECEIVE_STORY_CONTENT, 
            url: STORY_URL 
        });
    }
}