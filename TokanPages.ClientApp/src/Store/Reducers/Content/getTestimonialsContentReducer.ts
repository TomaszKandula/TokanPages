import { Action, Reducer } from "redux";
import { ApplicationDefaults } from "../../Configuration";
import { IGetTestimonialsContent } from "../../States";
import { 
    TKnownActions,
    RECEIVE_TESTIMONIALS_CONTENT, 
    REQUEST_TESTIMONIALS_CONTENT
} from "../../Actions/Content/getTestimonialsContentAction";

export const GetTestimonialsContentReducer: 
    Reducer<IGetTestimonialsContent> = (state: IGetTestimonialsContent | undefined, incomingAction: Action): 
    IGetTestimonialsContent => 
{
    if (state === undefined) return ApplicationDefaults.contentTestimonials;

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
