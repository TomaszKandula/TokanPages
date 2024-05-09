import { ApplicationAction } from "../../Configuration";
import { ArticleItem } from "../../../Shared/Components/RenderContent/Models";
import Validate from "validate.js";
import { 
    Execute, 
    GetConfiguration, 
    ExecuteContract, 
    RequestContract, 
    GET_ARTICLE, 
    GET_ARTICLE_BY_TITLE 
} from "../../../Api/Request";

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
        (props: {id?: string, title?: string}): ApplicationAction<TKnownActions> =>
        (dispatch, getState) => {
            dispatch({ type: REQUEST });

            let url = "";
            if (Validate.isDefined(props.id) && !Validate.isEmpty(props.id)) {
                url = GET_ARTICLE.replace("{id}", props.id!);
            }

            if (Validate.isDefined(props.title) && !Validate.isEmpty(props.title)) {
                url = GET_ARTICLE_BY_TITLE.replace("{title}", props.title?.toLowerCase()!);
            }

            const request: RequestContract = {
                configuration: {
                    method: "GET",
                    url: url,
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
