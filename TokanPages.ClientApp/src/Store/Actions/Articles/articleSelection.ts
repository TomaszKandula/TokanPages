import axios from "axios";
import { IAppThunkAction } from "../../Configuration";
import { IArticleItem } from "../../../Shared/Components/RenderContent/Models";
import { API_QUERY_GET_ARTICLE, NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { EnrichConfiguration } from "../../../Api/Request";

export const RESET_SELECTION = "RESET_SELECTION";
export const REQUEST_ARTICLE = "REQUEST_ARTICLE";
export const RECEIVE_ARTICLE = "RECEIVE_ARTICLE";
export interface IResetSelection { type: typeof RESET_SELECTION; }
export interface IRequestArticleAction { type: typeof REQUEST_ARTICLE; }
export interface IReceiveArticleAction { type: typeof RECEIVE_ARTICLE; payload: IArticleItem; }
export type TKnownActions = IResetSelection | IRequestArticleAction | IReceiveArticleAction;

export const ArticleSelectionAction = 
{
    reset: (): IAppThunkAction<TKnownActions> => (dispatch) =>
    {
        dispatch({ type: RESET_SELECTION });
    },
    select: (id: string): IAppThunkAction<TKnownActions> => (dispatch) =>
    {
        dispatch({ type: REQUEST_ARTICLE });

        const url = API_QUERY_GET_ARTICLE.replace("{id}", id);
        axios(url, EnrichConfiguration(
        { 
            method: "GET", 
            responseType: "json" 
        }))
        .then(response => 
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError({ dispatch, errorObject: NULL_RESPONSE_ERROR }) 
                    : dispatch({ type: RECEIVE_ARTICLE, payload: response.data });
            }

            RaiseError({ dispatch, errorObject: GetTextStatusCode({ statusCode: response.status }) });
        })
        .catch(error => 
        {
            RaiseError({ dispatch: dispatch, errorObject: error });
        });
    }
};