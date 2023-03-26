import { ApplicationAction } from "../../Configuration";
import { RevokeUserRefreshTokenDto } from "../../../Api/Models";
import { 
    RequestContract, 
    GetConfiguration, 
    REVOKE_USER_TOKEN,
    ExecuteContract,
    Execute,
    REVOKE_REFRESH_TOKEN
} from "../../../Api/Request";

export const SIGNOUT = "SIGNOUT_USER";
export const CLEAR = "SIGNOUT_USER_CLEAR";
export const RESPONSE = "SIGNOUT_USER_RESPONSE";
interface Signout { type: typeof SIGNOUT }
interface Clear { type: typeof CLEAR }
interface Response { type: typeof RESPONSE; }
export type TKnownActions = Signout | Clear | Response;

export const UserSignoutAction = 
{
    clear: (): ApplicationAction<TKnownActions> => (dispatch) =>
    {
        dispatch({ type: CLEAR });
    },
    signout: (): ApplicationAction<TKnownActions> => (dispatch, getState) => 
    {
        dispatch({ type: SIGNOUT });

        const payload: RevokeUserRefreshTokenDto = {
            refreshToken: getState().userDataStore.userData.refreshToken
        }

        const requestRevokeRefreshToken: RequestContract = {
            configuration: {
                method: "POST", 
                url: REVOKE_REFRESH_TOKEN, 
                data: payload
            }
        }

        const revokeRefreshToken: ExecuteContract = {
            configuration: GetConfiguration(requestRevokeRefreshToken),
            dispatch: dispatch,
            responseType: RESPONSE
        }

        const requestRevokeToken: RequestContract = {
            configuration: {
                method: "POST", 
                url: REVOKE_USER_TOKEN, 
                data: { }
            }
        }

        const revokeUserToken: ExecuteContract = {
            configuration: GetConfiguration(requestRevokeToken),
            dispatch: dispatch,
            responseType: RESPONSE
        }

        Execute(revokeRefreshToken);
        Execute(revokeUserToken);
    }
}
