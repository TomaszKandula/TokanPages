import { ApplicationAction } from "../../Configuration";
import { TKnownActions as TUpdateActions } from "./userDataStore";
import { 
    RequestContract, 
    GetConfiguration, 
    REVOKE_USER_TOKEN,
    ExecuteContract,
    Execute
} from "../../../Api/Request";

export const SIGNOUT = "SIGNOUT_USER";
export const CLEAR = "SIGNOUT_USER_CLEAR";
export const RESPONSE = "SIGNOUT_USER_RESPONSE";
interface Signout { type: typeof SIGNOUT }
interface Clear { type: typeof CLEAR }
interface Response { type: typeof RESPONSE; }
export type TKnownActions = Signout | Clear | Response | TUpdateActions;

export const UserSignoutAction = 
{
    clear: (): ApplicationAction<TKnownActions> => (dispatch) =>
    {
        dispatch({ type: CLEAR });
    },
    signout: (): ApplicationAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: SIGNOUT });

        const request: RequestContract = {
            configuration: {
                method: "POST", 
                url: REVOKE_USER_TOKEN, 
                data: { }
            }
        }

        const input: ExecuteContract = {
            configuration: GetConfiguration(request),
            dispatch: dispatch,
            responseType: RESPONSE
        }

        Execute(input);
    }
}
