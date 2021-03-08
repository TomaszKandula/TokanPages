import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import { 
    Avatar,
    Divider, 
    Grid, 
    IconButton, 
    Popover, 
    Tooltip, 
    Typography 
} from "@material-ui/core";
import { ArrowBack } from "@material-ui/icons";
import ThumbUpIcon from "@material-ui/icons/ThumbUp";
import Emoji from "react-emoji-render";
import Validate from "validate.js";
import useStyles from "./Hooks/styleArticleDetail";
import { IApplicationState } from "../../Redux/applicationState";
import { ActionCreators } from "../../Redux/Actions/selectArticleActions";
import CenteredCircularLoader from "../../Shared/ProgressBar/centeredCircularLoader";
import { RenderContent } from "../../Shared/ContentRender/renderContent";
import { CountWords, FormatDateTime, GetReadTime, TextObjectToRawText } from "../../Shared/helpers";
import { ITextObject } from "../../Shared/ContentRender/Model/textModel";
import { UpdateArticle } from "../../Api/Services/articles";
import { 
    AVATARS_PATH, 
    LIKES_LIMIT_FOR_ANONYM, 
    LIKES_LIMIT_FOR_USER,
    LIKES_TIP_FOR_ANONYM,
    LIKES_TIP_FOR_USER,
    MAX_LIKES_REACHED,
    WORDS_PER_MINUTE
} from "../../Shared/constants";

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
        dispatch(ActionCreators.selectArticle(props.id));
    }

    const [popover, setPopover] = React.useState<HTMLElement | null>(null);
    const [totalThumbs, setTotalThumbs] = React.useState(0);
    const [totalLikes, setTotalLikes] = React.useState(0);
    const [userLikes, setUserLikes] = React.useState(0);
    const [thumbClicked, setThumbsClicked] = React.useState(false);
    const [likesLeft, setLikesLeft] = React.useState(0);

    const classes = useStyles();
    const history = useHistory();
    const open = Boolean(popover);
    const userLetter = selection.article.author.aliasName.charAt(0).toUpperCase();
    const isAnonymous = true; // TODO: use authorization feature

    React.useEffect(() => 
    { 
        setTotalLikes(selection.article.likeCount);
    }, 
    [ selection.article.likeCount ]);

    React.useEffect(() => 
    { 
        let likesLeft = isAnonymous 
            ? LIKES_LIMIT_FOR_ANONYM - selection.article.userLikes - totalThumbs
            : LIKES_LIMIT_FOR_USER - selection.article.userLikes - totalThumbs;

        setLikesLeft(likesLeft);
    }, 
    [ selection.article.userLikes, isAnonymous, totalThumbs ]);

    const updateUserLikes = React.useCallback(async () => 
    {
        await UpdateArticle(
        {
            id: props.id,
            addToLikes: userLikes,
            upReadCount: false
        });   
    }, 
    [ userLikes, props.id ]);

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
        await UpdateArticle(
        {
            id: props.id,
            addToLikes: 0,
            upReadCount: true
        });
    }, 
    [ props.id ]);

    React.useEffect(() => 
    {
        if (selection.isLoading) return;
        updateReadCount();
    }, 
    [ selection.isLoading, updateReadCount ]);

    const thumbsUp = () =>
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

    const backToList = () =>
    {
        dispatch(ActionCreators.resetSelection());
        history.push("/articles");
    };

    const openPopover = (event: React.MouseEvent<HTMLElement, MouseEvent>) => 
    {
        setPopover(event.currentTarget);
    };

    const closePopover = () => 
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
            ? LIKES_TIP_FOR_ANONYM.replace("{LEFT_LIKES}", likesLeft.toString()) 
            : LIKES_TIP_FOR_USER.replace("{LEFT_LIKES}", likesLeft.toString());

        const textOut = likesLeft === 0 
            ? MAX_LIKES_REACHED 
            : textLikesLeft;

        return(textOut);
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

    return (
        <section>
            <Container className={classes.container}>
                <Box py={12}>
                    <div data-aos="fade-down">
                        <Grid container spacing={3}>
                            <Grid item xs={6}>
                                <IconButton onClick={backToList}>
                                    <ArrowBack  /> 
                                </IconButton>
                            </Grid>
                            <Grid item xs={6}>
                                <Typography className={classes.readCount} component="p" variant="subtitle1" align="right">
                                    Read: {selection.article.readCount}
                                </Typography>
                            </Grid>
                        </Grid>
                        <Divider className={classes.dividerTop} />
                        <Grid container spacing={2}>
                            <Grid item>
                                <Box onMouseEnter={openPopover} onMouseLeave={closePopover}>
                                    {renderAvatar(false)}
                                </Box>
                            </Grid>
                            <Grid item xs zeroMinWidth>
                                <Typography className={classes.aliasName} component="div" variant="subtitle1" align="left">
                                    <Box fontWeight="fontWeightBold">
                                        {selection.article.author.aliasName}
                                    </Box>
                                </Typography>
                                <Popover
                                    id="mouse-over-popover"
                                    className={classes.popover}
                                    open={open}
                                    anchorEl={popover}
                                    anchorOrigin={{ vertical: "bottom", horizontal: "left" }}
                                    transformOrigin={{ vertical: "top", horizontal: "left" }}
                                    onClose={closePopover}
                                    disableRestoreFocus
                                >
                                    <Box mt={2} mb={2} ml={3} mr={3} >
                                        <Typography component="p" variant="subtitle2" color="textSecondary">
                                            First name: {selection.article.author.firstName}
                                        </Typography>
                                        <Typography component="p" variant="subtitle2" color="textSecondary">
                                            Last name: {selection.article.author.lastName}
                                        </Typography>
                                        <Typography component="p" variant="subtitle2" color="textSecondary">
                                            Registered at: {FormatDateTime(selection.article.author.registered, false)}
                                        </Typography>
                                    </Box>
                                </Popover>
                            </Grid>
                        </Grid>
                        <Box mt={1} mb={5}>
                            <Typography component="p" variant="subtitle1">
                                Read time: {returnReadTime()} min.
                            </Typography>
                            <Typography component="p" variant="subtitle1">
                                Published at: {FormatDateTime(selection.article.createdAt, true)}
                            </Typography>
                            <Typography component="p" variant="subtitle2" color="textSecondary">
                                Updated at: {FormatDateTime(selection.article.updatedAt, true)}
                            </Typography>
                        </Box>
                    </div>
                    <div data-aos="fade-up">
                        {renderContent()}
                    </div>
                    <Box mt={5}>
                        <Grid container spacing={2}>
                            <Grid item>
                                <Tooltip title=
                                    {<span className={classes.likesTip}>
                                        {<Emoji text={renderLikesLeft()}/>}
                                    </span>} arrow>
                                    <ThumbUpIcon className={classes.thumbsMedium} onClick={thumbsUp} />
                                </Tooltip>
                            </Grid>
                            <Grid item xs zeroMinWidth>
                                <Typography component="p" variant="subtitle1">
                                    {totalLikes}
                                </Typography>
                            </Grid>
                        </Grid>
                    </Box>
                    <Divider className={classes.dividerBottom} />
                    <Grid container spacing={2}>
                        <Grid item>
                            {renderAvatar(true)}
                        </Grid>
                        <Grid item xs zeroMinWidth>
                            <Typography className={classes.aliasName} component="span" variant="h6" align="left" color="textSecondary">
                                Written by
                            </Typography>
                            <Box fontWeight="fontWeightBold">
                                <Typography className={classes.aliasName} component="span" variant="h6" align="left">
                                    {renderAuthorName()}
                                </Typography>
                            </Box>
                            <Typography className={classes.aliasName} component="span" variant="subtitle1" align="left" color="textSecondary">
                                About the author: {selection.article.author.shortBio}
                            </Typography>
                        </Grid>
                    </Grid>
                </Box>
            </Container>
        </section>
    );
}
