import axios from "axios";
import { AppThunkAction } from "../applicationState";
import { combinedDefaults } from "../../Redux/combinedDefaults";
import { RaiseError } from "../../Shared/helpers";
import { UnexpectedStatusCode } from "../../Shared/textWrappers";
import { GET_FEATURES_CONTENT, NULL_RESPONSE_ERROR } from "../../Shared/constants";
import { TErrorActions } from "./raiseErrorAction";
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

        axios( 
        {
            method: "GET", 
            url: GET_FEATURES_CONTENT,
            responseType: "json"
        })
        .then(response =>
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError(dispatch, NULL_RESPONSE_ERROR) 
                    : dispatch({ type: RECEIVE_FEATURES_CONTENT, payload: response.data });
            }
            
            RaiseError(dispatch, UnexpectedStatusCode(response.status));
        })
        .catch(error =>
        {
            RaiseError(dispatch, error);
        });
    }
}