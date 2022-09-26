import axios from "axios";
import { AppThunkAction } from "../../applicationState";
import { combinedDefaults } from "../../../Redux/combinedDefaults";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { GET_TESTIMONIALS_CONTENT, NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { TErrorActions } from "./../raiseErrorAction";
import { ITestimonialsContentDto } from "../../../Api/Models";
import { EnrichConfiguration } from "../../../Api/Request";

export const REQUEST_TESTIMONIALS_CONTENT = "REQUEST_TESTIMONIALS_CONTENT";
export const RECEIVE_TESTIMONIALS_CONTENT = "RECEIVE_TESTIMONIALS_CONTENT";
export interface IRequestTestimonialsContent { type: typeof REQUEST_TESTIMONIALS_CONTENT }
export interface IReceiveTestimonialsContent { type: typeof RECEIVE_TESTIMONIALS_CONTENT, payload: ITestimonialsContentDto }
export type TKnownActions = IRequestTestimonialsContent | IReceiveTestimonialsContent | TErrorActions;

export const ActionCreators = 
{
    getTestimonialsContent: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        const isLanguageChanged = getState().userLanguage.id !== getState().getTestimonialsContent.content.language;

        if (getState().getTestimonialsContent.content !== combinedDefaults.getTestimonialsContent.content && !isLanguageChanged) 
            return;

        dispatch({ type: REQUEST_TESTIMONIALS_CONTENT });

        const id = getState().userLanguage.id;
        const queryParam = id === "" ? "" : `&language=${id}`;

        axios(EnrichConfiguration(
        {
            method: "GET", 
            url: `${GET_TESTIMONIALS_CONTENT}${queryParam}`,
            responseType: "json"
        }))
        .then(response =>
        {
            if (response.status === 200)
            {
                return response.data === null 
                ? RaiseError({ dispatch, errorObject: NULL_RESPONSE_ERROR }) 
                : dispatch({ type: RECEIVE_TESTIMONIALS_CONTENT, payload: response.data });
            }

            RaiseError({ dispatch, errorObject: GetTextStatusCode({ statusCode: response.status }) });
        })
        .catch(error =>
        {
            RaiseError({ dispatch: dispatch, errorObject: error });
        });
    }
}