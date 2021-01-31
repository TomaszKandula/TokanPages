import * as React from "react";
import { Link } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import { Divider, Grid, IconButton, Popover, Typography } from "@material-ui/core";
import { ArrowBack } from "@material-ui/icons";
import PersonIcon from "@material-ui/icons/Person";
import Validate from "validate.js";
import useStyles from "./Hooks/styleArticleDetail";
import { IApplicationState } from "../../Redux/applicationState";
import { ActionCreators } from "../../Redux/Actions/selectArticleActions";
import CenteredCircularLoader from "../../Shared/ProgressBar/centeredCircularLoader";
import { RenderContent } from "../../Shared/ContentRender/renderContent";
import { FormatDateTime } from "../../Shared/helpers";
import { UpdateArticle } from "../../Api/Services/articles";
import { AVATARS_PATH } from "../../Shared/constants";

export interface IArticleDetail
{
    id: string;
}

export default function ArticleDetail(props: IArticleDetail) 
{
    const classes = useStyles();
    const [popover, setPopover] = React.useState<HTMLElement | null>(null);
    const selection = useSelector((state: IApplicationState) => state.selectArticle);
    const dispatch = useDispatch();
    const open = Boolean(popover);

    if (Validate.isEmpty(selection.article.id) && !selection.isLoading)
    {
        dispatch(ActionCreators.selectArticle(props.id));
    }
    
    const openPopover = (event: React.MouseEvent<HTMLElement, MouseEvent>) => 
    {
        setPopover(event.currentTarget);
    };

    const closePopover = () => 
    {
        setPopover(null);
    };

    const updateReadCount = async () => 
    {
        await UpdateArticle(
        {
            id: props.id,
            addToLikes: 0,
            upReadCount: true
        });
    };

    const renderContent = () =>
    {
        if (Validate.isEmpty(selection.article.id) || selection.isLoading)
        {
            return(<CenteredCircularLoader />);
        }
 
        updateReadCount();
        return(<RenderContent items={selection.article.text} />);
    };

    const renderAvatar = (isLargeScale: boolean) =>
    {
        const className = isLargeScale ? classes.avatarLarge : classes.avatarSmall;

        if (Validate.isEmpty(selection.article.author.avatarName))
        {
            return(<><PersonIcon className={className} /></>);
        }

        const avatarUrl = AVATARS_PATH + selection.article.author.avatarName;
        return(<><img className={className} src={avatarUrl} alt="" /></>);
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

    return (
        <section>
            <Container className={classes.container}>
                <Box py={12}>
                    <div data-aos="fade-down">
                        <Grid container spacing={3}>
                            <Grid item xs={6}>
                                <Link to="/articles">
                                    <IconButton>
                                        <ArrowBack/>
                                    </IconButton>
                                </Link> 
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
                    <Typography component="p" variant="subtitle1">
                        Votes: {selection.article.likeCount}
                    </Typography>
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
