export interface IUpdateArticleDto 
{
    id: string;
    title?: string;
    description?: string;
    textToUpload?: string;
    imageToUpload?: string;
    isPublished?: boolean;
    addToLikes: number;
    upReadCount?: boolean;
}
