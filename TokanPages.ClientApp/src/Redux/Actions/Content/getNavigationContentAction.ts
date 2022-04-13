import axios from "axios";
import { AppThunkAction } from "../../applicationState";
import { combinedDefaults } from "../../../Redux/combinedDefaults";
import { UnexpectedStatusCode } from "../../../Shared/textWrappers";
import { RaiseError } from "../../../Shared/helpers";
import { GET_NAVIGATION_CONTENT, NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { TErrorActions } from "./../raiseErrorAction";
import { INavigationContentDto } from "../../../Api/Models";
import { EnrichConfiguration } from "../../../Api/Request";
import { GetUserLanguage } from "../../../Shared/Services/languageService";

export const REQUEST_NAVIGATION_CONTENT = "REQUEST_NAVIGATION_CONTENT";
export const RECEIVE_NAVIGATION_CONTENT = "RECEIVE_NAVIGATION_CONTENT";
export interface IRequestNavigationContent { type: typeof REQUEST_NAVIGATION_CONTENT }
export interface IReceiveNavigationContent { type: typeof RECEIVE_NAVIGATION_CONTENT, payload: INavigationContentDto }
export type TKnownActions = IRequestNavigationContent | IReceiveNavigationContent | TErrorActions;

export const ActionCreators =
{
    getNavigationContent: (isLanguageChanged: boolean = false): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        if (getState().getNavigationContent.content !== combinedDefaults.getNavigationContent.content && !isLanguageChanged)
            return;

        dispatch({ type: REQUEST_NAVIGATION_CONTENT });

        const language = GetUserLanguage();
        const queryParam = language === "" ? "" : `&language=${language}`;

        axios(EnrichConfiguration(
        {
            method: "GET",
            url: `${GET_NAVIGATION_CONTENT}${queryParam}`,
            responseType: "json"
        }))
        .then(response =>
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError(dispatch, NULL_RESPONSE_ERROR) 
                    : dispatch({ type: RECEIVE_NAVIGATION_CONTENT, payload: response.data }); 
            }

            RaiseError(dispatch, UnexpectedStatusCode(response.status));
        })
        .catch(error => 
        {
            RaiseError(dispatch, error);
        });
    }
}