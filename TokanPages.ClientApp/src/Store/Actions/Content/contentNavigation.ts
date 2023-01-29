import { ApplicationAction, ApplicationDefault } from "../../Configuration";
import { GetContent, GET_NAVIGATION_CONTENT } from "../../../Api/Request";
import { NavigationContentDto } from "../../../Api/Models";

export const REQUEST = "REQUEST_NAVIGATION_CONTENT";
export const RECEIVE = "RECEIVE_NAVIGATION_CONTENT";
interface Request { type: typeof REQUEST }
interface Receive { type: typeof RECEIVE, payload: NavigationContentDto }
export type TKnownActions = Request | Receive;

export const ContentNavigationAction =
{
    get: (): ApplicationAction<TKnownActions> => (dispatch, getState) =>
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