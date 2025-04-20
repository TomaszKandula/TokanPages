import { ApplicationAction } from "../../Configuration";
import { RequestPageDataResultDto } from "../../../Api/Models";
import { GetVerifiedComponents } from "../../../Shared/Services/Utilities";
import { ExecuteStoreAction, ExecuteStoreActionProps, REQUEST_PAGE_DATA } from "../../../Api";

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
        (components: string[], pageId: string): ApplicationAction<TKnownActions> =>
        (dispatch, getState) => {
            const state = getState().contentPageData;
            if (state.pageId === pageId) {
                return;
            }

            const languageId = getState().applicationLanguage.id;
            const verifiedComponents = GetVerifiedComponents({ components, state, languageId });
            if (!verifiedComponents) {
                return;
            }

            dispatch({ type: REQUEST });
            const input: ExecuteStoreActionProps = {
                url: REQUEST_PAGE_DATA,
                dispatch: dispatch,
                state: getState,
                responseType: RECEIVE,
                configuration: {
                    method: "POST",
                    hasJsonResponse: true,
                    body: {
                        components: verifiedComponents,
                        language: languageId,
                        pageName: pageId,
                    },
                },
            };

            ExecuteStoreAction(input);
        },
};
