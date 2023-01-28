import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router-dom";
import { IApplicationState } from "../../../Store/Configuration";
import { ArticleSelectionAction, ArticleUpdateAction } from "../../../Store/Actions";
import { GetDateTime } from "../../../Shared/Services/Formatters";
import { ArticleContent } from "./Helpers/articleContent";
import { AuthorName } from "./Helpers/authorName";
import { LikesLeft } from "./Helpers/likesLeft";
import { ReadTime } from "./Helpers/readTime";
import { UserAvatar } from "../../../Shared/Components/UserAvatar";
import { ArticleDetailView } from "./View/articleDetailView";
import Validate from "validate.js";

import {
    LIKES_LIMIT_FOR_ANONYM, 
    LIKES_LIMIT_FOR_USER 
} from "../../../Shared/constants";

export interface IArticleDetail
{
    id: string;
}

export const ArticleDetail = (props: IArticleDetail): JSX.Element =>
{
    const dispatch = useDispatch();
    const selection = useSelector((state: IApplicationState) => state.articleSelection);
    const user = useSelector((state: IApplicationState) => state.userDataStore);

    if (Validate.isEmpty(selection.article.id) && !selection.isLoading)
    {
        dispatch(ArticleSelectionAction.select(props.id));
    }

    const [popoverElement, setPopover] = React.useState<HTMLElement | null>(null);
    const [totalThumbs, setTotalThumbs] = React.useState(0);
    const [totalLikes, setTotalLikes] = React.useState(0);
    const [userLikes, setUserLikes] = React.useState(0);
    const [thumbClicked, setThumbsClicked] = React.useState(false);
    const [likesLeft, setLikesLeft] = React.useState(0);

    const history = useHistory();
    const popoverOpen = Boolean(popoverElement);
    const userLetter = selection.article.author.aliasName.charAt(0).toUpperCase();
    const isAnonymous = Validate.isEmpty(user.userData.userId);

    React.useEffect(() =>
    {
        const likesLimitForAnonym = LIKES_LIMIT_FOR_ANONYM - selection.article.userLikes - totalThumbs;
        const likesLimitForUser = LIKES_LIMIT_FOR_USER - selection.article.userLikes - totalThumbs;
        setLikesLeft(isAnonymous ? likesLimitForAnonym : likesLimitForUser);
    }, 
    [ selection.article.userLikes, isAnonymous, totalThumbs ]);

    React.useEffect(() => 
    {
        setTotalLikes(selection.article.likeCount)
    }, 
    [ selection.article.likeCount ]);

    React.useEffect(() => 
    {
        if (selection.isLoading) return; 
        dispatch(ArticleUpdateAction.updateCount(
        {
            id: props.id
        }));
    }, 
    [ props.id, selection.isLoading ]);

    React.useEffect(() =>
    {
        const intervalId = setInterval(() => 
        { 
            if (userLikes === 0 || !thumbClicked) return;

            dispatch(ArticleUpdateAction.updateLikes(
            { 
                id: props.id, 
                addToLikes: userLikes, 
            }));

            setUserLikes(0);
            setThumbsClicked(false);
        }, 
        3000);
        
        return(() => clearInterval(intervalId));
    }, 
    [ userLikes, thumbClicked ]);

    const thumbsHandler = () =>
    {
        const likesLimitForAnonym = LIKES_LIMIT_FOR_ANONYM - selection.article.userLikes - totalThumbs;
        const likesLimitForUser = LIKES_LIMIT_FOR_USER - selection.article.userLikes - totalThumbs;
        const likesToAdd = isAnonymous ? likesLimitForAnonym : likesLimitForUser;

        if (likesToAdd > 0) 
        {
            setTotalLikes(totalLikes + 1);
            setUserLikes(userLikes + 1);
            setTotalThumbs(totalThumbs + 1);
            setThumbsClicked(true);
        }
    };

    const backButtonHandler = () =>
    {
        dispatch(ArticleSelectionAction.reset());
        history.push("/articles");
    };

    const openPopoverHandler = (event: React.MouseEvent<HTMLElement, MouseEvent>) => 
    { 
        setPopover(event.currentTarget); 
    };
    
    const closePopoverHandler = () => 
    {
        setPopover(null);
    };

    const smallAvatar = <UserAvatar 
        userId={selection.article.author.userId}
        isLarge={false}
        avatarName={selection.article.author.avatarName}
        userLetter={userLetter}
    />;

    const largeAvatar = <UserAvatar 
        userId={selection.article.author.userId}
        isLarge={true}
        avatarName={selection.article.author.avatarName}
        userLetter={userLetter}
    />;

    return (<ArticleDetailView
        backButtonHandler={backButtonHandler}
        articleReadCount={selection.article.readCount}
        openPopoverHandler={openPopoverHandler}
        closePopoverHandler={closePopoverHandler}
        renderSmallAvatar={smallAvatar}
        renderLargeAvatar={largeAvatar}
        authorAliasName={selection.article.author.aliasName}
        popoverOpen={popoverOpen}
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
        renderAuthorName={AuthorName(selection.article.author.firstName, selection.article.author.lastName, selection.article.author.aliasName)}
        authorShortBio={selection.article.author.shortBio}
    />);
}
