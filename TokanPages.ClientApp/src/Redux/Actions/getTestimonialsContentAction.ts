import axios from "axios";
import * as Sentry from "@sentry/react";
import { AppThunkAction } from "../applicationState";
import { combinedDefaults } from "../../Redux/combinedDefaults";
import { GetErrorMessage } from "../../Shared/helpers";
import { UnexpectedStatusCode } from "../../Shared/textWrappers";
import { GET_TESTIMONIALS_CONTENT } from "../../Shared/constants";
import { RAISE_ERROR, TErrorActions } from "./raiseErrorAction";
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

        axios.get(GET_TESTIMONIALS_CONTENT, 
        {
            method: "GET", 
            responseType: "json"
        })
        .then(response =>
        {
            if (response.status === 200)
            {
                dispatch({ type: RECEIVE_TESTIMONIALS_CONTENT, payload: response.data });
                return;
            }
            
            const error = UnexpectedStatusCode(response.status);
            dispatch({ type: RAISE_ERROR, errorObject: error });
            Sentry.captureException(error);
        })
        .catch(error =>
        {
            dispatch({ type: RAISE_ERROR, errorObject: GetErrorMessage(error) });
            Sentry.captureException(error);
        });
    }
}