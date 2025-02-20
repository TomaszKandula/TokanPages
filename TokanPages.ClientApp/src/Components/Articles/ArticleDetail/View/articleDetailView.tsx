import * as React from "react";
import Container from "@material-ui/core/Container";
import { Grid, Popover, Tooltip, Typography } from "@material-ui/core";
import ThumbUpIcon from "@material-ui/icons/ThumbUp";
import Emoji from "react-emoji-render";
import { GET_FLAG_URL } from "../../../../Api/Request";
import { ArticleContentDto } from "../../../../Api/Models";
import { Animated, CustomDivider, RenderImage } from "../../../../Shared/Components";
import { GetDateTime } from "../../../../Shared/Services/Formatters";
import { ReactMouseEvent } from "../../../../Shared/types";
import { ExtendedViewProps } from "../articleDetail";

interface ArticleDetailViewProps extends ExtendedViewProps {
    backButtonHandler: () => void;
    articleReadCount: string;
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
    totalLikes: string;
    renderAuthorName: string;
    authorShortBio: string;
    flagImage: string;
    content: ArticleContentDto;
}

export const ArticleDetailView = (props: ArticleDetailViewProps): React.ReactElement => {
    const readTime = props.content.textReadTime.replace("{TIME}", props.articleReadTime);
    return (
        <article className={`section ${props.background ?? ""}`}>
            <Container className="container">
                <div className="pb-96">
                    <Animated dataAos="fade-down">
                        <Grid container spacing={2}>
                            <Grid item>
                                <div onMouseEnter={props.openPopoverHandler} onMouseLeave={props.closePopoverHandler}>
                                    {props.renderSmallAvatar}
                                </div>
                            </Grid>
                            <Grid item xs zeroMinWidth>
                                <Typography className="article-details-alias-name" component="div" align="left">
                                    {props.authorAliasName}
                                </Typography>
                                <Popover
                                    id="mouse-over-popover"
                                    className="article-details-popover"
                                    open={props.popoverOpen}
                                    anchorEl={props.popoverElement}
                                    anchorOrigin={{ vertical: "bottom", horizontal: "left" }}
                                    transformOrigin={{ vertical: "top", horizontal: "left" }}
                                    onClose={props.closePopoverHandler}
                                    disableRestoreFocus
                                >
                                    <div className="article-details-user">
                                        <Typography component="p" variant="subtitle2" color="textSecondary">
                                            {props.content.textFirstName}&nbsp;{props.authorFirstName}
                                        </Typography>
                                        <Typography component="p" variant="subtitle2" color="textSecondary">
                                            {props.content.textSurname}&nbsp;{props.authorLastName}
                                        </Typography>
                                        <Typography component="p" variant="subtitle2" color="textSecondary">
                                            {props.content.textRegistered}&nbsp;{props.authorRegistered}
                                        </Typography>
                                    </div>
                                </Popover>
                            </Grid>
                        </Grid>
                        <div className="mt-8 mb-40">
                            <div className="article-details-text-block">
                                <Typography component="p" variant="subtitle1">
                                    {props.content.textLanguage}&nbsp;
                                </Typography>
                                <RenderImage
                                    base={GET_FLAG_URL}
                                    source={props.flagImage}
                                    title="Articles"
                                    alt="An article language flag"
                                    className="article-details-flag-image"
                                />
                            </div>
                            <Typography component="p" variant="subtitle1">
                                {readTime}
                            </Typography>
                            <div className="article-details-text-block">
                                <Typography component="p" variant="subtitle1">
                                    {props.content.textPublished}
                                </Typography>
                                <Typography
                                    component="p"
                                    variant="subtitle1"
                                    className="article-details-text-padding-left"
                                >
                                    {GetDateTime({ value: props.articleCreatedAt, hasTimeVisible: true })}
                                </Typography>
                            </div>
                            <div className="article-details-text-block">
                                <Typography component="p" variant="subtitle1">
                                    {props.content.textUpdated}
                                </Typography>
                                <Typography
                                    component="p"
                                    variant="subtitle1"
                                    className="article-details-text-padding-left"
                                >
                                    {GetDateTime({ value: props.articleUpdatedAt, hasTimeVisible: true })}
                                </Typography>
                            </div>
                            <div className="article-details-text-block">
                                <Typography component="p" variant="subtitle1">
                                    {props.content.textReadCount}
                                </Typography>
                                <Typography
                                    component="p"
                                    variant="subtitle1"
                                    className="article-details-text-padding-left"
                                >
                                    {props.articleReadCount}
                                </Typography>
                            </div>
                        </div>
                    </Animated>
                    <Animated dataAos="fade-up">{props.articleContent}</Animated>
                    <div className="mt-40">
                        <Grid container spacing={2}>
                            <Grid item>
                                <Tooltip
                                    title={
                                        <span className="article-details-likes-tip">
                                            {<Emoji text={props.renderLikesLeft} />}
                                        </span>
                                    }
                                    arrow
                                >
                                    <ThumbUpIcon
                                        className="article-details-thumbs-medium"
                                        onClick={props.thumbsHandler}
                                    />
                                </Tooltip>
                            </Grid>
                            <Grid item xs zeroMinWidth>
                                <Typography component="p" variant="subtitle1">
                                    {props.totalLikes}
                                </Typography>
                            </Grid>
                        </Grid>
                    </div>

                    <CustomDivider mt={32} mb={32} />

                    <Grid container spacing={2}>
                        <Grid item>{props.renderLargeAvatar}</Grid>
                        <Grid item xs zeroMinWidth>
                            <Typography className="alias-name" component="span" variant="h6" color="textSecondary">
                                {props.content.textWritten}
                            </Typography>
                            <Typography component="p" variant="h6" className="article-details-text-500">
                                {props.renderAuthorName}
                            </Typography>
                            <Typography
                                className="alias-name"
                                component="span"
                                variant="subtitle1"
                                color="textSecondary"
                            >
                                {props.content.textAbout}&nbsp;{props.authorShortBio}
                            </Typography>
                        </Grid>
                    </Grid>
                </div>
            </Container>
        </article>
    );
};
