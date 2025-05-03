import { ApplicationAction } from "../../Configuration";
import { RequestPageDataResultDto } from "../../../Api/Models";
import { GetVerifiedComponents } from "../../../Shared/Services/Utilities";
import { ExecuteStoreActionProps, REQUEST_PAGE_DATA } from "../../../Api";
import { useApiAction } from "../../../Shared/Hooks";

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
            const stateContent = getState().contentPageData;
            const stateLanguage = getState().applicationLanguage;

            const isLanguageSame = stateContent.languageId === stateLanguage.id;
            const isPageSame = stateContent.pageId === pageId;

            if (isLanguageSame && isPageSame) {
                return;
            }

            const languageId = stateLanguage.id;
            const verifiedComponents = GetVerifiedComponents({ components, state: stateContent, languageId });
            if (!verifiedComponents) {
                return;
            }

            dispatch({ type: REQUEST });
            const actions = useApiAction();
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

            actions.storeAction(input);
        },
};
