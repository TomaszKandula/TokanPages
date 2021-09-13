import axios from "axios";
import { AppThunkAction } from "../../applicationState";
import { combinedDefaults } from "../../combinedDefaults";
import { RaiseError } from "../../../Shared/helpers";
import { UnexpectedStatusCode } from "../../../Shared/textWrappers";
import { GET_UPDATE_PASSWORD_CONTENT, NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { TErrorActions } from "../raiseErrorAction";
import { IUpdatePasswordContentDto } from "../../../Api/Models";

export const REQUEST_UPDATE_PASSWORD_CONTENT = "REQUEST_UPDATE_PASSWORD_CONTENT";
export const RECEIVE_UPDATE_PASSWORD_CONTENT = "RECEIVE_UPDATE_PASSWORD_CONTENT";
export interface IRequestUpdatePasswordContent { type: typeof REQUEST_UPDATE_PASSWORD_CONTENT }
export interface IReceiveUpdatePasswordContent { type: typeof RECEIVE_UPDATE_PASSWORD_CONTENT, payload: IUpdatePasswordContentDto }
export type TKnownActions = IRequestUpdatePasswordContent | IReceiveUpdatePasswordContent | TErrorActions;

export const ActionCreators = 
{
    getUpdatePasswordContent: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        if (getState().getUpdatePasswordContent.content !== combinedDefaults.getUpdatePasswordContent.content) 
            return;

        dispatch({ type: REQUEST_UPDATE_PASSWORD_CONTENT });

        axios( 
        {
            method: "GET", 
            url: GET_UPDATE_PASSWORD_CONTENT,
            responseType: "json"
        })
        .then(response =>
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError(dispatch, NULL_RESPONSE_ERROR) 
                    : dispatch({ type: RECEIVE_UPDATE_PASSWORD_CONTENT, payload: response.data });
            }

            RaiseError(dispatch, UnexpectedStatusCode(response.status));
        })
        .catch(error =>
        {
            RaiseError(dispatch, error);
        });
    }
}