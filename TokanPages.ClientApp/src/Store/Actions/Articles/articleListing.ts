import { ApplicationAction } from "../../Configuration";
import { ExecuteStoreActionProps, GET_ARTICLES } from "../../../Api";
import { useApiAction } from "../../../Shared/Hooks";
import { GetArticlesDto, GetArticlesRequestDto } from "../../../Api/Models";
import { ProcessQueryParams } from "../../../Shared/Services/Utilities";

export const REQUEST = "REQUEST_ARTICLES";
export const RECEIVE = "RECEIVE_ARTICLES";
interface Request {
    type: typeof REQUEST;
}
interface Receive {
    type: typeof RECEIVE;
    payload: GetArticlesDto;
}
export type TKnownActions = Request | Receive;

export const ArticleListingAction = {
    get:
        (request: GetArticlesRequestDto): ApplicationAction<TKnownActions> =>
        (dispatch, getState) => {
            dispatch({ type: REQUEST });

            const actions = useApiAction();
            const url = GET_ARTICLES + ProcessQueryParams(request);

            const input: ExecuteStoreActionProps = {
                url: url,
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
