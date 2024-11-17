import * as React from "react";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import { Divider, Grid, Popover, Tooltip, Typography } from "@material-ui/core";
import ThumbUpIcon from "@material-ui/icons/ThumbUp";
import Emoji from "react-emoji-render";
import { GET_FLAG_URL } from "../../../../Api/Request";
import { ArticleContentDto } from "../../../../Api/Models";
import { RenderImage } from "../../../../Shared/Components";
import { GetDateTime } from "../../../../Shared/Services/Formatters";
import { ReactMouseEvent } from "../../../../Shared/types";
import { ExtendedViewProps } from "../articleDetail";

interface ArticleDetailViewProps extends ExtendedViewProps {
    backButtonHandler: () => void;
    articleReadCount: number;
    openPopoverHandler: (event: ReactMouseEvent) => void;
    closePopoverHandler: () => void;
    renderSmallAvatar: React.ReactElement;
    renderLargeAvatar: React.ReactElement;
    authorAliasName: string;
    popoverOpen: boolean;
    popoverElement: HTMLElement | null;
    authorFirstName: string;
    authorLastName: string;
    authorRegistered: string;
    articleReadTime: string;
    articleCreatedAt: string;
    articleUpdatedAt: string;
    articleContent: React.ReactElement;
    renderLikesLeft: string;
    thumbsHandler: any;
    totalLikes: number;
    renderAuthorName: string;
    authorShortBio: string;
    flagImage: string;
    content: ArticleContentDto;
}

export const ArticleDetailView = (props: ArticleDetailViewProps): React.ReactElement => {
    const readTime = props.content.textReadTime.replace("{TIME}", props.articleReadTime);
    return (
        <section className="section" style={props.background}>
            <Container className="container">
                <Box pb={12}>
                    <div data-aos="fade-down">
                        <Grid container spacing={2}>
                            <Grid item>
                                <Box onMouseEnter={props.openPopoverHandler} onMouseLeave={props.closePopoverHandler}>
                                    {props.renderSmallAvatar}
                                </Box>
                            </Grid>
                            <Grid item xs zeroMinWidth>
                                <Typography
                                    className="alias-name"
                                    component="div"
                                    variant="subtitle1"
                                    align="left"
                                >
                                    <Box fontWeight="fontWeightBold">{props.authorAliasName}</Box>
                                </Typography>
                                <Popover
                                    id="mouse-over-popover"
                                    className="popover"
                                    open={props.popoverOpen}
                                    anchorEl={props.popoverElement}
                                    anchorOrigin={{ vertical: "bottom", horizontal: "left" }}
                                    transformOrigin={{ vertical: "top", horizontal: "left" }}
                                    onClose={props.closePopoverHandler}
                                    disableRestoreFocus
                                >
                                    <Box mt={2} mb={2} ml={3} mr={3}>
                                        <Typography component="p" variant="subtitle2" color="textSecondary">
                                            {props.content.textFirstName}&nbsp;{props.authorFirstName}
                                        </Typography>
                                        <Typography component="p" variant="subtitle2" color="textSecondary">
                                            {props.content.textSurname}&nbsp;{props.authorLastName}
                                        </Typography>
                                        <Typography component="p" variant="subtitle2" color="textSecondary">
                                            {props.content.textRegistered}&nbsp;{props.authorRegistered}
                                        </Typography>
                                    </Box>
                                </Popover>
                            </Grid>
                        </Grid>
                        <Box mt={1} mb={5}>
                            <div className="text-block">
                                <Typography component="p" variant="subtitle1">
                                    {props.content.textLanguage}&nbsp;
                                </Typography>
                                {RenderImage(GET_FLAG_URL, props.flagImage, "flag-image")}
                            </div>
                            <Typography component="p" variant="subtitle1">
                                {readTime}
                            </Typography>
                            <div className="text-block">
                                <Typography component="p" variant="subtitle1">
                                    {props.content.textPublished}
                                </Typography>
                                <Typography component="p" variant="subtitle1" className="text-padding-left">
                                    {GetDateTime({ value: props.articleCreatedAt, hasTimeVisible: true })}
                                </Typography>
                            </div>
                            <div className="text-block">
                                <Typography component="p" variant="subtitle1">
                                    {props.content.textUpdated}
                                </Typography>
                                <Typography component="p" variant="subtitle1" className="text-padding-left">
                                    {GetDateTime({ value: props.articleUpdatedAt, hasTimeVisible: true })}
                                </Typography>
                            </div>
                            <div className="text-block">
                                <Typography component="p" variant="subtitle1">
                                    {props.content.textReadCount}
                                </Typography>
                                <Typography component="p" variant="subtitle1" className="text-padding-left">
                                    {props.articleReadCount}
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
                                        <span className="likes-tip">
                                            {<Emoji text={props.renderLikesLeft} />}
                                        </span>
                                    }
                                    arrow
                                >
                                    <ThumbUpIcon className="thumbs-medium" onClick={props.thumbsHandler} />
                                </Tooltip>
                            </Grid>
                            <Grid item xs zeroMinWidth>
                                <Typography component="p" variant="subtitle1">
                                    {props.totalLikes}
                                </Typography>
                            </Grid>
                        </Grid>
                    </Box>
                    <Divider className="divider-bottom" />
                    <Grid container spacing={2}>
                        <Grid item>{props.renderLargeAvatar}</Grid>
                        <Grid item xs zeroMinWidth>
                            <Typography
                                className="alias-name"
                                component="span"
                                variant="h6"
                                align="left"
                                color="textSecondary"
                            >
                                {props.content.textWritten}
                            </Typography>
                            <Box fontWeight="fontWeightBold">
                                <Typography className="alias-name" component="span" variant="h6" align="left">
                                    {props.renderAuthorName}
                                </Typography>
                            </Box>
                            <Typography
                                className="alias-name"
                                component="span"
                                variant="subtitle1"
                                align="left"
                                color="textSecondary"
                            >
                                {props.content.textAbout}&nbsp;{props.authorShortBio}
                            </Typography>
                        </Grid>
                    </Grid>
                </Box>
            </Container>
        </section>
    );
};
