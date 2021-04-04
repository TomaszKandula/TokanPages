import { Action, Reducer } from "redux";
import { IRaiseError } from "../States/raiseErrorState";
import { combinedDefaults } from "../../Redux/combinedDefaults";
import { RECEIVED_ERROR_MESSAGE } from "../../Shared/constants";
import { 
    REQUEST_ARTICLES_ERROR, 
    REQUEST_ARTICLE_ERROR, 
    UPDATE_ARTICLE_ERROR,
    ADD_SUBSCRIBER_ERROR,
    UPDATE_SUBSCRIBER_ERROR,
    REMOVE_SUBSCRIBER_ERROR,
    SEND_MESSAGE_ERROR,
    TKnownActions,  
} from "../Actions/raiseErrorAction";

const RaiseErrorReducer: Reducer<IRaiseError> = (state: IRaiseError | undefined, incomingAction: Action): IRaiseError => 
{
    if (state === undefined) return combinedDefaults.raiseError;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case REQUEST_ARTICLES_ERROR:
        case REQUEST_ARTICLE_ERROR:
        case UPDATE_ARTICLE_ERROR:
        case ADD_SUBSCRIBER_ERROR:
        case UPDATE_SUBSCRIBER_ERROR:
        case REMOVE_SUBSCRIBER_ERROR:
        case SEND_MESSAGE_ERROR:
            return { defaultErrorMessage: RECEIVED_ERROR_MESSAGE, attachedErrorObject: action.payload };        

        default: return state;
    }
};

export default RaiseErrorReducer;
