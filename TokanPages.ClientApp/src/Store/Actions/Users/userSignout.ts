import { ApplicationAction } from "../../Configuration";
import { RevokeUserRefreshTokenDto } from "../../../Api/Models";
import {
    REVOKE_USER_TOKEN as REVOKE_USER_TOKEN_URL,
    DispatchExecuteAction,
    REVOKE_REFRESH_TOKEN as REVOKE_REFRESH_TOKEN_URL,
    ExecuteRequest,
} from "../../../Api/Request";

export const REVOKE_USER_TOKEN_CLEAR = "REVOKE_USER_TOKEN_CLEAR";
export const REVOKE_REFRESH_TOKEN_CLEAR = "REVOKE_REFRESH_TOKEN_CLEAR";
export const REVOKE_USER_TOKEN = "REVOKE_USER_TOKEN";
export const REVOKE_REFRESH_TOKEN = "REVOKE_REFRESH_TOKEN";
export const USER_TOKEN_RESPONSE = "USER_TOKEN_RESPONSE";
export const REFRESH_TOKEN_RESPONSE = "REFRESH_TOKEN_RESPONSE";

interface RevokeUserTokenClear {
    type: typeof REVOKE_USER_TOKEN_CLEAR;
}
interface RevokeRefreshTokenClear {
    type: typeof REVOKE_REFRESH_TOKEN_CLEAR;
}
interface RevokeUserToken {
    type: typeof REVOKE_USER_TOKEN;
}
interface RevokeRefreshToken {
    type: typeof REVOKE_REFRESH_TOKEN;
}
interface UserToken {
    type: typeof USER_TOKEN_RESPONSE;
}
interface RefreshToken {
    type: typeof REFRESH_TOKEN_RESPONSE;
}

export type TKnownActions =
    | RevokeUserTokenClear
    | RevokeRefreshTokenClear
    | RevokeUserToken
    | RevokeRefreshToken
    | UserToken
    | RefreshToken;

export const UserSignoutAction = {
    clearUserToken: (): ApplicationAction<TKnownActions> => dispatch => {
        dispatch({ type: REVOKE_USER_TOKEN_CLEAR });
    },
    clearRefreshToken: (): ApplicationAction<TKnownActions> => dispatch => {
        dispatch({ type: REVOKE_REFRESH_TOKEN_CLEAR });
    },
    revokeUserToken: (): ApplicationAction<TKnownActions> => (dispatch, getState) => {
        dispatch({ type: REVOKE_USER_TOKEN });
        const input: ExecuteRequest = {
            url: REVOKE_USER_TOKEN_URL,
            dispatch: dispatch,
            state: getState,
            responseType: USER_TOKEN_RESPONSE,
            configuration: {
                method: "POST",
            },
        };

        DispatchExecuteAction(input);
    },
    revokeRefreshToken: (): ApplicationAction<TKnownActions> => (dispatch, getState) => {
        const payload: RevokeUserRefreshTokenDto = {
            refreshToken: getState().userDataStore.userData.refreshToken,
        };
        dispatch({ type: REVOKE_REFRESH_TOKEN });
        const input: ExecuteRequest = {
            url: REVOKE_REFRESH_TOKEN_URL,
            dispatch: dispatch,
            state: getState,
            responseType: REFRESH_TOKEN_RESPONSE,
            configuration: {
                method: "POST",
                body: payload,
            },
        };
        
        DispatchExecuteAction(input);
    },
};
