import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router-dom";
import { ApplicationState } from "../../../Store/Configuration";
import { ArticleSelectionAction, ArticleUpdateAction } from "../../../Store/Actions";
import { GetDateTime } from "../../../Shared/Services/Formatters";
import { ArticleContent } from "./Helpers/articleContent";
import { AuthorName } from "./Helpers/authorName";
import { LikesLeft } from "./Helpers/likesLeft";
import { ReadTime } from "./Helpers/readTime";
import { UserAvatar } from "../../../Shared/Components/UserAvatar";
import { ReactMouseEvent } from "../../../Shared/types";
import { ArticleDetailView } from "./View/articleDetailView";
import Validate from "validate.js";

import { LIKES_LIMIT_FOR_ANONYM, LIKES_LIMIT_FOR_USER } from "../../../Shared/constants";

interface Properties {
    id: string;
}

export const ArticleDetail = (props: Properties): JSX.Element => {
    const dispatch = useDispatch();
    const selection = useSelector((state: ApplicationState) => state.articleSelection);
    const user = useSelector((state: ApplicationState) => state.userDataStore);

    if (Validate.isEmpty(selection.article.id) && !selection.isLoading) {
        dispatch(ArticleSelectionAction.select(props.id));
    }

    const [popoverElement, setPopover] = React.useState<HTMLElement | null>(null);
    const [totalThumbs, setTotalThumbs] = React.useState(0);
    const [totalLikes, setTotalLikes] = React.useState(0);
    const [userLikes, setUserLikes] = React.useState(0);
    const [isThumbClicked, setIsThumbsClicked] = React.useState(false);
    const [likesLeft, setLikesLeft] = React.useState(0);

    const history = useHistory();
    const isPopoverOpen = Boolean(popoverElement);
    const userLetter = selection.article.author.aliasName.charAt(0).toUpperCase();
    const isAnonymous = Validate.isEmpty(user.userData.userId);

    React.useEffect(() => {
        const likesLimitForAnonym = LIKES_LIMIT_FOR_ANONYM - selection.article.userLikes - totalThumbs;
        const likesLimitForUser = LIKES_LIMIT_FOR_USER - selection.article.userLikes - totalThumbs;
        setLikesLeft(isAnonymous ? likesLimitForAnonym : likesLimitForUser);
    }, [selection.article.userLikes, isAnonymous, totalThumbs]);

    React.useEffect(() => {
        setTotalLikes(selection.article.likeCount);
    }, [selection.article.likeCount]);

    React.useEffect(() => {
        if (selection.isLoading) return;
        dispatch(
            ArticleUpdateAction.updateCount({
                id: props.id,
            })
        );
    }, [props.id, selection.isLoading]);

    React.useEffect(() => {
        const intervalId = setInterval(() => {
            if (userLikes === 0 || !isThumbClicked) return;

            dispatch(
                ArticleUpdateAction.updateLikes({
                    id: props.id,
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
        history.push("/articles");
    }, []);

    const openPopoverHandler = React.useCallback((event: ReactMouseEvent) => {
        setPopover(event.currentTarget);
    }, []);

    const closePopoverHandler = React.useCallback(() => {
        setPopover(null);
    }, []);

    const smallAvatar = (
        <UserAvatar
            userId={selection.article.author.userId}
            isLarge={false}
            avatarName={selection.article.author.avatarName}
            userLetter={userLetter}
        />
    );

    const largeAvatar = (
        <UserAvatar
            userId={selection.article.author.userId}
            isLarge={true}
            avatarName={selection.article.author.avatarName}
            userLetter={userLetter}
        />
    );

    return (
        <ArticleDetailView
            backButtonHandler={backButtonHandler}
            articleReadCount={selection.article.readCount}
            openPopoverHandler={openPopoverHandler}
            closePopoverHandler={closePopoverHandler}
            renderSmallAvatar={smallAvatar}
            renderLargeAvatar={largeAvatar}
            authorAliasName={selection.article.author.aliasName}
            popoverOpen={isPopoverOpen}
            popoverElement={popoverElement}
            authorFirstName={selection.article.author.firstName}
            authorLastName={selection.article.author.lastName}
            authorRegistered={GetDateTime({ value: selection.article.author.registered, hasTimeVisible: false })}
            articleReadTime={ReadTime(selection.article.text)}
            articleCreatedAt={GetDateTime({ value: selection.article.createdAt, hasTimeVisible: true })}
            articleUpdatedAt={GetDateTime({ value: selection.article.updatedAt, hasTimeVisible: true })}
            articleContent={ArticleContent(selection.article.id, selection.isLoading, selection.article.text)}
            renderLikesLeft={LikesLeft(isAnonymous, likesLeft)}
            thumbsHandler={thumbsHandler}
            totalLikes={totalLikes}
            renderAuthorName={AuthorName(
                selection.article.author.firstName,
                selection.article.author.lastName,
                selection.article.author.aliasName
            )}
            authorShortBio={selection.article.author.shortBio}
        />
    );
};
