import * as React from "react";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import { Divider, Grid, IconButton, Popover, Tooltip, Typography } from "@material-ui/core";
import { ArrowBack } from "@material-ui/icons";
import ThumbUpIcon from "@material-ui/icons/ThumbUp";
import Emoji from "react-emoji-render";
import { GET_FLAG_URL } from "../../../../Api/Request";
import { ArticleContentDto } from "../../../../Api/Models";
import { RenderImage } from "../../../../Shared/Components";
import { GetDateTime } from "../../../../Shared/Services/Formatters";
import { ReactMouseEvent } from "../../../../Shared/types";
import { ArticleDetailStyle } from "./articleDetailStyle";

interface Properties {
    backButtonHandler: () => void;
    articleReadCount: number;
    openPopoverHandler: (event: ReactMouseEvent) => void;
    closePopoverHandler: () => void;
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
    flagImage: string;
    content: ArticleContentDto;
}

export const ArticleDetailView = (props: Properties): JSX.Element => {
    const classes = ArticleDetailStyle();
    const readTime = props.content.content.textReadTime.replace("{TIME}", props.articleReadTime);
    return (
        <section className={classes.section}>
            <Container className={classes.container}>
                <Box py={12}>
                    <div data-aos="fade-down">
                        <Grid container spacing={3}>
                            <Grid item xs={6}>
                                <IconButton onClick={props.backButtonHandler}>
                                    <ArrowBack />
                                </IconButton>
                            </Grid>
                            <Grid item xs={6}>
                                <Typography
                                    className={classes.readCount}
                                    component="p"
                                    variant="subtitle1"
                                    align="right"
                                >
                                    {props.content.content.textReadCount}&nbsp;{props.articleReadCount}
                                </Typography>
                            </Grid>
                        </Grid>
                        <Divider className={classes.dividerTop} />
                        <Grid container spacing={2}>
                            <Grid item>
                                <Box onMouseEnter={props.openPopoverHandler} onMouseLeave={props.closePopoverHandler}>
                                    {props.renderSmallAvatar}
                                </Box>
                            </Grid>
                            <Grid item xs zeroMinWidth>
                                <Typography
                                    className={classes.aliasName}
                                    component="div"
                                    variant="subtitle1"
                                    align="left"
                                >
                                    <Box fontWeight="fontWeightBold">{props.authorAliasName}</Box>
                                </Typography>
                                <Popover
                                    id="mouse-over-popover"
                                    className={classes.popover}
                                    open={props.popoverOpen}
                                    anchorEl={props.popoverElement}
                                    anchorOrigin={{ vertical: "bottom", horizontal: "left" }}
                                    transformOrigin={{ vertical: "top", horizontal: "left" }}
                                    onClose={props.closePopoverHandler}
                                    disableRestoreFocus
                                >
                                    <Box mt={2} mb={2} ml={3} mr={3}>
                                        <Typography component="p" variant="subtitle2" color="textSecondary">
                                            {props.content.content.textFirstName}&nbsp;{props.authorFirstName}
                                        </Typography>
                                        <Typography component="p" variant="subtitle2" color="textSecondary">
                                            {props.content.content.textSurname}&nbsp;{props.authorLastName}
                                        </Typography>
                                        <Typography component="p" variant="subtitle2" color="textSecondary">
                                            {props.content.content.textRegistered}&nbsp;{props.authorRegistered}
                                        </Typography>
                                    </Box>
                                </Popover>
                            </Grid>
                        </Grid>
                        <Box mt={1} mb={5}>
                            <div className={classes.text_block}>
                                <Typography component="p" variant="subtitle1">
                                    {props.content.content.textLanguage}&nbsp;
                                </Typography>
                                {RenderImage(GET_FLAG_URL, props.flagImage, classes.flag_image)}
                            </div>
                            <Typography component="p" variant="subtitle1">
                                {readTime}
                            </Typography>
                            <div className={classes.text_block}>
                                <Typography component="p" variant="subtitle1">
                                    {props.content.content.textPublished}
                                </Typography>
                                <Typography component="p" variant="subtitle1" className={classes.text_padding_left}>
                                    {GetDateTime({ value: props.articleCreatedAt, hasTimeVisible: true })}
                                </Typography>
                            </div>
                            <div className={classes.text_block}>
                                <Typography component="p" variant="subtitle1">
                                    {props.content.content.textUpdated}
                                </Typography>
                                <Typography component="p" variant="subtitle1" className={classes.text_padding_left}>
                                    {GetDateTime({ value: props.articleUpdatedAt, hasTimeVisible: true })}
                                </Typography>
                            </div>
                        </Box>
                    </div>
                    <div data-aos="fade-up">{props.articleContent}</div>
                    <Box mt={5}>
                        <Grid container spacing={2}>
                            <Grid item>
                                <Tooltip
                                    title={
                                        <span className={classes.likesTip}>
                                            {<Emoji text={props.renderLikesLeft} />}
                                        </span>
                                    }
                                    arrow
                                >
                                    <ThumbUpIcon className={classes.thumbsMedium} onClick={props.thumbsHandler} />
                                </Tooltip>
                            </Grid>
                            <Grid item xs zeroMinWidth>
                                <Typography component="p" variant="subtitle1">
                                    {props.totalLikes}
                                </Typography>
                            </Grid>
                        </Grid>
                    </Box>
                    <Divider className={classes.dividerBottom} />
                    <Grid container spacing={2}>
                        <Grid item>{props.renderLargeAvatar}</Grid>
                        <Grid item xs zeroMinWidth>
                            <Typography
                                className={classes.aliasName}
                                component="span"
                                variant="h6"
                                align="left"
                                color="textSecondary"
                            >
                                {props.content.content.textWritten}
                            </Typography>
                            <Box fontWeight="fontWeightBold">
                                <Typography className={classes.aliasName} component="span" variant="h6" align="left">
                                    {props.renderAuthorName}
                                </Typography>
                            </Box>
                            <Typography
                                className={classes.aliasName}
                                component="span"
                                variant="subtitle1"
                                align="left"
                                color="textSecondary"
                            >
                                {props.content.content.textAbout}&nbsp;{props.authorShortBio}
                            </Typography>
                        </Grid>
                    </Grid>
                </Box>
            </Container>
        </section>
    );
};
