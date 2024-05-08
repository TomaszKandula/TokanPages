import { ContentDocumentState } from "../../States";

const defaultValues = { 
    isLoading: false,
    content: {
        language: "",
        items: [],
    },
}

export const ContentDocument: ContentDocumentState = {
    contentPolicy: defaultValues,
    contentTerms: defaultValues,
    contentStory: defaultValues,
    contentShowcase: defaultValues,
    contentBicycle: defaultValues,
    contentFootball: defaultValues,
    contentElectronics: defaultValues,
    contentGuitar: defaultValues,
    contentPhotography: defaultValues
};
