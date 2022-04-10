import * as React from "react";
import Card from "@material-ui/core/Card";
import CardActions from "@material-ui/core/CardActions";
import CardContent from "@material-ui/core/CardContent";
import Button from "@material-ui/core/Button";
import Typography from "@material-ui/core/Typography";
import { Grid } from "@material-ui/core";
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
            <Card className={classes.root} elevation={4}>
                <Grid container spacing={2}>
                    <Grid item xs={4}>
                        <img className={classes.image} alt="" src={props.bind?.imageUrl} />
                    </Grid>
                    <Grid item xs={8}>
                        <CardContent>
                            <Typography gutterBottom={true} className={classes.title}>
                                {props.bind?.title}
                            </Typography>
                            <Typography className={classes.description}>
                                {props.bind?.description}
                            </Typography>
                        </CardContent>
                        <CardActions>
                            <Button onClick={props.bind?.onClickEvent} size="small" className={classes.button}>{props.bind?.buttonText}</Button>
                        </CardActions>
                    </Grid>
                </Grid>
            </Card>
        </div>
    );
}

export default ArticleCardView;
