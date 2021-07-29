import axios from "axios";
import { AppThunkAction } from "../applicationState";
import { combinedDefaults } from "../../Redux/combinedDefaults";
import { RaiseError } from "../../Shared/helpers";
import { UnexpectedStatusCode } from "../../Shared/textWrappers";
import { GET_ARTICLE_FEAT_CONTENT, NULL_RESPONSE_ERROR } from "../../Shared/constants";
import { TErrorActions } from "./raiseErrorAction";
import { IArticleFeatContentDto } from "../../Api/Models";

export const REQUEST_ARTICE_FEATURES = "REQUEST_ARTICE_FEATURES";
export const RECEIVE_ARTICE_FEATURES = "RECEIVE_ARTICE_FEATURES";
export interface IRequestArticleFeatures { type: typeof REQUEST_ARTICE_FEATURES }
export interface IReceiveArticleFeatures { type: typeof RECEIVE_ARTICE_FEATURES, payload: IArticleFeatContentDto }
export type TKnownActions = IRequestArticleFeatures | IReceiveArticleFeatures | TErrorActions;

export const ActionCreators = 
{
    getArticleFeaturesContent: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        if (getState().getArticleFeatContent.content !== combinedDefaults.getArticleFeatContent.content) 
            return;

        dispatch({ type: REQUEST_ARTICE_FEATURES });

        axios( 
        {
            method: "GET", 
            url: GET_ARTICLE_FEAT_CONTENT,
            responseType: "json"
        })
        .then(response =>
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError(dispatch, NULL_RESPONSE_ERROR) 
                    : dispatch({ type: RECEIVE_ARTICE_FEATURES, payload: response.data });
            }

            RaiseError(dispatch, UnexpectedStatusCode(response.status));
        })
        .catch(error =>
        {
            RaiseError(dispatch, error);
        });
    }
}