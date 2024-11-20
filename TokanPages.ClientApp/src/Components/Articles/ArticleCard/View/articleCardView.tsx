import * as React from "react";
import Card from "@material-ui/core/Card";
import CardMedia from "@material-ui/core/CardMedia";
import CardActions from "@material-ui/core/CardActions";
import CardContent from "@material-ui/core/CardContent";
import Button from "@material-ui/core/Button";
import Typography from "@material-ui/core/Typography";
import { GET_FLAG_URL } from "../../../../Api/Request";
import { RenderImage } from "../../../../Shared/Components";

interface Properties {
    imageUrl: string;
    title: string;
    description: string;
    onClickEvent: () => void;
    buttonText: string;
    flagImage: string;
}

export const ArticleCardView = (props: Properties): React.ReactElement => {
    return (
        <div data-aos="fade-up">
            <Card elevation={0} className="article-card">
                <CardMedia image={props.imageUrl} className="article-card-image">
                    {RenderImage(GET_FLAG_URL, props.flagImage, "article-flag-image")}
                </CardMedia>
                <CardContent>
                    <Typography gutterBottom={true} className="article-card-title">
                        {props.title}
                    </Typography>
                    <Typography className="article-card-description">{props.description}</Typography>
                    <CardActions className="article-card-action">
                        <Button
                            onClick={props.onClickEvent}
                            size="small"
                            className="button article-button article-card-button"
                        >
                            {props.buttonText}
                        </Button>
                    </CardActions>
                </CardContent>
            </Card>
        </div>
    );
};
