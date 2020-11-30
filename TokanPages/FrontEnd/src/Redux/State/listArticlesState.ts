export interface IListArticlesState
{
    isLoading: boolean;
    articles: IListArticles[];
}

export interface IListArticles
{
    id: string;
    title: string;
    desc: string;
    status: string;
    likes: number;
    readCount: number;
}
