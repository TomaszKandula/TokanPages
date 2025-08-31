import { ApplicationAction } from "../../Configuration";
import { ExecuteStoreActionProps, GET_ARTICLES } from "../../../Api";
import { useApiAction } from "../../../Shared/Hooks";
import { GetArticlesDto } from "../../../Api/Models";
import Validate from "validate.js"

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
    get: (pageNumber: number, pageSize: number, phrase?: string): ApplicationAction<TKnownActions> => (dispatch, getState) => {
        dispatch({ type: REQUEST });

        const actions = useApiAction();
        const baseParams = "orderByColumn=createdAt&orderByAscending=false&isPublished=true&noCache=false";
        let url = `${GET_ARTICLES}?pageNumber=${pageNumber}&pageSize=${pageSize}&${baseParams}`;
        if (!Validate.isEmpty(phrase)) {
            url = `${GET_ARTICLES}?pageNumber=${pageNumber}&pageSize=${pageSize}&${baseParams}&phrase=${phrase}`;
        }

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
