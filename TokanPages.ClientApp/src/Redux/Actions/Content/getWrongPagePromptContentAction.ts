import axios from "axios";
import { AppThunkAction } from "../../applicationState";
import { combinedDefaults } from "../../../Redux/combinedDefaults";
import { RaiseError } from "../../../Shared/helpers";
import { UnexpectedStatusCode } from "../../../Shared/textWrappers";
import { GET_WRONG_PAGE_PROMPT_CONTENT, NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { TErrorActions } from "./../raiseErrorAction";
import { IWrongPagePromptContentDto } from "../../../Api/Models";
import { EnrichConfiguration } from "../../../Api/Request";
import { GetUserLanguage } from "../../../Shared/Services/languageService";

export const REQUEST_WRONG_PAGE_CONTENT = "REQUEST_WRONG_PAGE_CONTENT";
export const RECEIVE_WRONG_PAGE_CONTENT = "RECEIVE_WRONG_PAGE_CONTENT";
export interface IRequestWrongPageContent { type: typeof REQUEST_WRONG_PAGE_CONTENT }
export interface IReceiveWrongPageContent { type: typeof RECEIVE_WRONG_PAGE_CONTENT, payload: IWrongPagePromptContentDto }
export type TKnownActions = IRequestWrongPageContent | IReceiveWrongPageContent | TErrorActions;

export const ActionCreators = 
{
    getWrongPagePromptContent: (isLanguageChanged: boolean = false): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        if (getState().getWrongPagePromptContent.content !== combinedDefaults.getWrongPagePromptContent.content && !isLanguageChanged)
            return;

        dispatch({ type: REQUEST_WRONG_PAGE_CONTENT });

        const language = GetUserLanguage();
        const queryParam = language === "" ? "" : `&language=${language}`;

        axios(EnrichConfiguration( 
        {
            method: "GET", 
            url: `${GET_WRONG_PAGE_PROMPT_CONTENT}${queryParam}`,
            responseType: "json"
        }))
        .then(response =>
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError(dispatch, NULL_RESPONSE_ERROR) 
                    : dispatch({ type: RECEIVE_WRONG_PAGE_CONTENT, payload: response.data });
            }

            RaiseError(dispatch, UnexpectedStatusCode(response.status));
        })
        .catch(error =>
        {
            RaiseError(dispatch, error);
        });
    }
}