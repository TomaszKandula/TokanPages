import axios from "axios";
import * as Sentry from "@sentry/react";
import { AppThunkAction } from "../applicationState";
import { combinedDefaults } from "../../Redux/combinedDefaults";
import { GetErrorMessage } from "../../Shared/helpers";
import { UnexpectedStatusCode } from "../../Shared/textWrappers";
import { GET_FEATURES_CONTENT } from "../../Shared/constants";
import { RAISE_ERROR, TErrorActions } from "./raiseErrorAction";
import { IFeaturesContentDto } from "../../Api/Models";

export const REQUEST_FEATURES_CONTENT = "REQUEST_FEATURES_CONTENT";
export const RECEIVE_FEATURES_CONTENT = "RECEIVE_FEATURES_CONTENT";

export interface IRequestFeaturesContent { type: typeof REQUEST_FEATURES_CONTENT }
export interface IReceiveFeaturesContent { type: typeof RECEIVE_FEATURES_CONTENT, payload: IFeaturesContentDto }

export type TKnownActions = IRequestFeaturesContent | IReceiveFeaturesContent | TErrorActions;

export const ActionCreators = 
{
    getFeaturesContent: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        if (getState().getFeaturesContent.content !== combinedDefaults.getFeaturesContent.content) 
            return;

        dispatch({ type: REQUEST_FEATURES_CONTENT });

        axios.get(GET_FEATURES_CONTENT, 
        {
            method: "GET", 
            responseType: "json"
        })
        .then(response =>
        {
            if (response.status === 200)
            {
                dispatch({ type: RECEIVE_FEATURES_CONTENT, payload: response.data });
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