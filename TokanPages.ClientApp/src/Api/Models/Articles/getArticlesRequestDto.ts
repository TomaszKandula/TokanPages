export interface GetArticlesRequestDto {
    pageNumber: number;
    pageSize: number;
    phrase?: string;
    categoryId?: string;
    orderByColumn?: string;
    orderByAscending: boolean;
    isPublished: boolean;
    noCache: boolean;
}
