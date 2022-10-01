import { IApplicationAction, ApplicationDefaults } from "../../Configuration";
import { GET_NAVIGATION_CONTENT } from "../../../Shared/constants";
import { INavigationContentDto } from "../../../Api/Models";
import { GetContent } from "./Services/getContentService";

export const REQUEST_NAVIGATION_CONTENT = "REQUEST_NAVIGATION_CONTENT";
export const RECEIVE_NAVIGATION_CONTENT = "RECEIVE_NAVIGATION_CONTENT";
export interface IRequestNavigationContent { type: typeof REQUEST_NAVIGATION_CONTENT }
export interface IReceiveNavigationContent { type: typeof RECEIVE_NAVIGATION_CONTENT, payload: INavigationContentDto }
export type TKnownActions = IRequestNavigationContent | IReceiveNavigationContent;

export const ContentNavigationAction =
{
    get: (): IApplicationAction<TKnownActions> => (dispatch, getState) =>
    {
        const isLanguageChanged = getState().applicationLanguage.id !== getState().contentNavigation.content.language;

        if (getState().contentNavigation.content !== ApplicationDefaults.contentNavigation.content && !isLanguageChanged)
        {
            return;
        }

        GetContent(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST_NAVIGATION_CONTENT, 
            receive: RECEIVE_NAVIGATION_CONTENT, 
            url: GET_NAVIGATION_CONTENT 
        });
    }
}