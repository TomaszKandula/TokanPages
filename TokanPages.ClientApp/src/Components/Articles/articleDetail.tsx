import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router-dom";
import Validate from "validate.js";
import { IApplicationState } from "../../Redux/applicationState";
import { ActionCreators as SelectArticleActions } from "../../Redux/Actions/Articles/selectArticleAction";
import { ActionCreators as UpdateArticleAction } from "../../Redux/Actions/Articles/updateArticleAction";
import { LIKES_LIMIT_FOR_ANONYM, LIKES_LIMIT_FOR_USER } from "../../Shared/constants";
import { FormatDateTime } from "../../Shared/helpers";
import { ArticleContent } from "./Renderers/articleContent";
import { AuthorName } from "./Renderers/authorName";
import { LikesLeft } from "./Renderers/likesLeft";
import { ReadTime } from "./Renderers/readTime";
import ArticleDetailView from "./articleDetailView";
import UserAvatar from "../../Shared/Components/UserAvatar/userAvatar";

export interface IArticleDetail
{
    id: string;
}

const ArticleDetail = (props: IArticleDetail): JSX.Element =>
{
    const dispatch = useDispatch();
    const selection = useSelector((state: IApplicationState) => state.selectArticle);
    const user = useSelector((state: IApplicationState) => state.updateUserData);

    if (Validate.isEmpty(selection.article.id) && !selection.isLoading)
        dispatch(SelectArticleActions.selectArticle(props.id));

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

    const updateUserLikes = React.useCallback(() => 
    {
        dispatch(UpdateArticleAction.updateArticleLikes(
        { 
            id: props.id, 
            addToLikes: userLikes, 
        }))
    }, 
    [ dispatch, userLikes, props.id ]);

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
        dispatch(UpdateArticleAction.updateArticleCount(
        {
            id: props.id
        }));
    }, 
    [ dispatch, props.id, selection.isLoading ]);

    React.useEffect(() =>
    {
        const intervalId = setInterval(() => 
        { 
            if (userLikes === 0 || !thumbClicked) return;
            updateUserLikes(); 
            setUserLikes(0);
            setThumbsClicked(false);
        }, 
        3000);
        
        return(() => { clearInterval(intervalId) });
    }, [ userLikes, thumbClicked, updateUserLikes ]);

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
        dispatch(SelectArticleActions.resetSelection());
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

    return (<ArticleDetailView bind={
    {
        backButtonHandler: backButtonHandler,
        articleReadCount: selection.article.readCount,
        openPopoverHandler: openPopoverHandler,
        closePopoverHandler: closePopoverHandler,
        renderSmallAvatar: <UserAvatar isLargeScale={false} avatarName={selection.article.author.avatarName} userLetter={userLetter} />,
        renderLargeAvatar: <UserAvatar isLargeScale={true} avatarName={selection.article.author.avatarName} userLetter={userLetter} />,
        authorAliasName: selection.article.author.aliasName,
        popoverOpen: popoverOpen,
        popoverElement: popoverElement,
        authorFirstName: selection.article.author.firstName,
        authorLastName: selection.article.author.lastName,
        authorRegistered: FormatDateTime(selection.article.author.registered, false),
        articleReadTime: ReadTime(selection.article.text),
        articleCreatedAt: FormatDateTime(selection.article.createdAt, true),
        articleUpdatedAt: FormatDateTime(selection.article.updatedAt, true),
        articleContent: ArticleContent(selection.article.id, selection.isLoading, selection.article.text),
        renderLikesLeft: LikesLeft(isAnonymous, likesLeft),
        thumbsHandler: thumbsHandler,
        totalLikes: totalLikes,
        renderAuthorName: AuthorName(selection.article.author.firstName, selection.article.author.lastName, selection.article.author.aliasName),
        authorShortBio: selection.article.author.shortBio
    }}/>);
}

export default ArticleDetail;
