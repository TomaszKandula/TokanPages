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

export interface ContentDocumentState {
    contentPolicy?: ContentPolicy;
    contentTerms?: ContentTerms;
    contentStory?: ContentStory;
    contentShowcase?: ContentShowcase;
}
