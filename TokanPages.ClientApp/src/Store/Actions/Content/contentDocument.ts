import { ApplicationAction, ApplicationDefault } from "../../Configuration";
import { DocumentContentDto } from "../../../Api/Models";
import {
    GetContent,
    GET_POLICY_CONTENT,
    GET_TERMS_CONTENT,
    GET_STORY_CONTENT,
    GET_SHOWCASE_CONTENT,
    GET_BICYCLE_CONTENT,
    GET_ELECTRONICS_CONTENT,
    GET_GUITAR_CONTENT,
    GET_PHOTOGRAPHY_CONTENT,
    GET_FOOTBALL_CONTENT,
    GET_ABOUT_CONTENT
} from "../../../Api/Request";

export const REQUEST_POLICY = "REQUEST_POLICY_CONTENT";
export const RECEIVE_POLICY = "RECEIVE_POLICY_CONTENT";
export const REQUEST_TERMS = "REQUEST_TERMS_CONTENT";
export const RECEIVE_TERMS = "RECEIVE_TERMS_CONTENT";
export const REQUEST_ABOUT = "REQUEST_ABOUT_CONTENT";
export const RECEIVE_ABOUT = "RECEIVE_ABOUT_CONTENT";
export const REQUEST_STORY = "REQUEST_STORY_CONTENT";
export const RECEIVE_STORY = "RECEIVE_STORY_CONTENT";
export const REQUEST_SHOWCASE = "REQUEST_SHOWCASE_CONTENT";
export const RECEIVE_SHOWCASE = "RECEIVE_SHOWCASE_CONTENT";
export const REQUEST_BICYCLE = "REQUEST_BICYCLE_CONTENT";
export const RECEIVE_BICYCLE = "RECEIVE_BICYCLE_CONTENT";
export const REQUEST_ELECTRONICS = "REQUEST_ELECTRONICS_CONTENT";
export const RECEIVE_ELECTRONICS = "RECEIVE_ELECTRONICS_CONTENT";
export const REQUEST_FOOTBALL = "REQUEST_FOOTBALL_CONTENT";
export const RECEIVE_FOOTBALL = "RECEIVE_FOOTBALL_CONTENT";
export const REQUEST_GUITAR = "REQUEST_GUITAR_CONTENT";
export const RECEIVE_GUITAR = "RECEIVE_GUITAR_CONTENT";
export const REQUEST_PHOTOGRAPHY = "REQUEST_PHOTOGRAPHY_CONTENT";
export const RECEIVE_PHOTOGRAPHY = "RECEIVE_PHOTOGRAPHY_CONTENT";

interface Payload {
    payload: DocumentContentDto;
}
interface RequestPolicy {
    type: typeof REQUEST_POLICY;
}
interface ReceivePolicy extends Payload {
    type: typeof RECEIVE_POLICY;
}
interface RequestTerms {
    type: typeof REQUEST_TERMS;
}
interface ReceiveTerms extends Payload {
    type: typeof RECEIVE_TERMS;
}
interface RequestAbout {
    type: typeof REQUEST_ABOUT;
}
interface ReceiveAbout extends Payload {
    type: typeof RECEIVE_ABOUT;
}
interface RequestStory {
    type: typeof REQUEST_STORY;
}
interface ReceiveStory extends Payload {
    type: typeof RECEIVE_STORY;
}
interface RequestShowcase {
    type: typeof REQUEST_SHOWCASE;
}
interface ReceiveShowcase extends Payload {
    type: typeof RECEIVE_SHOWCASE;
}
interface RequestBicycle {
    type: typeof REQUEST_BICYCLE;
}
interface ReceiveBicycle extends Payload {
    type: typeof RECEIVE_BICYCLE;
}
interface RequestElectronics {
    type: typeof REQUEST_ELECTRONICS;
}
interface ReceiveElectronics extends Payload {
    type: typeof RECEIVE_ELECTRONICS;
}
interface RequestFootball {
    type: typeof REQUEST_FOOTBALL;
}
interface ReceiveFootball extends Payload {
    type: typeof RECEIVE_FOOTBALL;
}
interface RequestGuitar {
    type: typeof REQUEST_GUITAR;
}
interface ReceiveGuitar extends Payload {
    type: typeof RECEIVE_GUITAR;
}
interface RequestPhotography {
    type: typeof REQUEST_PHOTOGRAPHY;
}
interface ReceivePhotography extends Payload {
    type: typeof RECEIVE_PHOTOGRAPHY;
}

