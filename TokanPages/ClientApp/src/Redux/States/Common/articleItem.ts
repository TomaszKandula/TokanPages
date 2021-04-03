import { ITextItem } from "../../../Shared/ContentRender/Model/textModel";
import { IAuthor } from "./author";

export interface IArticleItem
{
    id: string;
    title: string;
    description: string;
    isPublished: boolean;
    likeCount: number;
    userLikes: number;
    readCount: number;
    createdAt: string;
    updatedAt: string;
    author: IAuthor;
    text: ITextItem[];
}
