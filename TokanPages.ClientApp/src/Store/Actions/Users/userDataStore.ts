import { AuthenticateUserResultDto } from "../../../Api/Models";
import { ApplicationAction } from "../../Configuration";

export const SHOW = "SHOW_USERDATA";
export const CLEAR = "CLEAR_USERDATA";
export const UPDATE = "UPDATE_USERDATA";
interface Show {
    type: typeof SHOW;
    payload: boolean;
}
interface Clear {
    type: typeof CLEAR;
}
interface Update {
    type: typeof UPDATE;
    payload: AuthenticateUserResultDto;
}
export type TKnownActions = Show | Clear | Update;

export const UserDataStoreAction = {
    show:
        (isShown: boolean): ApplicationAction<TKnownActions> =>
        dispatch => {
            dispatch({ type: SHOW, payload: isShown });
        },
    clear: (): ApplicationAction<TKnownActions> => dispatch => {
        dispatch({ type: CLEAR });
    },
    update:
        (userData: AuthenticateUserResultDto): ApplicationAction<TKnownActions> =>
        dispatch => {
            dispatch({ type: UPDATE, payload: userData });
        },
};
