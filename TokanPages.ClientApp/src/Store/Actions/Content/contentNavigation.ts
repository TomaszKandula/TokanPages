import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { GetContent, GET_NAVIGATION_CONTENT } from "../../../Api/Request";
import { INavigationContentDto } from "../../../Api/Models";

export const REQUEST = "REQUEST_NAVIGATION_CONTENT";
export const RECEIVE = "RECEIVE_NAVIGATION_CONTENT";
interface IRequest { type: typeof REQUEST }
interface IReceive { type: typeof RECEIVE, payload: INavigationContentDto }
export type TKnownActions = IRequest | IReceive;

export const ContentNavigationAction =
{
    get: (): IApplicationAction<TKnownActions> => (dispatch, getState) =>
    {
        const content = getState().contentNavigation.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentNavigation.content;
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
            url: GET_NAVIGATION_CONTENT 
        });
    }
}