import axios from "axios";
import { AppThunkAction } from "../applicationState";
import { combinedDefaults } from "../../Redux/combinedDefaults";
import { RaiseError } from "../../Shared/helpers";
import { UnexpectedStatusCode } from "../../Shared/textWrappers";
import { GET_FOOTER_CONTENT, NULL_RESPONSE_ERROR } from "../../Shared/constants";
import { TErrorActions } from "./raiseErrorAction";
import { IFooterContentDto } from "../../Api/Models";

export const REQUEST_FOOTER_CONTENT = "REQUEST_FOOTER_CONTENT";
export const RECEIVE_FOOTER_CONTENT = "RECEIVE_FOOTER_CONTENT";
export interface IRequestFooterContent { type: typeof REQUEST_FOOTER_CONTENT }
export interface IReceiveFooterContent { type: typeof RECEIVE_FOOTER_CONTENT, payload: IFooterContentDto }
export type TKnownActions = IRequestFooterContent | IReceiveFooterContent | TErrorActions;

export const ActionCreators = 
{
    getFooterContent: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        if (getState().getFooterContent.content !== combinedDefaults.getFooterContent.content) 
            return;

        dispatch({ type: REQUEST_FOOTER_CONTENT });

        axios( 
        {
            method: "GET", 
            url: GET_FOOTER_CONTENT,
            responseType: "json"
        })
        .then(response =>
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError(dispatch, NULL_RESPONSE_ERROR) 
                    : dispatch({ type: RECEIVE_FOOTER_CONTENT, payload: response.data });
            }
            
            RaiseError(dispatch, UnexpectedStatusCode(response.status));
        })
        .catch(error =>
        {
            RaiseError(dispatch, error);
        });
    }
}