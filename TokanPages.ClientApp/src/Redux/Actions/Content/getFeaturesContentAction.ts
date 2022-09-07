import axios from "axios";
import { AppThunkAction } from "../../applicationState";
import { combinedDefaults } from "../../../Redux/combinedDefaults";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { GET_FEATURES_CONTENT, NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { TErrorActions } from "./../raiseErrorAction";
import { IFeaturesContentDto } from "../../../Api/Models";
import { EnrichConfiguration } from "../../../Api/Request";
import { GetUserLanguageFromStore } from "../../../Shared/Services/languageService";

export const REQUEST_FEATURES_CONTENT = "REQUEST_FEATURES_CONTENT";
export const RECEIVE_FEATURES_CONTENT = "RECEIVE_FEATURES_CONTENT";
export interface IRequestFeaturesContent { type: typeof REQUEST_FEATURES_CONTENT }
export interface IReceiveFeaturesContent { type: typeof RECEIVE_FEATURES_CONTENT, payload: IFeaturesContentDto }
export type TKnownActions = IRequestFeaturesContent | IReceiveFeaturesContent | TErrorActions;

export const ActionCreators = 
{
    getFeaturesContent: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        const isLanguageChanged = getState().userLanguage.id !== getState().getFeaturesContent.content.language;

        if (getState().getFeaturesContent.content !== combinedDefaults.getFeaturesContent.content && !isLanguageChanged) 
            return;

        dispatch({ type: REQUEST_FEATURES_CONTENT });

        const language = GetUserLanguageFromStore();
        const queryParam = language === "" ? "" : `&language=${language}`;

        axios(EnrichConfiguration(
        {
            method: "GET", 
            url: `${GET_FEATURES_CONTENT}${queryParam}`,
            responseType: "json"
        }))
        .then(response =>
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError({ dispatch, errorObject: NULL_RESPONSE_ERROR }) 
                    : dispatch({ type: RECEIVE_FEATURES_CONTENT, payload: response.data });
            }
            
            RaiseError({ dispatch, errorObject: GetTextStatusCode({ statusCode: response.status }) });
        })
        .catch(error =>
        {
            RaiseError({ dispatch: dispatch, errorObject: error });
        });
    }
}