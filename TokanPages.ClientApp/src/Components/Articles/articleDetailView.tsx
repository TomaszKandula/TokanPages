import * as React from "react";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import { Divider, Grid, IconButton, Popover, Tooltip, Typography } from "@material-ui/core";
import { ArrowBack } from "@material-ui/icons";
import ThumbUpIcon from "@material-ui/icons/ThumbUp";
import Emoji from "react-emoji-render";
import articleDetailStyle from "./Styles/articleDetailStyle";
import { FormatDateTime } from "../../Shared/helpers";

interface IBinding 
{
    bind: IProperties;
}

interface IProperties
{
    backButtonHandler: any;
    articleReadCount: number;
    openPopoverHandler: any;
    closePopoverHandler: any;
    renderSmallAvatar: JSX.Element;
    renderLargeAvatar: JSX.Element;
    authorAliasName: string;
    popoverOpen: boolean;
    popoverElement: HTMLElement | null;
    authorFirstName: string;
    authorLastName: string;
    authorRegistered: string;
    articleReadTime: string;
    articleCreatedAt: string;
    articleUpdatedAt: string;
    articleContent: JSX.Element;
    renderLikesLeft: string;
    thumbsHandler: any;
    totalLikes: number;
    renderAuthorName: string;
    authorShortBio: string;
}

const ArticleDetailView = (props: IBinding): JSX.Element =>
{
    const classes = articleDetailStyle();
    return (
        <section>
            <Container className={classes.container}>
                <Box py={12}>
                    <div data-aos="fade-down">
                        <Grid container spacing={3}>
                            <Grid item xs={6}>
                                <IconButton onClick={props.bind?.backButtonHandler}>
                                    <ArrowBack  /> 
                                </IconButton>
                            </Grid>
                            <Grid item xs={6}>
                                <Typography className={classes.readCount} component="p" variant="subtitle1" align="right">
                                    Read: {props.bind?.articleReadCount}
                                </Typography>
                            </Grid>
                        </Grid>
                        <Divider className={classes.dividerTop} />
                        <Grid container spacing={2}>
                            <Grid item>
                                <Box onMouseEnter={props.bind?.openPopoverHandler} onMouseLeave={props.bind?.closePopoverHandler}>
                                    {props.bind?.renderSmallAvatar}
                                </Box>
                            </Grid>
                            <Grid item xs zeroMinWidth>
                                <Typography className={classes.aliasName} component="div" variant="subtitle1" align="left">
                                    <Box fontWeight="fontWeightBold">
                                        {props.bind?.authorAliasName}
                                    </Box>
                                </Typography>
                                <Popover
                                    id="mouse-over-popover"
                                    className={classes.popover}
                                    open={props.bind?.popoverOpen}
                                    anchorEl={props.bind?.popoverElement}
                                    anchorOrigin={{ vertical: "bottom", horizontal: "left" }}
                                    transformOrigin={{ vertical: "top", horizontal: "left" }}
                                    onClose={props.bind?.closePopoverHandler}
                                    disableRestoreFocus
                                >
                                    <Box mt={2} mb={2} ml={3} mr={3} >
                                        <Typography component="p" variant="subtitle2" color="textSecondary">
                                            First name: {props.bind?.authorFirstName}
                                        </Typography>
                                        <Typography component="p" variant="subtitle2" color="textSecondary">
                                            Last name: {props.bind?.authorLastName}
                                        </Typography>
                                        <Typography component="p" variant="subtitle2" color="textSecondary">
                                            Registered at: {props.bind?.authorRegistered}
                                        </Typography>
                                    </Box>
                                </Popover>
                            </Grid>
                        </Grid>
                        <Box mt={1} mb={5}>
                            <Typography component="p" variant="subtitle1">
                                Read time: {props.bind?.articleReadTime} min.
                            </Typography>
                            <Typography component="p" variant="subtitle1">
                                Published at: {FormatDateTime(props.bind?.articleCreatedAt, true)}
                            </Typography>
                            <Typography component="p" variant="subtitle2" color="textSecondary">
                                Updated at: {FormatDateTime(props.bind?.articleUpdatedAt, true)}
                            </Typography>
                        </Box>
                    </div>
                    <div data-aos="fade-up">
                        {props.bind?.articleContent}
                    </div>
                    <Box mt={5}>
                        <Grid container spacing={2}>
                            <Grid item>
                                <Tooltip title=
                                    {<span className={classes.likesTip}>
                                        {<Emoji text={props.bind?.renderLikesLeft}/>}
                                    </span>} arrow>
                                    <ThumbUpIcon className={classes.thumbsMedium} onClick={props.bind?.thumbsHandler} />
                                </Tooltip>
                            </Grid>
                            <Grid item xs zeroMinWidth>
                                <Typography component="p" variant="subtitle1">
                                    {props.bind?.totalLikes}
                                </Typography>
                            </Grid>
                        </Grid>
                    </Box>
                    <Divider className={classes.dividerBottom} />
                    <Grid container spacing={2}>
                        <Grid item>
                            {props.bind?.renderLargeAvatar}
                        </Grid>
                        <Grid item xs zeroMinWidth>
                            <Typography className={classes.aliasName} component="span" variant="h6" align="left" color="textSecondary">
                                Written by
                            </Typography>
                            <Box fontWeight="fontWeightBold">
                                <Typography className={classes.aliasName} component="span" variant="h6" align="left">
                                    {props.bind?.renderAuthorName}
                                </Typography>
                            </Box>
                            <Typography className={classes.aliasName} component="span" variant="subtitle1" align="left" color="textSecondary">
                                About the author: {props.bind?.authorShortBio}
                            </Typography>
                        </Grid>
                    </Grid>
                </Box>
            </Container>
        </section>
    );
}

export default ArticleDetailView;
