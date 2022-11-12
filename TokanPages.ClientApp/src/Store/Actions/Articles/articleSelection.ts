import axios from "axios";
import { IApplicationAction } from "../../Configuration";
import { IArticleItem } from "../../../Shared/Components/RenderContent/Models";
import { NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { EnrichConfiguration, GET_ARTICLE } from "../../../Api/Request";

export const RESET = "RESET_SELECTION";
export const REQUEST = "REQUEST_ARTICLE";
export const RECEIVE = "RECEIVE_ARTICLE";
interface IReset { type: typeof RESET; }
interface IRequest { type: typeof REQUEST; }
interface IReceive { type: typeof RECEIVE; payload: IArticleItem; }
export type TKnownActions = IReset | IRequest | IReceive;

export const ArticleSelectionAction = 
{
    reset: (): IApplicationAction<TKnownActions> => (dispatch) =>
    {
        dispatch({ type: RESET });
    },
    select: (id: string): IApplicationAction<TKnownActions> => (dispatch) =>
    {
        dispatch({ type: REQUEST });

        const url = GET_ARTICLE.replace("{id}", id);
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
                    : dispatch({ type: RECEIVE, payload: response.data });
            }

            RaiseError({ dispatch, errorObject: GetTextStatusCode({ statusCode: response.status }) });
        })
        .catch(error => 
        {
            RaiseError({ dispatch: dispatch, errorObject: error });
        });
    }
};