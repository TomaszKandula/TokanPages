import { TLoading } from "../../../../Shared/Types";

export interface ArticleCardProps {
    id: string;
    title: string;
    description: string;
    languageIso: string;
    canAnimate: boolean;
    canDisplayDate: boolean;
    published: string;
    readCount?: number;
    totalLikes?: number;
    loading?: TLoading;
}
