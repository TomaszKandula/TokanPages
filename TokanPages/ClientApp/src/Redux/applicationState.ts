export interface ApplicationState 
{
    selectArticle: IArticle;
    listArticles: IArticles;
}

export interface IListArticles
{
    listArticles: IArticles;
}

export interface IArticles
{
    isLoading: boolean;
    articles: IArticle[];
}

export interface IArticle
{
    id: string;
    title: string;
    description: string;
    isPublished: boolean;
    likes: number;
    readCount: number;
    createdAt: string;
    updatedAt: string;
}

export interface AppThunkAction<TAction> 
{
    (dispatch: (action: TAction) => void, getState: () => ApplicationState): void;
}

export const ArticleDefaultValues: IArticle = 
{
    id: "",
    title: "",
    description: "",
    isPublished: false,
    likes:  0,
    readCount: 0,
    createdAt: "",
    updatedAt: ""
};

export const ArticlesDefaultValues: IArticles = 
{
    articles: [],
    isLoading: false
};

export const DefaultAppState = 
{
    selectArticle:
    {
        id: ArticleDefaultValues.id,
        title: ArticleDefaultValues.title,
        description: ArticleDefaultValues.description,
        isPublished: ArticleDefaultValues.isPublished,
        likes: ArticleDefaultValues.likes,
        readCount: ArticleDefaultValues.readCount,
        createdAt: ArticleDefaultValues.createdAt,
        updatedAt: ArticleDefaultValues.updatedAt
    },

    listArticles:
    {
        articles:  ArticlesDefaultValues.articles,
        isLoading: ArticlesDefaultValues.isLoading
    }
};
