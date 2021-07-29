import axios from "axios";
import { AppThunkAction } from "../applicationState";
import { combinedDefaults } from "../../Redux/combinedDefaults";
import { RaiseError } from "../../Shared/helpers";
import { UnexpectedStatusCode } from "../../Shared/textWrappers";
import { GET_NEWSLETTER_CONTENT, NULL_RESPONSE_ERROR } from "../../Shared/constants";
import { TErrorActions } from "./raiseErrorAction";
import { INewsletterContentDto } from "../../Api/Models";

export const REQUEST_NEWSLETTER_CONTENT = "REQUEST_NEWSLETTER_CONTENT";
export const RECEIVE_NEWSLETTER_CONTENT = "RECEIVE_NEWSLETTER_CONTENT";
export interface IRequestNewsletterContent { type: typeof REQUEST_NEWSLETTER_CONTENT }
export interface IReceiveNewsletterContent { type: typeof RECEIVE_NEWSLETTER_CONTENT, payload: INewsletterContentDto }
export type TKnownActions = IRequestNewsletterContent | IReceiveNewsletterContent | TErrorActions;

export const ActionCreators = 
{
    getNewsletterContent: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        if (getState().getNewsletterContent.content !== combinedDefaults.getNewsletterContent.content) 
            return;

        dispatch({ type: REQUEST_NEWSLETTER_CONTENT });

        axios( 
        {
            method: "GET", 
            url: GET_NEWSLETTER_CONTENT,
            responseType: "json"
        })
        .then(response =>
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError(dispatch, NULL_RESPONSE_ERROR) 
                    : dispatch({ type: RECEIVE_NEWSLETTER_CONTENT, payload: response.data });
            }
            
            RaiseError(dispatch, UnexpectedStatusCode(response.status));
        })
        .catch(error =>
        {
            RaiseError(dispatch, error);
        });
    }
}