import { ApplicationAction, ApplicationDefault } from "../../Configuration";
import { GetContent, GET_STORY_CONTENT } from "../../../Api/Request";
import { DocumentContentDto } from "../../../Api/Models";

export const REQUEST = "REQUEST_STORY_CONTENT";
export const RECEIVE = "RECEIVE_STORY_CONTENT";
interface Request { type: typeof REQUEST }
interface Receive { type: typeof RECEIVE, payload: DocumentContentDto }
export type TKnownActions = Request | Receive;

export const ContentStoryAction = 
{
    get: (): ApplicationAction<TKnownActions> => (dispatch, getState) =>
    {
        const content = getState().contentStory.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentStory.content;
        const isLanguageChanged = languageId !== content.language;

        if (isContentChanged && !isLanguageChanged) 
        {
            return;
        }

        GetContent(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST, 
            receive: RECEIVE, 
            url: GET_STORY_CONTENT 
        });
    }
}