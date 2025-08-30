import { SearchLabelsDto } from "./Common";

export interface ArticlesContentDto {
    language: string;
    caption: string;
    labels: SearchLabelsDto;
    content: string[];
}
