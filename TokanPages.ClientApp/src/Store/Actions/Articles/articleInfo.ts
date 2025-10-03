import { ApplicationAction } from "../../Configuration";
import { ExecuteStoreActionProps, RETRIEVE_ARTICLE_INFO } from "../../../Api";
import { RetrieveArticleInfoRequestDto, RetrieveArticleInfoResponseDto } from "../../../Api/Models";
import { useApiAction } from "../../../Shared/Hooks";

export const REQUEST = "REQUEST_ARTICLE_INFO";
export const RECEIVE = "RECEIVE_ARTICLE_INFO";
export const CLEAR = "CLEAR_ARTICLE_INFO";
interface Request {
    type: typeof REQUEST;
}
interface Receive {
    type: typeof RECEIVE;
    payload: RetrieveArticleInfoResponseDto;
}
interface Clear {
    type: typeof CLEAR;
}
export type TKnownActions = Request | Receive | Clear;

export const ArticleInfoAction = {
    get:
        (request: RetrieveArticleInfoRequestDto): ApplicationAction<TKnownActions> =>
        (dispatch, getState) => {
            dispatch({ type: REQUEST });

            const actions = useApiAction();
            const input: ExecuteStoreActionProps = {
                url: RETRIEVE_ARTICLE_INFO,
                dispatch: dispatch,
                state: getState,
                responseType: RECEIVE,
                configuration: {
                    method: "POST",
                    body: request,
                    hasJsonResponse: true,
                },
            };

            actions.storeAction(input);
        },
    clear: (): ApplicationAction<TKnownActions> => (dispatch, _getState) => {
        dispatch({ type: CLEAR });
    }
};
