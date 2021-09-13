import { Action, Reducer } from "redux";
import { combinedDefaults } from "../../combinedDefaults";
import { IGetUpdatePasswordContent } from "../../States/Content/getUpdatePasswordContentState";
import { 
    TKnownActions,
    RECEIVE_UPDATE_PASSWORD_CONTENT, 
    REQUEST_UPDATE_PASSWORD_CONTENT
} from "../../Actions/Content/getUpdatePasswordContentAction";

const GetUpdatePasswordContentReducer: Reducer<IGetUpdatePasswordContent> = (state: IGetUpdatePasswordContent | undefined, incomingAction: Action): IGetUpdatePasswordContent => 
{
    if (state === undefined) return combinedDefaults.getUpdatePasswordContent;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case REQUEST_UPDATE_PASSWORD_CONTENT:
            return { 
                isLoading: true, 
                content: state.content
            };

        case RECEIVE_UPDATE_PASSWORD_CONTENT:
            return { 
                isLoading: false, 
                content: action.payload.content
            };

        default: return state;
    }
}

export default GetUpdatePasswordContentReducer;
