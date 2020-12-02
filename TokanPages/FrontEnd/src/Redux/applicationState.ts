export interface ApplicationState 
{
    selectArticle: IArticle;
    listArticles:  IArticles;
}

export interface IListArticles
{
    listArticles: IArticles;
}

export interface IArticles
{
    isLoading: boolean;
    articles:  IArticle[];
}

export interface IArticle
{
    id:     string;
    title:  string;
    desc:   string;
    status: string;
    likes:  number;
    readCount: number;
}

export interface AppThunkAction<TAction> 
{
    (dispatch: (action: TAction) => void, getState: () => ApplicationState): void;
}

export const initArticle = 
{
    id:     "",
    title:  "",
    desc:   "",
    status: "",
    likes:  0,
    readCount: 0
};

export const initArticles = 
{
    articles:  [],
    isLoading: false
};

export const initialState = 
{

    selectArticle:
    {
        id:     initArticle.id,
        title:  initArticle.title,
        desc:   initArticle.desc,
        status: initArticle.status,
        likes:  initArticle.likes,
        readCount: initArticle.readCount
    },

    listArticles:
    {
        articles:  initArticles.articles,
        isLoading: initArticles.isLoading
    }

};
