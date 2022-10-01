import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { GET_NAVIGATION_CONTENT } from "../../../Shared/constants";
import { INavigationContentDto } from "../../../Api/Models";
import { GetContentService } from "./Services/getContentService";

export const REQUEST_NAVIGATION_CONTENT = "REQUEST_NAVIGATION_CONTENT";
export const RECEIVE_NAVIGATION_CONTENT = "RECEIVE_NAVIGATION_CONTENT";
export interface IRequestNavigationContent { type: typeof REQUEST_NAVIGATION_CONTENT }
export interface IReceiveNavigationContent { type: typeof RECEIVE_NAVIGATION_CONTENT, payload: INavigationContentDto }
export type TKnownActions = IRequestNavigationContent | IReceiveNavigationContent;

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

        GetContentService(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST_NAVIGATION_CONTENT, 
            receive: RECEIVE_NAVIGATION_CONTENT, 
            url: GET_NAVIGATION_CONTENT 
        });
    }
}