import { ArticleItem } from "../../../Shared/Components/RenderContent/Models";

export interface GetArticlesDto {
    pagingInfo: PageInfoDto;
    totalSize: number;
    articleCategories: ArticleCategory[];
    results: ArticleItem[];
}

export interface ArticleCategory {
    id: string;
    categoryName: string;
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
