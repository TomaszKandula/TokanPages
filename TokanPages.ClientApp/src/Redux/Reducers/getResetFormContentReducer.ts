import { Action, Reducer } from "redux";
import { combinedDefaults } from "../combinedDefaults";
import { IGetResetFormContent } from "../../Redux/States/getResetFormContentState";
import { 
    TKnownActions,
    RECEIVE_RESET_FORM_CONTENT, 
    REQUEST_RESET_FORM_CONTENT
} from "../../Redux/Actions/getResetFormContentAction";

const GetResetFormContentReducer: Reducer<IGetResetFormContent> = (state: IGetResetFormContent | undefined, incomingAction: Action): IGetResetFormContent => 
{
    if (state === undefined) return combinedDefaults.getResetFormContent;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case REQUEST_RESET_FORM_CONTENT:
            return { 
                isLoading: true, 
                content: state.content
            };

        case RECEIVE_RESET_FORM_CONTENT:
            return { 
                isLoading: false, 
                content: action.payload.content
            };

        default: return state;
    }
}

export default GetResetFormContentReducer;
