import * as React from "react";
import Card from "@material-ui/core/Card";
import CardMedia from "@material-ui/core/CardMedia";
import CardActions from "@material-ui/core/CardActions";
import CardContent from "@material-ui/core/CardContent";
import Button from "@material-ui/core/Button";
import Typography from "@material-ui/core/Typography";
import articleCardStyle from "./Styles/articleCardStyle";

interface IBinding
{
    bind: IProperties;
}

interface IProperties
{
    imageUrl: string;
    title: string;
    description: string;
    onClickEvent: any;
    buttonText: string;
}

const ArticleCardView = (props: IBinding): JSX.Element =>
{
    const classes = articleCardStyle();
    return(
        <div data-aos="fade-up">
            <Card elevation={0} className={classes.card} >
                <CardMedia image={props.bind?.imageUrl} className={classes.card_image} />
                <CardContent>
                    <Typography gutterBottom={true} className={classes.card_title}>
                        {props.bind?.title}
                    </Typography>
                    <Typography className={classes.card_description}>
                        {props.bind?.description}
                    </Typography>
                    <CardActions className={classes.card_action}>
                        <Button onClick={props.bind?.onClickEvent} size="small" className={classes.button}>
                            {props.bind?.buttonText}
                        </Button>
                    </CardActions>
                </CardContent>
            </Card>
        </div>
    );
}

export default ArticleCardView;
