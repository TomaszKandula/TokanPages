import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { ContentDocumentState } from "../../States";
import { 
    TKnownActions, 
    RECEIVE_POLICY, 
    REQUEST_POLICY, 
    RECEIVE_TERMS, 
    REQUEST_TERMS, 
    REQUEST_STORY,
    RECEIVE_STORY,
    REQUEST_SHOWCASE,
    RECEIVE_SHOWCASE
} from "../../Actions/Content/contentDocument";

export const ContentDocument: Reducer<ContentDocumentState> = (
    state: ContentDocumentState | undefined,
    incomingAction: Action
): ContentDocumentState => {
    if (state === undefined) return ApplicationDefault.contentDocument;

    const fallback = { items: [], language: "" };
    const action = incomingAction as TKnownActions;
    switch (action.type) {
        case REQUEST_POLICY:
            return {
                ...state,
                contentPolicy: {
                    isLoading: true,
                    content: state.contentPolicy?.content ?? fallback,
                },
            };
        case RECEIVE_POLICY:
            return {
                ...state,
                contentPolicy: {
                    isLoading: false,
                    content: action.payload.content,
                }
            };
        case REQUEST_TERMS:
            return {
                ...state,
                contentTerms: {
                    isLoading: true,
                    content: state.contentTerms?.content ?? fallback,
                },
            };
        case RECEIVE_TERMS:
            return {
                ...state,
                contentTerms: {
                    isLoading: false,
                    content: action.payload.content,
                }
            };
        case REQUEST_STORY:
            return {
                ...state,
                contentStory: {
                    isLoading: true,
                    content: state.contentStory?.content ?? fallback,
                },
            };
        case RECEIVE_STORY:
            return {
                ...state,
                contentStory: {
                    isLoading: false,
                    content: action.payload.content,
                }
            };
        case REQUEST_SHOWCASE:
            return {
                ...state,
                contentShowcase: {
                    isLoading: true,
                    content: state.contentShowcase?.content ?? fallback,
                },
            };
        case RECEIVE_SHOWCASE:
            return {
                ...state,
                contentShowcase: {
                    isLoading: false,
                    content: action.payload.content,
                }
            };
        default:
            return state;
    }
};
