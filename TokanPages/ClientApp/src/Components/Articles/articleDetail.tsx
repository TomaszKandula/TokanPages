import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router-dom";
import { Avatar } from "@material-ui/core";
import Validate from "validate.js";
import articleDetailStyle from "./Styles/articleDetailStyle";
import { IApplicationState } from "../../Redux/applicationState";
import { ActionCreators as SelectArticleActions } from "../../Redux/Actions/selectArticleAction";
import { ActionCreators as UpdateArticleAction } from "../../Redux/Actions/updateArticleAction";
import CenteredCircularLoader from "../../Shared/Components/ProgressBar/centeredCircularLoader";
import { RenderContent } from "../../Shared/Components/ContentRender/renderContent";
import { CountWords, FormatDateTime, GetReadTime, TextObjectToRawText } from "../../Shared/helpers";
import { ITextObject } from "../../Shared/Components/ContentRender/Models/textModel";
import { 
    AVATARS_PATH, 
    LIKES_LIMIT_FOR_ANONYM, 
    LIKES_LIMIT_FOR_USER,
    LIKES_HINT_FOR_ANONYM,
    LIKES_HINT_FOR_USER,
    MAX_LIKES_REACHED,
    WORDS_PER_MINUTE
} from "../../Shared/constants";
import ArticleDetailView from "./articleDetailView";

export interface IArticleDetail
{
    id: string;
}

export default function ArticleDetail(props: IArticleDetail) 
{
    const dispatch = useDispatch();
    const selection = useSelector((state: IApplicationState) => state.selectArticle);
    if (Validate.isEmpty(selection.article.id) && !selection.isLoading)
    {
        dispatch(SelectArticleActions.selectArticle(props.id));
    }

    const [popoverElement, setPopover] = React.useState<HTMLElement | null>(null);
    const [totalThumbs, setTotalThumbs] = React.useState(0);
    const [totalLikes, setTotalLikes] = React.useState(0);
    const [userLikes, setUserLikes] = React.useState(0);
    const [thumbClicked, setThumbsClicked] = React.useState(false);
    const [likesLeft, setLikesLeft] = React.useState(0);

    const classes = articleDetailStyle();
    const history = useHistory();
    const popoverOpen = Boolean(popoverElement);
    const userLetter = selection.article.author.aliasName.charAt(0).toUpperCase();
    const isAnonymous = true; // TODO: use authorization feature

    React.useEffect(() => 
    { 
        setTotalLikes(selection.article.likeCount);
    }, 
    [ selection.article.likeCount ]);

    React.useEffect(() => 
    { 
        setLikesLeft(isAnonymous 
            ? LIKES_LIMIT_FOR_ANONYM - selection.article.userLikes - totalThumbs
            : LIKES_LIMIT_FOR_USER - selection.article.userLikes - totalThumbs);
    }, 
    [ selection.article.userLikes, isAnonymous, totalThumbs ]);

    const updateUserLikes = React.useCallback(async () => 
    {
        dispatch(UpdateArticleAction.updateArticle(
        {
            id: props.id,
            addToLikes: userLikes,
            upReadCount: false
        }));
    }, 
    [ dispatch, userLikes, props.id ]);

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
    }, 
    [ userLikes, thumbClicked, updateUserLikes ]);

    const updateReadCount = React.useCallback(async () => 
    {
        dispatch(UpdateArticleAction.updateArticle(
        {
            id: props.id,
            addToLikes: 0,
            upReadCount: true
        }));
    }, 
    [ dispatch, props.id ]);

    React.useEffect(() => 
    {
        if (selection.isLoading) return;
        updateReadCount();
    }, 
    [ selection.isLoading, updateReadCount ]);

    const thumbsUpHandler = () =>
    {
        let likesToAdd = isAnonymous 
            ? LIKES_LIMIT_FOR_ANONYM - selection.article.userLikes - totalThumbs
            : LIKES_LIMIT_FOR_USER - selection.article.userLikes - totalThumbs;

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

    const renderContent = () =>
    {
        if (Validate.isEmpty(selection.article.id) || selection.isLoading)
        {
            return(<CenteredCircularLoader />);
        }
 
        return(<RenderContent items={selection.article.text} />);
    };

    const renderAvatar = (isLargeScale: boolean) =>
    {
        const className = isLargeScale ? classes.avatarLarge : classes.avatarSmall;

        if (Validate.isEmpty(selection.article.author.avatarName))
        {
            return(<Avatar className={className}>{userLetter}</Avatar>);
        }

        const avatarUrl = AVATARS_PATH + selection.article.author.avatarName;
        return(<><Avatar className={className} src={avatarUrl} alt="" /></>);
    };

    const renderAuthorName = () => 
    {
        const fullNameWithAlias = selection.article.author.firstName + " '" 
            + selection.article.author.aliasName + "' " 
            + selection.article.author.lastName;
        return selection.article.author.firstName && selection.article.author.lastName 
            ? fullNameWithAlias
            : selection.article.author.aliasName;
    };

    const renderLikesLeft = () =>
    {
        const textLikesLeft = isAnonymous 
            ? LIKES_HINT_FOR_ANONYM.replace("{LEFT_LIKES}", likesLeft.toString()) 
            : LIKES_HINT_FOR_USER.replace("{LEFT_LIKES}", likesLeft.toString());

        return likesLeft === 0 
            ? MAX_LIKES_REACHED 
            : textLikesLeft;
    };

    const returnReadTime = (): string =>
    {
        let textObject: ITextObject = 
        { 
            items: [{ id: "", type: "", value: "", prop: "", text: "" }]
        };
        
        textObject.items = selection.article.text;
        const rawText = TextObjectToRawText(textObject);
        const words = CountWords(rawText);

        return GetReadTime(words, WORDS_PER_MINUTE);
    }

    return (<ArticleDetailView bind={
    {
        backButtonHandler: backButtonHandler,
        articleReadCount: selection.article.readCount,
        openPopoverHandler: openPopoverHandler,
        closePopoverHandler: closePopoverHandler,
        renderSmallAvatar: renderAvatar(false),
        renderLargeAvatar: renderAvatar(true),
        authorAliasName: selection.article.author.aliasName,
        popoverOpen: popoverOpen,
        popoverElement: popoverElement,
        authorFirstName: selection.article.author.firstName,
        authorLastName: selection.article.author.lastName,
        authorRegistered: FormatDateTime(selection.article.author.registered, false),
        articleReadTime: returnReadTime(),
        articleCreatedAt: FormatDateTime(selection.article.createdAt, true),
        articleUpdatedAt: FormatDateTime(selection.article.updatedAt, true),
        articleContent: renderContent(),
        renderLikesLeft: renderLikesLeft(),
        thumbsUpHandler: thumbsUpHandler,
        totalLikes: totalLikes,
        renderAuthorName: renderAuthorName(),
        authorShortBio: selection.article.author.shortBio
    }}/>);
}
