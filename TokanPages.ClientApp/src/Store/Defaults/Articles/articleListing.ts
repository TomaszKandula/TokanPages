import { ArticleListingState } from "../../States";

export const ArticleListing: ArticleListingState = {
    isLoading: false,
    payload: {
        pagingInfo: {
            isPublished: false,
            pageNumber: 0,
            pageSize: 0,
            orderByColumn: "",
            orderByAscending: false,
        },
        articleCategories: [],
        totalSize: 0,
        results: [],
    },
};
