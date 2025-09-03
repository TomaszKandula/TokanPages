import { SearchLabelsDto, SearchNotFoundDto } from "./Common";

export interface ArticlesContentDto {
    language: string;
    caption: string;
    labels: SearchLabelsDto;
    search: SearchNotFoundDto;
    content: string[];
}
