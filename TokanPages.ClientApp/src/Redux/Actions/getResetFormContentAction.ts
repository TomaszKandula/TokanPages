import axios from "axios";
import { AppThunkAction } from "../applicationState";
import { combinedDefaults } from "../../Redux/combinedDefaults";
import { RaiseError } from "../../Shared/helpers";
import { UnexpectedStatusCode } from "../../Shared/textWrappers";
import { GET_RESET_FORM_CONTENT, NULL_RESPONSE_ERROR } from "../../Shared/constants";
import { TErrorActions } from "./raiseErrorAction";
import { IResetFormContentDto } from "../../Api/Models";

export const REQUEST_RESET_FORM_CONTENT = "REQUEST_RESET_FORM_CONTENT";
export const RECEIVE_RESET_FORM_CONTENT = "RECEIVE_RESET_FORM_CONTENT";
export interface IRequestResetFormContent { type: typeof REQUEST_RESET_FORM_CONTENT }
export interface IReceiveResetFormContent { type: typeof RECEIVE_RESET_FORM_CONTENT, payload: IResetFormContentDto }
export type TKnownActions = IRequestResetFormContent | IReceiveResetFormContent | TErrorActions;

export const ActionCreators = 
{
    getResetFormContent: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        if (getState().getResetFormContent.content !== combinedDefaults.getResetFormContent.content) 
            return;

        dispatch({ type: REQUEST_RESET_FORM_CONTENT });

        axios( 
        {
            method: "GET", 
            url: GET_RESET_FORM_CONTENT,
            responseType: "json"
        })
        .then(response =>
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError(dispatch, NULL_RESPONSE_ERROR) 
                    : dispatch({ type: RECEIVE_RESET_FORM_CONTENT, payload: response.data });
            }

            RaiseError(dispatch, UnexpectedStatusCode(response.status));
        })
        .catch(error =>
        {
            RaiseError(dispatch, error);
        });
    }
}