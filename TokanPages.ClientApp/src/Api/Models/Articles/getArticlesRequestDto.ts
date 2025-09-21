export interface GetArticlesRequestDto {
    pageNumber: number;
    pageSize: number;
    phrase?: string;
    category?: string;
    orderByColumn?: string;
    orderByAscending: boolean;
    isPublished: boolean;
    noCache: boolean;
}
