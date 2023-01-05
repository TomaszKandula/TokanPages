import axios from "axios";
import { IApplicationAction } from "../../Configuration";
import { IArticleItem } from "../../../Shared/Components/RenderContent/Models";
import { NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { EnrichConfiguration, GET_ARTICLES } from "../../../Api/Request";

export const REQUEST = "REQUEST_ARTICLES";
export const RECEIVE = "RECEIVE_ARTICLES";
interface IRequest { type: typeof REQUEST; }
interface IReceive { type: typeof RECEIVE; payload: IArticleItem[]; }
export type TKnownActions = IRequest | IReceive;

export const ArticleListingAction = 
{
    get: (): IApplicationAction<TKnownActions> => (dispatch) =>
    {
        dispatch({ type: REQUEST });

        axios(EnrichConfiguration( 
        {
            method: "GET", 
            url: GET_ARTICLES,
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
