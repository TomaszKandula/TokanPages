import * as React from "react";
import { useDispatch } from "react-redux";
import { useHistory } from "react-router-dom";
import Card from "@material-ui/core/Card";
import CardActions from "@material-ui/core/CardActions";
import CardContent from "@material-ui/core/CardContent";
import Button from "@material-ui/core/Button";
import Typography from "@material-ui/core/Typography";
import { Grid } from "@material-ui/core";
import useStyles from "./Hooks/styleArticleCard";
import { ActionCreators } from "../../Redux/Actions/selectArticleActions";
import { ARTICLE_PATH, IMAGE_URL } from "../../Shared/constants";

export interface IArticleCard
{
    title: string;
    description: string;
    id: string;
}

export default function ArticleCard(props: IArticleCard)
{
    const classes = useStyles();
    const content = 
    {
        button: "Read"
    };

    const articleUrl = ARTICLE_PATH.replace("{ID}", props.id);
    const imageUrl = IMAGE_URL.replace("{ID}", props.id);

    const dispatch = useDispatch();
    const history = useHistory();

    const onClickEvent = () => 
    {
        dispatch(ActionCreators.selectArticle(props.id));
        history.push(articleUrl);        
    };

    return(
        <div data-aos="fade-up">
            <Card className={classes.root} elevation={4}>
                <Grid container spacing={2}>
                    <Grid item xs={4}>
                        <img className={classes.img} alt="" src={imageUrl} />
                    </Grid>
                    <Grid item xs={8}>
                        <CardContent>
                            <Typography gutterBottom variant="h5" component="h2" className={classes.title}>
                                {props.title}
                            </Typography>
                            <Typography variant="body2" color="textSecondary" component="p" className={classes.description}>
                                {props.description}
                            </Typography>
                        </CardContent>
                        <CardActions>
                            <Button onClick={onClickEvent} size="small" color="primary">{content.button}</Button>
                        </CardActions>
                    </Grid>
                </Grid>
            </Card>
        </div>
    );
}
