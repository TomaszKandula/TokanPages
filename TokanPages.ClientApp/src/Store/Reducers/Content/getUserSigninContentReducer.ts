import { Action, Reducer } from "redux";
import { ApplicationDefaults } from "../../Configuration";
import { IGetUserSigninContent } from "../../States";
import { 
    TKnownActions, 
    REQUEST_USER_SIGNIN_CONTENT, 
    RECEIVE_USER_SIGNIN_CONTENT 
} from "../../Actions/Content/getUserSigninContentAction";

export const GetUserSigninContentReducer: 
    Reducer<IGetUserSigninContent> = (state: IGetUserSigninContent | undefined, incomingAction: Action): 
    IGetUserSigninContent => 
{
    if (state === undefined) return ApplicationDefaults.getUserSigninContent;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case REQUEST_USER_SIGNIN_CONTENT:
            return { 
                isLoading: true, 
                content: state.content
            };

        case RECEIVE_USER_SIGNIN_CONTENT:
            return { 
                isLoading: false, 
                content: action.payload.content
            };

        default: return state;
    }
}
