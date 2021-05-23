import { Action, Reducer } from "redux";
import { combinedDefaults } from "../combinedDefaults";
import { IGetFooterContent } from "../States/getFooterContentState";
import { 
    TKnownActions,
    RECEIVE_FOOTER_CONTENT, 
    REQUEST_FOOTER_CONTENT
} from "../Actions/getFooterContentAction";

const GetFooterContentReducer: Reducer<IGetFooterContent> = (state: IGetFooterContent | undefined, incomingAction: Action): IGetFooterContent => 
{
    if (state === undefined) return combinedDefaults.getFooterContent;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case REQUEST_FOOTER_CONTENT:
            return { 
                isLoading: true, 
                content: state.content
            };

        case RECEIVE_FOOTER_CONTENT:
            return { 
                isLoading: false, 
                content: action.payload.content
            };

        default: return state;
    }
}

export default GetFooterContentReducer;
