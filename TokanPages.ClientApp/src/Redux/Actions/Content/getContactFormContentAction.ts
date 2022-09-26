import axios from "axios";
import { AppThunkAction } from "../../applicationState";
import { combinedDefaults } from "../../../Redux/combinedDefaults";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { GET_CONTACT_FORM_CONTENT, NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { TErrorActions } from "./../raiseErrorAction";
import { IContactFormContentDto } from "../../../Api/Models";
import { EnrichConfiguration } from "../../../Api/Request";

export const REQUEST_CONTACT_FORM_CONTENT = "REQUEST_CONTACT_FORM_CONTENT";
export const RECEIVE_CONTACT_FORM_CONTENT = "RECEIVE_CONTACT_FORM_CONTENT";
export interface IRequestContactFormContent { type: typeof REQUEST_CONTACT_FORM_CONTENT }
export interface IReceiveContactFormContent { type: typeof RECEIVE_CONTACT_FORM_CONTENT, payload: IContactFormContentDto }
export type TKnownActions = IRequestContactFormContent | IReceiveContactFormContent | TErrorActions;

export const ActionCreators = 
{
    getContactFormContent: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        const isLanguageChanged = getState().userLanguage.id !== getState().getContactFormContent.content.language;

        if (getState().getContactFormContent.content !== combinedDefaults.getContactFormContent.content && !isLanguageChanged) 
            return;

        dispatch({ type: REQUEST_CONTACT_FORM_CONTENT });

        const id = getState().userLanguage.id;
        const queryParam = id === "" ? "" : `&language=${id}`;

        axios(EnrichConfiguration(
        {
            method: "GET", 
            url: `${GET_CONTACT_FORM_CONTENT}${queryParam}`,
            responseType: "json"
        }))
        .then(response =>
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError({ dispatch, errorObject: NULL_RESPONSE_ERROR }) 
                    : dispatch({ type: RECEIVE_CONTACT_FORM_CONTENT, payload: response.data });              
            }

            RaiseError({ dispatch, errorObject: GetTextStatusCode({ statusCode: response.status }) });
        })
        .catch(error =>
        {
            RaiseError({ dispatch: dispatch, errorObject: error });
        });
    }
}