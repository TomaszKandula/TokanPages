import { IApplicationAction } from "../../Configuration";
import { IActivateUserDto } from "../../../Api/Models";
import { 
    Execute, 
    GetConfiguration, 
    IExecute, 
    IRequest, 
    ACTIVATE_USER
} from "../../../Api/Request";

export const ACTIVATE = "ACTIVATE_ACCOUNT";
export const CLEAR = "ACTIVATE_ACCOUNT_CLEAR";
export const RESPONSE = "ACTIVATE_ACCOUNT_RESPONSE";
interface IActivate { type: typeof ACTIVATE }
interface IClear { type: typeof CLEAR }
interface IResponse { type: typeof RESPONSE; payload: any; }
export type TKnownActions = IActivate | IClear | IResponse;

export const UserActivateAction = 
{
    clear: (): IApplicationAction<TKnownActions> => (dispatch) =>
    {
        dispatch({ type: CLEAR });
    },
    activate: (payload: IActivateUserDto): IApplicationAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: ACTIVATE });

        const request: IRequest = {
            configuration: {
                method: "POST", 
                url: ACTIVATE_USER, 
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
