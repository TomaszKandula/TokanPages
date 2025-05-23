import { ApplicationAction } from "../../Configuration";
import { ArticleItemBase } from "../../../Shared/Components/RenderContent/Models";
import { ExecuteStoreActionProps, GET_ARTICLE_INFO } from "../../../Api";
import { useApiAction } from "../../../Shared/Hooks";

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
    get:
        (id: string): ApplicationAction<TKnownActions> =>
        (dispatch, getState) => {
            dispatch({ type: REQUEST });

            const actions = useApiAction();
            const input: ExecuteStoreActionProps = {
                url: GET_ARTICLE_INFO.replace("{id}", id),
                dispatch: dispatch,
                state: getState,
                responseType: RECEIVE,
                configuration: {
                    method: "GET",
                    hasJsonResponse: true,
                },
            };

            actions.storeAction(input);
        },
};
