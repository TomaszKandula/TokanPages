import axios from "axios";
import { AppThunkAction } from "../../applicationState";
import { combinedDefaults } from "../../../Redux/combinedDefaults";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { GET_UPDATE_SUBSCRIBER_CONTENT, NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { TErrorActions } from "./../raiseErrorAction";
import { IUpdateSubscriberContentDto } from "../../../Api/Models";
import { EnrichConfiguration } from "../../../Api/Request";
import { GetUserLanguage } from "../../../Shared/Services/languageService";

export const REQUEST_UPDATE_SUBSCRIBER_CONTENT = "REQUEST_UPDATE_SUBSCRIBER_CONTENT";
export const RECEIVE_UPDATE_SUBSCRIBER_CONTENT = "RECEIVE_UPDATE_SUBSCRIBER_CONTENT";
export interface IRequestUpdateSubscriberContent { type: typeof REQUEST_UPDATE_SUBSCRIBER_CONTENT }
export interface IReceiveUpdateSubscriberContent { type: typeof RECEIVE_UPDATE_SUBSCRIBER_CONTENT, payload: IUpdateSubscriberContentDto }
export type TKnownActions = IRequestUpdateSubscriberContent | IReceiveUpdateSubscriberContent | TErrorActions;

export const ActionCreators = 
{
    getUpdateSubscriberContent: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        const isLanguageChanged = getState().userLanguage.id !== getState().getUpdateSubscriberContent.content.language;

        if (getState().getUpdateSubscriberContent.content !== combinedDefaults.getUpdateSubscriberContent.content && !isLanguageChanged) 
            return;
        
        dispatch({ type: REQUEST_UPDATE_SUBSCRIBER_CONTENT });

        const language = GetUserLanguage();
        const queryParam = language === "" ? "" : `&language=${language}`;

        axios(EnrichConfiguration(
        {
            method: "GET", 
            url: `${GET_UPDATE_SUBSCRIBER_CONTENT}${queryParam}`,
            responseType: "json"
        }))
        .then(response =>
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError({ dispatch: dispatch, errorObject: NULL_RESPONSE_ERROR }) 
                    : dispatch({ type: RECEIVE_UPDATE_SUBSCRIBER_CONTENT, payload: response.data });
            }

            RaiseError({ dispatch: dispatch, errorObject: GetTextStatusCode({ statusCode: response.status }) });
        })
        .catch(error =>
        {
            RaiseError({ dispatch: dispatch, errorObject: error });
        });
    }
}