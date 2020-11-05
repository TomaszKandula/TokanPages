import React from 'react';
import { Link } from 'react-router-dom';
import { makeStyles } from '@material-ui/core/styles';
import Card from '@material-ui/core/Card';
import CardActionArea from '@material-ui/core/CardActionArea';
import CardActions from '@material-ui/core/CardActions';
import CardContent from '@material-ui/core/CardContent';
import CardMedia from '@material-ui/core/CardMedia';
import Button from '@material-ui/core/Button';
import Typography from '@material-ui/core/Typography';
import { STORAGE_URL } from "../../Shared/apis";

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
    }
});

export default function Article(props: IArticle)
{

    const classes = useStyles();
    const articleUrl = `/articles/?id=${props.uid}`;
    const imageUrl = `${STORAGE_URL}/content/articles/${props.uid}/image.jpg`;

    return(
        <Card className={classes.root} elevation={4}>
            <CardActionArea>
                <CardMedia
                    className={classes.media}
                    image={imageUrl}
                    title={props.title}
                />
                <CardContent>
                    <Typography gutterBottom variant="h5" component="h2">
                        {props.title}
                    </Typography>
                    <Typography variant="body2" color="textSecondary" component="p">
                        {props.desc}
                    </Typography>
                </CardContent>
            </CardActionArea>
            <CardActions>
                <Link to={articleUrl} className={classes.link}>
                    <Button size="small" color="primary">Read</Button>
                </Link>
            </CardActions>
        </Card>
    );

}
