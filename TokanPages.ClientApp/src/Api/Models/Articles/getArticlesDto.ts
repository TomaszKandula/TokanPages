import { ArticleItem } from "../../../Shared/Components/RenderContent/Models";

export interface GetArticlesDto {
    pagingInfo: PageInfoDto;
    totalSize: number;
    results: ArticleItem[];
}

export interface PageInfoDto {
    isPublished: boolean;
    categoryName?: string;
    phrase?: string;
    pageNumber: number;
    pageSize: number;
    orderByColumn: string;
    orderByAscending: boolean;
}
