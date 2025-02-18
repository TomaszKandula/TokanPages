import * as React from "react";
import Card from "@material-ui/core/Card";
import CardMedia from "@material-ui/core/CardMedia";
import CardActions from "@material-ui/core/CardActions";
import CardContent from "@material-ui/core/CardContent";
import Button from "@material-ui/core/Button";
import Typography from "@material-ui/core/Typography";
import VisibilityIcon from "@material-ui/icons/Visibility";
import LanguageIcon from "@material-ui/icons/Language";
import ThumbUpIcon from "@material-ui/icons/ThumbUp";
import { GET_FLAG_URL } from "../../../../Api/Request";
import { Animated, RenderImage } from "../../../../Shared/Components";
import Validate from "validate.js";

interface ArticleCardViewProps {
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
    const styleCard = props.styleSmallCard ? "article-card-action-small" : "article-card-action-large"

    return (
        <Animated isDisabled={!props.canAnimate} dataAos="fade-up">
            <Card elevation={0} className="article-card">
                <CardMedia image={props.imageUrl} className="article-card-image" />
                <CardContent className="article-card-content">
                    <Typography className="article-card-title">{props.title}</Typography>
                    <Typography className="article-card-description">{props.description}</Typography>
                    <div className={`article-card-action-container ${styleCard}`}>
                        <CardActions className="article-card-action">
                            <div className="article-card-details">
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
                                            alt="An article language flag"
                                            className="article-flag-image" 
                                        />
                                    </> 
                                ) : (
                                    <></>
                                )}
                            </div>
                            <div className="article-card-button-holder">
                                <Button onClick={props.onClickEvent} size="small" className="button article-button">
                                    {props.buttonText}
                                </Button>
                            </div>
                        </CardActions>
                    </div>
                </CardContent>
            </Card>
        </Animated>
    );
};
