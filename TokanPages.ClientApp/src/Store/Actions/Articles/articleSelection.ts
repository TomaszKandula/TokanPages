import { ApplicationAction } from "../../Configuration";
import { ArticleItem } from "../../../Shared/Components/RenderContent/Models";
import { Execute, GetConfiguration, ExecuteContract, RequestContract, GET_ARTICLE } from "../../../Api/Request";

export const RESET = "RESET_SELECTION";
export const REQUEST = "REQUEST_ARTICLE";
export const RECEIVE = "RECEIVE_ARTICLE";
interface Reset {
    type: typeof RESET;
}
interface Request {
    type: typeof REQUEST;
}
interface Receive {
    type: typeof RECEIVE;
    payload: ArticleItem;
}
export type TKnownActions = Reset | Request | Receive;

export const ArticleSelectionAction = {
    reset: (): ApplicationAction<TKnownActions> => dispatch => {
        dispatch({ type: RESET });
    },
    select:
        (id: string): ApplicationAction<TKnownActions> =>
        dispatch => {
            dispatch({ type: REQUEST });

            const url = GET_ARTICLE.replace("{id}", id);

            const request: RequestContract = {
                configuration: {
                    method: "GET",
                    url: url,
                },
            };

            const input: ExecuteContract = {
                configuration: GetConfiguration(request),
                dispatch: dispatch,
                responseType: RECEIVE,
            };

            Execute(input);
        },
};
