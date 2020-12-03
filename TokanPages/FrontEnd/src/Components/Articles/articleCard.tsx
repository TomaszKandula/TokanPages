import * as React from "react";
import { Link } from "react-router-dom";
import Card from "@material-ui/core/Card";
import CardActions from "@material-ui/core/CardActions";
import CardContent from "@material-ui/core/CardContent";
import Button from "@material-ui/core/Button";
import Typography from "@material-ui/core/Typography";
import { Grid } from "@material-ui/core";
import useStyles from "./Hooks/styleArticleCard";

interface IArticle
{
    title: string;
    desc: string;
    uid: string;
}

export default function ArticleCard(props: IArticle)
{

    const classes = useStyles();
    const content = 
    {
        button: "Read",
        articleUrl: "/articles/?id={UID}",
        imageUrl: "https://maindbstorage.blob.core.windows.net/tokanpages/content/articles/{UID}/image.jpg"
    };
    const articleUrl = content.articleUrl.replace("{UID}", props.uid);
    const imageUrl = content.imageUrl.replace("{UID}", props.uid);

    return(
        <div data-aos="fade-up">
            <Card className={classes.root} elevation={4}>
                <Grid container spacing={2}>
                    <Grid item xs={4}>
                        <img className={classes.img} alt="" src={imageUrl} />
                    </Grid>
                    <Grid item xs={8}>
                        <CardContent>
                            <Typography gutterBottom variant="h5" component="h2">
                                {props.title}
                            </Typography>
                            <Typography variant="body2" color="textSecondary" component="p">
                                {props.desc}
                            </Typography>
                        </CardContent>
                        <CardActions>
                            <Link to={articleUrl} className={classes.link}>
                                <Button size="small" color="primary">{content.button}</Button>
                            </Link>
                        </CardActions>
                    </Grid>
                </Grid>
            </Card>
        </div>
    );

}
