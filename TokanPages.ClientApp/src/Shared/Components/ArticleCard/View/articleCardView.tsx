import * as React from "react";
import Card from "@material-ui/core/Card";
import CardMedia from "@material-ui/core/CardMedia";
import CardActions from "@material-ui/core/CardActions";
import CardContent from "@material-ui/core/CardContent";
import Button from "@material-ui/core/Button";
import VisibilityIcon from "@material-ui/icons/Visibility";
import LanguageIcon from "@material-ui/icons/Language";
import ThumbUpIcon from "@material-ui/icons/ThumbUp";
import { GET_FLAG_URL } from "../../../../Api";
import { Animated, RenderImage } from "../../../../Shared/Components";
import { ViewProperties } from "../../../../Shared/Abstractions";
import Validate from "validate.js";
import Skeleton from "@material-ui/lab/Skeleton";

interface ArticleCardViewProps extends ViewProperties {
    imageUrl: string;
    title: string;
    description: string;
    onClickEvent: () => void;
    buttonText: string;
    flagImage: string;
    canAnimate: boolean;
    readCount?: string;
    totalLikes?: string;
    styleSmallCard?: boolean;
}

export const ArticleCardView = (props: ArticleCardViewProps): React.ReactElement => {
    const hasFlag = !Validate.isEmpty(props.flagImage);
    const styleCard = props.styleSmallCard ? "article-card-action-small" : "article-card-action-large";

    return (
        <Animated isDisabled={!props.canAnimate} dataAos="fade-up">
            <Card elevation={0} className="article-card">
                {props.isLoading 
                ? <Skeleton variant="rect" width="200px" height="200px" />
                : <CardMedia
                    component="img"
                    loading="lazy"
                    image={props.imageUrl}
                    className="article-card-image lazyloaded"
                    title="Article illustration"
                    alt="An article card for given article"
                />}

                <CardContent className="article-card-content">
                    {props.isLoading 
                    ? <Skeleton variant="rect" width="150px" height="30px" />
                    : <h2 className="article-card-title m-zero">{props.title}</h2>}

                    {props.isLoading 
                    ? <Skeleton variant="rect" width="100px" height="30px" /> 
                    : <h3 className="article-card-description m-zero">{props.description}</h3>}

                    <div className={`article-card-action-container ${styleCard}`}>
                        <CardActions className="article-card-action">
                            {props.isLoading 
                            ? <Skeleton variant="rect" width="190px" height="30px" /> 
                            : <div className="article-card-details">
                                {props.readCount === undefined ? (
                                    <></>
                                ) : (
                                    <>
                                        <VisibilityIcon className="article-card-item-icon" />
                                        <div className="article-card-item-value">{props.readCount}</div>
                                    </>
                                )}
                                <div className="article-card-details-separator"></div>
                                {props.totalLikes === undefined ? (
                                    <></>
                                ) : (
                                    <>
                                        <ThumbUpIcon className="article-card-item-icon" />
                                        <div className="article-card-item-value">{props.totalLikes}</div>
                                    </>
                                )}
                                {hasFlag ? (
                                    <>
                                        <div className="article-card-details-separator"></div>
                                        <LanguageIcon className="article-card-item-icon" />
                                        <div className="article-card-details-separator"></div>
                                        <RenderImage
                                            base={GET_FLAG_URL}
                                            source={props.flagImage}
                                            title="Articles"
                                            alt="An article language flag"
                                            className="article-flag-image"
                                        />
                                    </>
                                ) : (
                                    <></>
                                )}
                            </div>}

                            <div className="article-card-button-holder">
                                {props.isLoading 
                                ? <Skeleton variant="rect" width="64px" height="30px" /> 
                                : <Button onClick={props.onClickEvent} size="small" className="button article-button">
                                    {props.buttonText}
                                </Button>}
                            </div>

                        </CardActions>
                    </div>
                </CardContent>
            </Card>
        </Animated>
    );
};
