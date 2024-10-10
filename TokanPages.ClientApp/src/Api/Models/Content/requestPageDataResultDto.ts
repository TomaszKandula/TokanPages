import { ContentModelDto } from "./Items/contentModelDto";

export interface RequestPageDataResultDto {
    components: ContentModelDto[];
    pageName?: string;
    language?: string;
}
