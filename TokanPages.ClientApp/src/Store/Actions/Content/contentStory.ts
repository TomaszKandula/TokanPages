import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { GetContent, GET_STORY_CONTENT } from "../../../Api/Request";
import { IDocumentContentDto } from "../../../Api/Models";

export const REQUEST = "REQUEST_STORY_CONTENT";
export const RECEIVE = "RECEIVE_STORY_CONTENT";
interface IRequest { type: typeof REQUEST }
interface IReceive { type: typeof RECEIVE, payload: IDocumentContentDto }
export type TKnownActions = IRequest | IReceive;

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