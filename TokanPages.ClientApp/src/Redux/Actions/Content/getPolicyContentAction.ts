import axios from "axios";
import { AppThunkAction } from "../../applicationState";
import { combinedDefaults } from "../../../Redux/combinedDefaults";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { POLICY_URL, NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { TErrorActions } from "./../raiseErrorAction";
import { IDocumentContentDto } from "../../../Api/Models";
import { EnrichConfiguration } from "../../../Api/Request";
import { GetUserLanguageFromStore } from "../../../Shared/Services/languageService";

export const REQUEST_POLICY_CONTENT = "REQUEST_POLICY_CONTENT";
export const RECEIVE_POLICY_CONTENT = "RECEIVE_POLICY_CONTENT";
export interface IRequestPolicyContent { type: typeof REQUEST_POLICY_CONTENT }
export interface IReceivePolicyContent { type: typeof RECEIVE_POLICY_CONTENT, payload: IDocumentContentDto }
export type TKnownActions = IRequestPolicyContent | IReceivePolicyContent | TErrorActions;

export const ActionCreators = 
{
    getPolicyContent: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        const isLanguageChanged = getState().userLanguage.id !== getState().getPolicyContent.content.language;

        if (getState().getPolicyContent.content !== combinedDefaults.getPolicyContent.content && !isLanguageChanged) 
            return;

        dispatch({ type: REQUEST_POLICY_CONTENT });

        const language = GetUserLanguageFromStore();
        const queryParam = language === "" ? "" : `&language=${language}`;

        axios(EnrichConfiguration(
        {
            method: "GET", 
            url: `${POLICY_URL}${queryParam}`,
            responseType: "json"
        }))
        .then(response =>
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError({ dispatch, errorObject: NULL_RESPONSE_ERROR }) 
                    : dispatch({ type: RECEIVE_POLICY_CONTENT, payload: response.data });
            }

            RaiseError({ dispatch, errorObject: GetTextStatusCode({ statusCode: response.status }) });
        })
        .catch(error =>
        {
            RaiseError({ dispatch: dispatch, errorObject: error });
        });
    }
}