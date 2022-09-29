import { Action, Reducer } from "redux";
import { CombinedDefaults } from "../../Configuration";
import { IGetResetPasswordContent } from "../../States/Content/getResetPasswordContentState";
import { 
    TKnownActions,
    RECEIVE_RESET_PASSWORD_CONTENT, 
    REQUEST_RESET_PASSWORD_CONTENT
} from "../../Actions/Content/getResetPasswordContentAction";

export const GetResetPasswordContentReducer: Reducer<IGetResetPasswordContent> = (state: IGetResetPasswordContent | undefined, incomingAction: Action): IGetResetPasswordContent => 
{
    if (state === undefined) return CombinedDefaults.getResetPasswordContent;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case REQUEST_RESET_PASSWORD_CONTENT:
            return { 
                isLoading: true, 
                content: state.content
            };

        case RECEIVE_RESET_PASSWORD_CONTENT:
            return { 
                isLoading: false, 
                content: action.payload.content
            };

        default: return state;
    }
}
