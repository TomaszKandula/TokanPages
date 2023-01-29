import { TextItem } from "../TextModel";
import { Author } from "../AuthorModel";

export interface ArticleItem
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
    author: Author;
    text: TextItem[];
}
