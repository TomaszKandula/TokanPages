import React from 'react';
import { Link } from 'react-router-dom';
import { makeStyles } from '@material-ui/core/styles';
import Card from '@material-ui/core/Card';
import CardActions from '@material-ui/core/CardActions';
import CardContent from '@material-ui/core/CardContent';
import Button from '@material-ui/core/Button';
import Typography from '@material-ui/core/Typography';
import { STORAGE_URL } from "../../Shared/apis";
import { Grid } from '@material-ui/core';

interface IArticle
{
    title: string;
    desc: string;
    uid: string;
}

const useStyles = makeStyles(
{
    root: 
    {
        marginTop: 25,
        marginBottom: 25
    },
    media: 
    {
        height: 140,
    },
    link:
    {
        textDecoration: "none"
    },
    img:
    {
        margin: "auto",
        display: "block",
        objectFit: "cover",
        height: 150,
        maxWidth: "100%"
    }
});

export default function ArticleCard(props: IArticle)
{

    const classes = useStyles();
    const articleUrl = `/articles/?id=${props.uid}`;
    const imageUrl = `${STORAGE_URL}/content/articles/${props.uid}/image.jpg`;

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
                                <Button size="small" color="primary">Read</Button>
                            </Link>
                        </CardActions>
                    </Grid>
                </Grid>
            </Card>
        </div>
    );

}
