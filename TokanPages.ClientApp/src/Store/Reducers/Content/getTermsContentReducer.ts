import { Action, Reducer } from "redux";
import { CombinedDefaults } from "../../Configuration";
import { IGetTermsContent } from "../../States";
import { 
    TKnownActions,
    RECEIVE_TERMS_CONTENT, 
    REQUEST_TERMS_CONTENT
} from "../../Actions/Content/getTermsContentAction";

export const GetTermsContentReducer: 
    Reducer<IGetTermsContent> = (state: IGetTermsContent | undefined, incomingAction: Action): 
    IGetTermsContent => 
{
    if (state === undefined) return CombinedDefaults.getTermsContent;

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
