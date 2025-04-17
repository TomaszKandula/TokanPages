import { ApplicationAction } from "../../Configuration";
import { ArticleItem } from "../../../Shared/Components/RenderContent/Models";
import { DispatchExecuteAction, ExecuteRequest, GET_ARTICLES } from "../../../Api/Request";

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

        const input: ExecuteRequest = {
            url: GET_ARTICLES,
            dispatch: dispatch,
            state: getState,
            responseType: RECEIVE,
            configuration: {
                method: "GET",
            },
        };

        DispatchExecuteAction(input);
    },
};
