interface IUpdateArticleDto 
{
    id: string;
    title: string;
    description: string;
    textToUpload: string;
    imageToUpload: string;
    isPublished: boolean;
    likes: number;
    readCount: number;
}

export type { IUpdateArticleDto }
