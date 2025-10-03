export interface RetrieveArticleInfoResponseDto {
    articles: ArticleDataDto[];
}

export interface ArticleDataDto {
    id: string;
    categoryName: string;
    title: string;
    description: string;
    isPublished: boolean;
    readCount: number;
    totalLikes: number;
    createdAt: string;
    updatedAt?: string;
    languageIso?: string;
}