type Policy = RequestPolicy | ReceivePolicy;
type Terms = RequestTerms | ReceiveTerms;
type About = RequestAbout | ReceiveAbout;
type Story = RequestStory | ReceiveStory;
type Showcase = RequestShowcase | ReceiveShowcase;
type Bicycle = RequestBicycle | ReceiveBicycle;
type Electronics = RequestElectronics | ReceiveElectronics;
type Football = RequestFootball | ReceiveFootball;
type Guitar = RequestGuitar | ReceiveGuitar;
type Photography = RequestPhotography | ReceivePhotography;
export type TKnownActions = Policy | Terms | About | Story | Showcase | Bicycle | Electronics | Football | Guitar | Photography;

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
    getAbout: (): ApplicationAction<TKnownActions> => (dispatch, getState) => {
        const content = getState().contentDocument.contentAbout?.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentDocument.contentAbout?.content;
        const isLanguageChanged = languageId !== content?.language;

        if (isContentChanged && !isLanguageChanged) {
            return;
        }

        GetContent({
            dispatch: dispatch,
            state: getState,
            request: REQUEST_ABOUT,
            receive: RECEIVE_ABOUT,
            url: GET_ABOUT_CONTENT,
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
    getBicycle: (): ApplicationAction<TKnownActions> => (dispatch, getState) => {
        const content = getState().contentDocument.contentBicycle?.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentDocument.contentBicycle?.content;
        const isLanguageChanged = languageId !== content?.language;

        if (isContentChanged && !isLanguageChanged) {
            return;
        }

        GetContent({
            dispatch: dispatch,
            state: getState,
            request: REQUEST_BICYCLE,
            receive: RECEIVE_BICYCLE,
            url: GET_BICYCLE_CONTENT,
        });
    },
    getElectronics: (): ApplicationAction<TKnownActions> => (dispatch, getState) => {
        const content = getState().contentDocument.contentElectronics?.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentDocument.contentElectronics?.content;
        const isLanguageChanged = languageId !== content?.language;

        if (isContentChanged && !isLanguageChanged) {
            return;
        }

        GetContent({
            dispatch: dispatch,
            state: getState,
            request: REQUEST_ELECTRONICS,
            receive: RECEIVE_ELECTRONICS,
            url: GET_ELECTRONICS_CONTENT,
        });
    },
    getFootball: (): ApplicationAction<TKnownActions> => (dispatch, getState) => {
        const content = getState().contentDocument.contentFootball?.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentDocument.contentFootball?.content;
        const isLanguageChanged = languageId !== content?.language;

        if (isContentChanged && !isLanguageChanged) {
            return;
        }

        GetContent({
            dispatch: dispatch,
            state: getState,
            request: REQUEST_FOOTBALL,
            receive: RECEIVE_FOOTBALL,
            url: GET_FOOTBALL_CONTENT,
        });
    },
    getGuitar: (): ApplicationAction<TKnownActions> => (dispatch, getState) => {
        const content = getState().contentDocument.contentGuitar?.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentDocument.contentGuitar?.content;
        const isLanguageChanged = languageId !== content?.language;

        if (isContentChanged && !isLanguageChanged) {
            return;
        }

        GetContent({
            dispatch: dispatch,
            state: getState,
            request: REQUEST_GUITAR,
            receive: RECEIVE_GUITAR,
            url: GET_GUITAR_CONTENT,
        });
    },
    getPhotography: (): ApplicationAction<TKnownActions> => (dispatch, getState) => {
        const content = getState().contentDocument.contentPhotography?.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentDocument.contentPhotography?.content;
        const isLanguageChanged = languageId !== content?.language;

        if (isContentChanged && !isLanguageChanged) {
            return;
        }

        GetContent({
            dispatch: dispatch,
            state: getState,
            request: REQUEST_PHOTOGRAPHY,
            receive: RECEIVE_PHOTOGRAPHY,
            url: GET_PHOTOGRAPHY_CONTENT,
        });
    },
};
