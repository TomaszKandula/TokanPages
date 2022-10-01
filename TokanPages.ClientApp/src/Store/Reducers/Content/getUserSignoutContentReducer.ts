import { Action, Reducer } from "redux";
import { ApplicationDefaults } from "../../Configuration";
import { IGetUserSignoutContent } from "../../States";
import { 
    TKnownActions, 
    REQUEST_USER_SIGNOUT_CONTENT, 
    RECEIVE_USER_SIGNOUT_CONTENT 
} from "../../Actions/Content/getUserSignoutContentAction";

export const GetUserSignoutContentReducer: 
    Reducer<IGetUserSignoutContent> = (state: IGetUserSignoutContent | undefined, incomingAction: Action): 
    IGetUserSignoutContent => 
{
    if (state === undefined) return ApplicationDefaults.contentUserSignout;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case REQUEST_USER_SIGNOUT_CONTENT:
            return { 
                isLoading: true, 
                content: state.content
            };

        case RECEIVE_USER_SIGNOUT_CONTENT:
            return { 
                isLoading: false, 
                content: action.payload.content
            };

        default: return state;
    }
}
