import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router-dom";
import { GET_AVATARS_URL } from "../../../Api/Paths";
import { ApplicationState } from "../../../Store/Configuration";
import { ArticleSelectionAction, ArticleUpdateAction } from "../../../Store/Actions";
import { GetDateTime } from "../../../Shared/Services/Formatters";
import { DEFAULT_USER_IMAGE, LIKES_LIMIT_FOR_ANONYM, LIKES_LIMIT_FOR_USER } from "../../../Shared/constants";
import { Author } from "../../../Shared/Components/RenderContent/Models";
import { UserAvatar } from "../../../Shared/Components/UserAvatar";
import { MapLanguage } from "../../../Shared/Services/Utilities";
import { useDimensions } from "../../../Shared/Hooks";
import { FigoureSize } from "../../../Shared/enums";
import { ArticleContent } from "./Helpers/articleContent";
import { LikesLeft } from "./Helpers/likesLeft";
import { ReadTime } from "./Helpers/readTime";
import { ArticleDetailView } from "./View/articleDetailView";
import Validate from "validate.js";

export interface ExtendedViewProps {
    className?: string;
}

export interface ArticleDetailProps extends ExtendedViewProps {
    title: string;
}

const fallbackImagePath = `${GET_AVATARS_URL}/${DEFAULT_USER_IMAGE}`;

export const ArticleDetail = (props: ArticleDetailProps): React.ReactElement => {
    const media = useDimensions();
    const dispatch = useDispatch();
    const history = useHistory();

    const selection = useSelector((state: ApplicationState) => state.articleSelection);
    const user = useSelector((state: ApplicationState) => state.userDataStore);
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const languageId = useSelector((state: ApplicationState) => state.applicationLanguage?.id);
    const template = data.components.templates.templates.articles;
    const content = data.components.pageArticle;

    if (Validate.isEmpty(selection.article.id) && !selection.isLoading) {
        dispatch(ArticleSelectionAction.select({ title: props.title }));
    }

    const [totalThumbs, setTotalThumbs] = React.useState(0);
    const [totalLikes, setTotalLikes] = React.useState(0);
    const [userLikes, setUserLikes] = React.useState(0);
    const [isThumbClicked, setIsThumbsClicked] = React.useState(false);
    const [likesLeft, setLikesLeft] = React.useState(0);

    const deletedUser: Author = {
        userId: "n/a",
        aliasName: "",
        avatarName: "",
        firstName: content.textDeletedUser,
        lastName: "",
        shortBio: "n/a",
        registered: ""
    }

    const author = !Validate.isDefined(selection.article?.author) ? deletedUser : selection.article?.author as Author;
    const userLetter = author.aliasName.charAt(0).toUpperCase();
    const isAnonymous = Validate.isEmpty(user.userData.userId);

    const flagImage = MapLanguage(selection.article.languageIso);

    React.useEffect(() => {
        const likesLimitForAnonym = LIKES_LIMIT_FOR_ANONYM - selection.article.userLikes - totalThumbs;
        const likesLimitForUser = LIKES_LIMIT_FOR_USER - selection.article.userLikes - totalThumbs;
        setLikesLeft(isAnonymous ? likesLimitForAnonym : likesLimitForUser);
    }, [selection.article.userLikes, isAnonymous, totalThumbs]);

    React.useEffect(() => {
        setTotalLikes(selection.article.totalLikes);
    }, [selection.article.totalLikes]);

    React.useEffect(() => {
        if (selection.isLoading) return;
        if (Validate.isEmpty(selection.article.id)) return;
        dispatch(
            ArticleUpdateAction.updateCount({
                id: selection.article.id,
            })
        );
    }, [selection.article.id, selection.isLoading]);

    React.useEffect(() => {
        const intervalId = setInterval(() => {
            if (userLikes === 0 || !isThumbClicked) return;

            dispatch(
                ArticleUpdateAction.updateLikes({
                    id: selection.article.id,
                    addToLikes: userLikes,
                })
            );

            setUserLikes(0);
            setIsThumbsClicked(false);
        }, 3000);

        return () => clearInterval(intervalId);
    }, [userLikes, isThumbClicked]);

    const thumbsHandler = React.useCallback(() => {
        const likesLimitForAnonym = LIKES_LIMIT_FOR_ANONYM - selection.article.userLikes - totalThumbs;
        const likesLimitForUser = LIKES_LIMIT_FOR_USER - selection.article.userLikes - totalThumbs;
        const likesToAdd = isAnonymous ? likesLimitForAnonym : likesLimitForUser;

        if (likesToAdd > 0) {
            setTotalLikes(totalLikes + 1);
            setUserLikes(userLikes + 1);
            setTotalThumbs(totalThumbs + 1);
            setIsThumbsClicked(true);
        }
    }, [selection.article.userLikes, totalThumbs, userLikes, totalLikes, isAnonymous]);

    const backButtonHandler = React.useCallback(() => {
        dispatch(ArticleSelectionAction.reset());
        history.push(`/${languageId}/articles`);
    }, [languageId]);

    return (
        <ArticleDetailView
            isLoading={selection.isLoading}
            isMobile={media.isMobile}
            backButtonHandler={backButtonHandler}
            articleReadCount={selection.article.readCount.toLocaleString(undefined, { minimumFractionDigits: 0 })}
            renderSmallAvatar={
                <UserAvatar
                    userId={author.userId}
                    size={FigoureSize.extralarge}
                    avatarName={author.avatarName}
                    userLetter={userLetter}
                    altSource={fallbackImagePath}
                />
            }
            renderLargeAvatar={
                <UserAvatar
                    userId={author.userId}
                    size={FigoureSize.extralarge}
                    avatarName={author.avatarName}
                    userLetter={userLetter}
                    altSource={fallbackImagePath}
                />
            }
            authorFirstName={author.firstName}
            authorLastName={author.lastName}
            authorRegistered={GetDateTime({ value: author.registered, hasTimeVisible: false })}
            articleReadTime={ReadTime(selection.article.text)}
            articleCreatedAt={GetDateTime({ value: selection.article.createdAt, hasTimeVisible: true })}
            articleUpdatedAt={GetDateTime({ value: selection.article.updatedAt, hasTimeVisible: true })}
            articleContent={ArticleContent(selection.article.id, selection.isLoading, selection.article.text)}
            renderLikesLeft={LikesLeft(isAnonymous, likesLeft, template)}
            thumbsHandler={thumbsHandler}
            totalLikes={totalLikes.toLocaleString(undefined, { minimumFractionDigits: 0 })}
            authorShortBio={author.shortBio}
            flagImage={flagImage}
            content={content}
            className={props.className}
        />
    );
};
