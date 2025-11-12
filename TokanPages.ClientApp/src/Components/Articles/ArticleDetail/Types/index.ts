import { ArticleContentDto } from "../../../../Api/Models";

export interface ExtendedViewProps {
    className?: string;
}

export interface ArticleDetailProps extends ExtendedViewProps {
    title: string;
}

export interface ArticleDetailViewProps extends ExtendedViewProps {
    isLoading: boolean;
    isMobile: boolean | null;
    backButtonHandler: () => void;
    articleReadCount: string;
    renderSmallAvatar: React.ReactElement;
    renderLargeAvatar: React.ReactElement;
    authorFirstName: string;
    authorLastName: string;
    authorRegistered: string;
    articleTags?: string[];
    articleReadTime: string;
    articleCreatedAt: string;
    articleUpdatedAt: string;
    articleContent: React.ReactElement;
    renderLikesLeft: string;
    thumbsHandler: () => void;
    totalLikes: string;
    authorShortBio: string;
    flagImage: string;
    content: ArticleContentDto;
}
