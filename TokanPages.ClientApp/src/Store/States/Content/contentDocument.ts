import { DocumentContentDto } from "../../../Api/Models";

interface ContentPolicy extends DocumentContentDto {
    isLoading: boolean;
}

interface ContentTerms extends DocumentContentDto {
    isLoading: boolean;
}

interface ContentStory extends DocumentContentDto {
    isLoading: boolean;
}

interface ContentShowcase extends DocumentContentDto {
    isLoading: boolean;
}

interface ContentBicycle extends DocumentContentDto {
    isLoading: boolean;
}

interface ContentElectronics extends DocumentContentDto {
    isLoading: boolean;
}

interface ContentFootball extends DocumentContentDto {
    isLoading: boolean;
}

interface ContentGuitar extends DocumentContentDto {
    isLoading: boolean;
}

interface ContentPhotography extends DocumentContentDto {
    isLoading: boolean;
}

export interface ContentDocumentState {
    contentPolicy?: ContentPolicy;
    contentTerms?: ContentTerms;
    contentStory?: ContentStory;
    contentShowcase?: ContentShowcase;
    contentBicycle?: ContentBicycle;
    contentElectronics?: ContentElectronics;
    contentFootball?: ContentFootball;
    contentGuitar?: ContentGuitar;
    contentPhotography?: ContentPhotography;
}
