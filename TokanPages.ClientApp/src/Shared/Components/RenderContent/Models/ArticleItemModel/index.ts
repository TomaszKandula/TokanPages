import { ITextItem } from "../TextModel";
import { IAuthor } from "../AuthorModel";

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
