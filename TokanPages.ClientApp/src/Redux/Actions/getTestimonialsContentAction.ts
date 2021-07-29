import axios from "axios";
import { AppThunkAction } from "../applicationState";
import { combinedDefaults } from "../../Redux/combinedDefaults";
import { RaiseError } from "../../Shared/helpers";
import { UnexpectedStatusCode } from "../../Shared/textWrappers";
import { GET_TESTIMONIALS_CONTENT, NULL_RESPONSE_ERROR } from "../../Shared/constants";
import { TErrorActions } from "./raiseErrorAction";
import { ITestimonialsContentDto } from "../../Api/Models";

export const REQUEST_TESTIMONIALS_CONTENT = "REQUEST_TESTIMONIALS_CONTENT";
export const RECEIVE_TESTIMONIALS_CONTENT = "RECEIVE_TESTIMONIALS_CONTENT";
export interface IRequestTestimonialsContent { type: typeof REQUEST_TESTIMONIALS_CONTENT }
export interface IReceiveTestimonialsContent { type: typeof RECEIVE_TESTIMONIALS_CONTENT, payload: ITestimonialsContentDto }
export type TKnownActions = IRequestTestimonialsContent | IReceiveTestimonialsContent | TErrorActions;

export const ActionCreators = 
{
    getTestimonialsContent: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        if (getState().getTestimonialsContent.content !== combinedDefaults.getTestimonialsContent.content) 
            return;

        dispatch({ type: REQUEST_TESTIMONIALS_CONTENT });

        axios( 
        {
            method: "GET", 
            url: GET_TESTIMONIALS_CONTENT,
            responseType: "json"
        })
        .then(response =>
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError(dispatch, NULL_RESPONSE_ERROR) 
                    : dispatch({ type: RECEIVE_TESTIMONIALS_CONTENT, payload: response.data });
            }

            RaiseError(dispatch, UnexpectedStatusCode(response.status));
        })
        .catch(error =>
        {
            RaiseError(dispatch, error);
        });
    }
}