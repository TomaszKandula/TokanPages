import axios from "axios";
import { AppThunkAction } from "../../applicationState";
import { combinedDefaults } from "../../../Redux/combinedDefaults";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { TERMS_URL, NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { TErrorActions } from "./../raiseErrorAction";
import { IDocumentContentDto } from "../../../Api/Models";
import { EnrichConfiguration } from "../../../Api/Request";
import { GetUserLanguage } from "../../../Shared/Services/languageService";

export const REQUEST_TERMS_CONTENT = "REQUEST_TERMS_CONTENT";
export const RECEIVE_TERMS_CONTENT = "RECEIVE_TERMS_CONTENT";
export interface IRequestTermsContent { type: typeof REQUEST_TERMS_CONTENT }
export interface IReceivetermsContent { type: typeof RECEIVE_TERMS_CONTENT, payload: IDocumentContentDto }
export type TKnownActions = IRequestTermsContent | IReceivetermsContent | TErrorActions;

export const ActionCreators = 
{
    getTermsContent: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        const isLanguageChanged = getState().userLanguage.id !== getState().getTermsContent.content.language;

        if (getState().getTermsContent.content !== combinedDefaults.getTermsContent.content && !isLanguageChanged) 
            return;

        dispatch({ type: REQUEST_TERMS_CONTENT });

        const language = GetUserLanguage();
        const queryParam = language === "" ? "" : `&language=${language}`;

        axios(EnrichConfiguration(
        {
            method: "GET", 
            url: `${TERMS_URL}${queryParam}`,
            responseType: "json"
        }))
        .then(response =>
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError({ dispatch, errorObject: NULL_RESPONSE_ERROR }) 
                    : dispatch({ type: RECEIVE_TERMS_CONTENT, payload: response.data });
            }

            RaiseError({ dispatch, errorObject: GetTextStatusCode({ statusCode: response.status }) });
        })
        .catch(error =>
        {
            RaiseError({ dispatch: dispatch, errorObject: error });
        });
    }
}