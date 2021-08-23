import { Action, Reducer } from "redux";
import { combinedDefaults } from "../../combinedDefaults";
import { IGetTestimonialsContent } from "../../../Redux/States/Content/getTestimonialsContentState";
import { 
    TKnownActions,
    RECEIVE_TESTIMONIALS_CONTENT, 
    REQUEST_TESTIMONIALS_CONTENT
} from "../../../Redux/Actions/Content/getTestimonialsContentAction";

const GetTestimonialsContentReducer: Reducer<IGetTestimonialsContent> = (state: IGetTestimonialsContent | undefined, incomingAction: Action): IGetTestimonialsContent => 
{
    if (state === undefined) return combinedDefaults.getTestimonialsContent;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case REQUEST_TESTIMONIALS_CONTENT:
            return { 
                isLoading: true, 
                content: state.content
            };

        case RECEIVE_TESTIMONIALS_CONTENT:
            return { 
                isLoading: false, 
                content: action.payload.content
            };

        default: return state;
    }
}

export default GetTestimonialsContentReducer;
