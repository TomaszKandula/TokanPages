import { ApplicationAction, ApplicationDefault } from "../../Configuration";
import { GetContent, GET_POLICY_CONTENT, GET_TERMS_CONTENT, GET_STORY_CONTENT, GET_SHOWCASE_CONTENT } from "../../../Api/Request";
import { DocumentContentDto } from "../../../Api/Models";

export const REQUEST_POLICY = "REQUEST_POLICY_CONTENT";
export const RECEIVE_POLICY = "RECEIVE_POLICY_CONTENT";
export const REQUEST_TERMS = "REQUEST_TERMS_CONTENT";
export const RECEIVE_TERMS = "RECEIVE_TERMS_CONTENT";
export const REQUEST_STORY = "REQUEST_STORY_CONTENT";
export const RECEIVE_STORY = "RECEIVE_STORY_CONTENT";
export const REQUEST_SHOWCASE = "REQUEST_SHOWCASE_CONTENT";
export const RECEIVE_SHOWCASE = "RECEIVE_SHOWCASE_CONTENT";

interface Payload { payload: DocumentContentDto; }
interface RequestPolicy { type: typeof REQUEST_POLICY; }
interface ReceivePolicy extends Payload { type: typeof RECEIVE_POLICY; }
interface RequestTerms { type: typeof REQUEST_TERMS; }
interface ReceiveTerms extends Payload { type: typeof RECEIVE_TERMS; }
interface RequestStory { type: typeof REQUEST_STORY; }
interface ReceiveStory extends Payload { type: typeof RECEIVE_STORY; }
interface RequestShowcase { type: typeof REQUEST_SHOWCASE; }
interface ReceiveShowcase extends Payload { type: typeof RECEIVE_SHOWCASE; }

type Policy = RequestPolicy | ReceivePolicy;
type Terms = RequestTerms | ReceiveTerms;
type Story = RequestStory | ReceiveStory;
type Showcase = RequestShowcase | ReceiveShowcase;
export type TKnownActions = Policy | Terms | Story | Showcase;

export const ContentDocumentAction = {
    getPolicy: (): ApplicationAction<TKnownActions> => (dispatch, getState) => {
        const content = getState().contentDocument.contentPolicy?.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentDocument.contentPolicy?.content;
        const isLanguageChanged = languageId !== content?.language;

        if (isContentChanged && !isLanguageChanged) {
            return;
        }

        GetContent({
            dispatch: dispatch,
            state: getState,
            request: REQUEST_POLICY,
            receive: RECEIVE_POLICY,
            url: GET_POLICY_CONTENT,
        });
    },
    getTerms: (): ApplicationAction<TKnownActions> => (dispatch, getState) => {
        const content = getState().contentDocument.contentTerms?.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentDocument.contentTerms?.content;
        const isLanguageChanged = languageId !== content?.language;

        if (isContentChanged && !isLanguageChanged) {
            return;
        }

        GetContent({
            dispatch: dispatch,
            state: getState,
            request: REQUEST_TERMS,
            receive: RECEIVE_TERMS,
            url: GET_TERMS_CONTENT,
        });
    },
    getStory: (): ApplicationAction<TKnownActions> => (dispatch, getState) => {
        const content = getState().contentDocument.contentTerms?.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentDocument.contentTerms?.content;
        const isLanguageChanged = languageId !== content?.language;

        if (isContentChanged && !isLanguageChanged) {
            return;
        }

        GetContent({
            dispatch: dispatch,
            state: getState,
            request: REQUEST_STORY,
            receive: RECEIVE_STORY,
            url: GET_STORY_CONTENT,
        });
    },
    getShowcase: (): ApplicationAction<TKnownActions> => (dispatch, getState) => {
        const content = getState().contentDocument.contentTerms?.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentDocument.contentTerms?.content;
        const isLanguageChanged = languageId !== content?.language;

        if (isContentChanged && !isLanguageChanged) {
            return;
        }

        GetContent({
            dispatch: dispatch,
            state: getState,
            request: REQUEST_SHOWCASE,
            receive: RECEIVE_SHOWCASE,
            url: GET_SHOWCASE_CONTENT,
        });
    },
};
