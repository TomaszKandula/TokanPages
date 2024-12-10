import { ApplicationAction } from "../../Configuration";
import { ArticleItemBase } from "../../../Shared/Components/RenderContent/Models";
import { Execute, GetConfiguration, ExecuteContract, RequestContract, GET_ARTICLE_INFO } from "../../../Api/Request";

export const REQUEST = "REQUEST_ARTICLE_INFO";
export const RECEIVE = "RECEIVE_ARTICLE_INFO";
interface Request {
    type: typeof REQUEST;
}
interface Receive {
    type: typeof RECEIVE;
    payload: ArticleItemBase;
}
export type TKnownActions = Request | Receive;

export const ArticleInfoAction = {
    get: (id: string): ApplicationAction<TKnownActions> => (dispatch, getState) => {
        dispatch({ type: REQUEST });

        const request: RequestContract = {
            configuration: {
                method: "GET",
                url: GET_ARTICLE_INFO.replace("{id}", id),
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
