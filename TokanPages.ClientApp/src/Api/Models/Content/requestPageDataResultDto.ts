import { ContentModelDto } from "./Items/contentModelDto";

export interface RequestPageDataResultDto {
    components: ContentModelDto[];
    language?: string;
}
