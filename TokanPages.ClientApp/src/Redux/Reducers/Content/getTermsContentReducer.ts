import { Action, Reducer } from "redux";
import { combinedDefaults } from "../../combinedDefaults";
import { IGetTermsContent } from "../../../Redux/States/Content/getTermsContentState";
import { 
    TKnownActions,
    RECEIVE_TERMS_CONTENT, 
    REQUEST_TERMS_CONTENT
} from "../../../Redux/Actions/Content/getTermsContentAction";

export const GetTermsContentReducer: Reducer<IGetTermsContent> = (state: IGetTermsContent | undefined, incomingAction: Action): IGetTermsContent => 
{
    if (state === undefined) return combinedDefaults.getTermsContent;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case REQUEST_TERMS_CONTENT:
            return { 
                isLoading: true, 
                content: state.content
            };

        case RECEIVE_TERMS_CONTENT:
            return { 
                isLoading: false, 
                content: action.payload.content
            };

        default: return state;
    }
}
