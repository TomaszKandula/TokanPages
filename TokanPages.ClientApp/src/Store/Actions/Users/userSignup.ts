import { IApplicationAction } from "../../Configuration";
import { IAddUserDto } from "../../../Api/Models";
import { 
    Execute, 
    GetConfiguration, 
    IExecute, 
    IRequest, 
    ADD_USER
} from "../../../Api/Request";

export const SIGNUP = "SIGNUP_USER";
export const CLEAR = "SIGNUP_USER_CLEAR";
export const RESPONSE = "SIGNUP_USER_RESPONSE";
interface ISignup { type: typeof SIGNUP }
interface IClear { type: typeof CLEAR }
interface IResponse { type: typeof RESPONSE; payload: any; }
export type TKnownActions = ISignup | IClear | IResponse;

export const UserSignupAction = 
{
    clear: (): IApplicationAction<TKnownActions> => (dispatch) =>
    {
        dispatch({ type: CLEAR });
    },
    signup: (payload: IAddUserDto): IApplicationAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: SIGNUP });

        const request: IRequest = {
            configuration: {
                method: "POST", 
                url: ADD_USER, 
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
