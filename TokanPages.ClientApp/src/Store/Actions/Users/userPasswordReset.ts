import { IApplicationAction } from "../../Configuration";
import { IResetUserPasswordDto } from "../../../Api/Models";
import { 
    Execute, 
    GetConfiguration, 
    IExecute, 
    IRequest, 
    RESET_USER_PASSWORD
} from "../../../Api/Request";

export const RESET = "RESET_USER_PASSWORD";
export const CLEAR = "RESET_USER_PASSWORD_CLEAR";
export const RESPONSE = "RESET_USER_PASSWORD_RESPONSE";
interface IReset { type: typeof RESET }
interface IClear { type: typeof CLEAR }
interface IResponse { type: typeof RESPONSE; payload: any; }
export type TKnownActions = IReset | IClear | IResponse;

export const UserPasswordResetAction = 
{
    clear: (): IApplicationAction<TKnownActions> => (dispatch) =>
    {
        dispatch({ type: CLEAR });
    },
    reset: (payload: IResetUserPasswordDto): IApplicationAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: RESET });

        const request: IRequest = {
            configuration: {
                method: "POST", 
                url: RESET_USER_PASSWORD, 
                data: payload
            }
        }
    
        const input: IExecute = {
            configuration: GetConfiguration(request),
            dispatch: dispatch,
            responseType: RESPONSE
        }

        Execute(input);
    }
}
