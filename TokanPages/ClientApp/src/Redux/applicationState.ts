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

export const initArticle = 
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

export const initArticles = 
{
    articles: [],
    isLoading: false
};

export const initialState = 
{

    selectArticle:
    {
        id: initArticle.id,
        title: initArticle.title,
        description: initArticle.description,
        isPublished: initArticle.isPublished,
        likes: initArticle.likes,
        readCount: initArticle.readCount,
        createdAt: initArticle.createdAt,
        updatedAt: initArticle.updatedAt
    },

    listArticles:
    {
        articles:  initArticles.articles,
        isLoading: initArticles.isLoading
    }

};
