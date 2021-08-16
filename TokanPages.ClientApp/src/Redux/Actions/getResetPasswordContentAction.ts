import axios from "axios";
import { AppThunkAction } from "../applicationState";
import { combinedDefaults } from "../combinedDefaults";
import { RaiseError } from "../../Shared/helpers";
import { UnexpectedStatusCode } from "../../Shared/textWrappers";
import { GET_RESET_PASSWORD_CONTENT, NULL_RESPONSE_ERROR } from "../../Shared/constants";
import { TErrorActions } from "./raiseErrorAction";
import { IResetPasswordContentDto } from "../../Api/Models";

export const REQUEST_RESET_PASSWORD_CONTENT = "REQUEST_RESET_PASSWORD_CONTENT";
export const RECEIVE_RESET_PASSWORD_CONTENT = "RECEIVE_RESET_PASSWORD_CONTENT";
export interface IRequestResetPasswordContent { type: typeof REQUEST_RESET_PASSWORD_CONTENT }
export interface IReceiveResetPasswordContent { type: typeof RECEIVE_RESET_PASSWORD_CONTENT, payload: IResetPasswordContentDto }
export type TKnownActions = IRequestResetPasswordContent | IReceiveResetPasswordContent | TErrorActions;

export const ActionCreators = 
{
    getResetPasswordContent: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        if (getState().getResetPasswordContent.content !== combinedDefaults.getResetPasswordContent.content) 
            return;

        dispatch({ type: REQUEST_RESET_PASSWORD_CONTENT });

        axios( 
        {
            method: "GET", 
            url: GET_RESET_PASSWORD_CONTENT,
            responseType: "json"
        })
        .then(response =>
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError(dispatch, NULL_RESPONSE_ERROR) 
                    : dispatch({ type: RECEIVE_RESET_PASSWORD_CONTENT, payload: response.data });
            }

            RaiseError(dispatch, UnexpectedStatusCode(response.status));
        })
        .catch(error =>
        {
            RaiseError(dispatch, error);
        });
    }
}