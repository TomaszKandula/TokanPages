import { AppThunkAction, ApplicationDefaults } from "../../Configuration";
import { GET_NAVIGATION_CONTENT } from "../../../Shared/constants";
import { TErrorActions } from "../raiseErrorAction";
import { INavigationContentDto } from "../../../Api/Models";
import { GetContent } from "./Services/getContentService";

export const REQUEST_NAVIGATION_CONTENT = "REQUEST_NAVIGATION_CONTENT";
export const RECEIVE_NAVIGATION_CONTENT = "RECEIVE_NAVIGATION_CONTENT";
export interface IRequestNavigationContent { type: typeof REQUEST_NAVIGATION_CONTENT }
export interface IReceiveNavigationContent { type: typeof RECEIVE_NAVIGATION_CONTENT, payload: INavigationContentDto }
export type TKnownActions = IRequestNavigationContent | IReceiveNavigationContent | TErrorActions;

export const ActionCreators =
{
    getNavigationContent: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        const isLanguageChanged = getState().userLanguage.id !== getState().getNavigationContent.content.language;

        if (getState().getNavigationContent.content !== ApplicationDefaults.getNavigationContent.content && !isLanguageChanged)
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