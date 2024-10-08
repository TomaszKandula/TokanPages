import { ApplicationAction, ApplicationDefault } from "../../Configuration";
import { RequestPageDataResultDto } from "../../../Api/Models";
import {
    Execute,
    ExecuteContract,
    GetConfiguration,
    REQUEST_PAGE_DATA,
    RequestContract,
} from "../../../Api/Request";

export const CLEAR = "CLEAR_PAGE_DATA";
export const REQUEST = "REQUEST_PAGE_DATA";
export const RECEIVE = "RECEIVE_PAGE_DATA";
interface Clear {
    type: typeof CLEAR;
}
interface Request {
    type: typeof REQUEST;
}
interface Receive {
    type: typeof RECEIVE;
    payload: RequestPageDataResultDto;
}
export type TKnownActions = Clear | Request | Receive;

export const ContentPageDataAction = {
    clear: (): ApplicationAction<TKnownActions> => dispatch => {
        dispatch({ type: CLEAR });
    },
    request:
        (components: string[]): ApplicationAction<TKnownActions> =>
        (dispatch, getState) => {
            const content = getState().contentPageData;
            const languageId = getState().applicationLanguage.id;
            const isContentChanged = content !== ApplicationDefault.contentPageData;
            const isLanguageChanged = languageId !== content.languageId;

            if (isContentChanged && !isLanguageChanged) {
                return;
            }

            dispatch({ type: REQUEST });

            const request: RequestContract = {
                configuration: {
                    method: "POST",
                    url: REQUEST_PAGE_DATA,
                    data: {
                        components: components,
                        language: languageId,
                    },
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
