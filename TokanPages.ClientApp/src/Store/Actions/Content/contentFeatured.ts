import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { GetContent, GET_FEATURED_CONTENT } from "../../../Api/Request";
import { IFeaturedContentDto } from "../../../Api/Models";

export const REQUEST = "REQUEST_FEATURED_CONTENT";
export const RECEIVE = "RECEIVE_FEATURED_CONTENT";
interface IRequest { type: typeof REQUEST }
interface IReceive { type: typeof RECEIVE, payload: IFeaturedContentDto }
export type TKnownActions = IRequest | IReceive;

export const ContentFeaturedAction = 
{
    get: (): IApplicationAction<TKnownActions> => (dispatch, getState) =>
    {
        const content = getState().contentFeatured.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentFeatured.content;
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
            url: GET_FEATURED_CONTENT 
        });
    }
}