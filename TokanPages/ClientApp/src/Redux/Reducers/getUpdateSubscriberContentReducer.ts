import { Action, Reducer } from "redux";
import { combinedDefaults } from "../combinedDefaults";
import { IGetUpdateSubscriberContent } from "../../Redux/States/getUpdateSubscriberContentState";
import { 
    TKnownActions,
    RECEIVE_UPDATE_SUBSCRIBER_CONTENT, 
    REQUEST_UPDATE_SUBSCRIBER_CONTENT
} from "../../Redux/Actions/getUpdateSubscriberContentAction";

const GetUpdateSubscriberContentReducer: Reducer<IGetUpdateSubscriberContent> = (state: IGetUpdateSubscriberContent | undefined, incomingAction: Action): IGetUpdateSubscriberContent => 
{
    if (state === undefined) return combinedDefaults.getUpdateSubscriberContent;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case REQUEST_UPDATE_SUBSCRIBER_CONTENT:
            return { 
                isLoading: true, 
                content: state.content
            };

        case RECEIVE_UPDATE_SUBSCRIBER_CONTENT:
            return { 
                isLoading: false, 
                content: action.payload.content
            };

        default: return state;
    }
}

export default GetUpdateSubscriberContentReducer;
