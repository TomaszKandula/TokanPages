import { ApplicationAction } from "../../Configuration";
import { ActivateUserDto, ActivateUserResultDto } from "../../../Api/Models";
import { ACTIVATE_USER, ExecuteStoreActionProps } from "../../../Api";
import { useApiAction } from "../../../Shared/Hooks";

export const ACTIVATE = "ACTIVATE_ACCOUNT";
export const CLEAR = "ACTIVATE_ACCOUNT_CLEAR";
export const RESPONSE = "ACTIVATE_ACCOUNT_RESPONSE";
interface Activate {
    type: typeof ACTIVATE;
}
interface Clear {
    type: typeof CLEAR;
}
interface Response {
    type: typeof RESPONSE;
    payload: ActivateUserResultDto;
}
export type TKnownActions = Activate | Clear | Response;

export const UserActivateAction = {
    clear: (): ApplicationAction<TKnownActions> => dispatch => {
        dispatch({ type: CLEAR });
    },
    activate:
        (payload: ActivateUserDto): ApplicationAction<TKnownActions> =>
        (dispatch, getState) => {
            dispatch({ type: ACTIVATE });
            const actions = useApiAction();
            const input: ExecuteStoreActionProps = {
                url: ACTIVATE_USER,
                dispatch: dispatch,
                state: getState,
                responseType: RESPONSE,
                configuration: {
                    method: "POST",
                    body: payload,
                    hasJsonResponse: true,
                },
            };

            actions.storeAction(input);
        },
};
