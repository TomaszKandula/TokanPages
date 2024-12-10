import { TextItem } from "../TextModel";
import { Author } from "../AuthorModel";

export interface ArticleItemBase {
    id: string;
    title: string;
    description: string;
    isPublished: boolean;
    likeCount: number;
    userLikes: number;
    readCount: number;
    createdAt: string;
    updatedAt: string;
    languageIso: string;
}

export interface ArticleItem extends ArticleItemBase {
    author: Author;
    text: TextItem[];
}
