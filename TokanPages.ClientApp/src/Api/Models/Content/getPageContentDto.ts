import { ContentModelDto } from "./Items/contentModelDto";

export interface GetPageContentDto {
    components: ContentModelDto[];
    language?: string;
}
