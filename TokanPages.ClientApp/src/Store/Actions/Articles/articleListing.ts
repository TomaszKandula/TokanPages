import { ApplicationAction } from "../../Configuration";
import { ArticleItem } from "../../../Shared/Components/RenderContent/Models";
import { Execute, GetConfiguration, ExecuteContract, RequestContract, GET_ARTICLES } from "../../../Api/Request";

export const REQUEST = "REQUEST_ARTICLES";
export const RECEIVE = "RECEIVE_ARTICLES";
interface Request {
    type: typeof REQUEST;
}
interface Receive {
    type: typeof RECEIVE;
    payload: ArticleItem[];
}
export type TKnownActions = Request | Receive;

export const ArticleListingAction = {
    get: (): ApplicationAction<TKnownActions> => (dispatch, getState) => {
        dispatch({ type: REQUEST });

        const request: RequestContract = {
            configuration: {
                method: "GET",
                url: GET_ARTICLES,
                responseType: "json",
            },
        };

        const input: ExecuteContract = {
            configuration: GetConfiguration(request),
            dispatch: dispatch,
            state: getState,
            responseType: RECEIVE,
        };

        Execute(input);
    },
};
