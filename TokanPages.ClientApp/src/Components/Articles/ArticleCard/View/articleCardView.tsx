import * as React from "react";
import Card from "@material-ui/core/Card";
import CardMedia from "@material-ui/core/CardMedia";
import CardActions from "@material-ui/core/CardActions";
import CardContent from "@material-ui/core/CardContent";
import Button from "@material-ui/core/Button";
import Typography from "@material-ui/core/Typography";
import { GET_FLAG_URL } from "../../../../Api/Request";
import { RenderImage } from "../../../../Shared/Components";
import { ArticleCardStyle } from "./articleCardStyle";

interface Properties {
    imageUrl: string;
    title: string;
    description: string;
    onClickEvent: () => void;
    buttonText: string;
    flagImage: string;
}

export const ArticleCardView = (props: Properties): JSX.Element => {
    const classes = ArticleCardStyle();
    return (
        <div data-aos="fade-up">
            <Card elevation={0} className={classes.card}>
                <CardMedia image={props.imageUrl} className={classes.card_image} >
                    {RenderImage(GET_FLAG_URL, props.flagImage, classes.flag_image)} 
                </CardMedia>
                <CardContent>
                    <Typography gutterBottom={true} className={classes.card_title}>
                        {props.title}
                    </Typography>
                    <Typography className={classes.card_description}>{props.description}</Typography>
                    <CardActions className={classes.card_action}>
                        <Button onClick={props.onClickEvent} size="small" className={classes.button}>
                            {props.buttonText}
                        </Button>
                    </CardActions>
                </CardContent>
            </Card>
        </div>
    );
};
