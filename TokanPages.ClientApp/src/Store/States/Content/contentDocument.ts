import { DocumentContentDto } from "../../../Api/Models";

interface DocumentContentProps extends DocumentContentDto {
    isLoading: boolean;
}

export interface ContentDocumentState {
    contentPolicy?: DocumentContentProps;
    contentTerms?: DocumentContentProps;
    contentAbout?: DocumentContentProps;
    contentStory?: DocumentContentProps;
    contentShowcase?: DocumentContentProps;
    contentBicycle?: DocumentContentProps;
    contentElectronics?: DocumentContentProps;
    contentFootball?: DocumentContentProps;
    contentGuitar?: DocumentContentProps;
    contentPhotography?: DocumentContentProps;
}
