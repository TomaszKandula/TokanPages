import * as React from "react";
import { Link } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Grid from "@material-ui/core/Grid";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import Button from "@material-ui/core/Button";
import ArrowRightAltIcon from "@material-ui/icons/ArrowRightAlt";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import Skeleton from "@material-ui/lab/Skeleton";
import { IArticleFeat } from "../../Api/Models";
import { renderCardMedia } from "../../Shared/Components/renderCardMedia";
import useStyles from "./Hooks/styleArticleFeat";

export default function ArticleFeat(props: { articles: IArticleFeat, isLoading: boolean }) 
{
    const classes = useStyles();
    return (
        <section className={classes.section}>
            <Container maxWidth="lg">
                <div data-aos="fade-up">
                    <Box pt={8} pb={10}>
                        <Container maxWidth="sm">
                            <Box textAlign="center" mb={5}>
                                <Typography variant="h4" component="h2" gutterBottom={true}>
                                    {props.isLoading ? <Skeleton variant="text" /> : props.articles.content.title}
                                </Typography>
                                <Typography variant="subtitle1" color="textSecondary">
                                    {props.isLoading ? <Skeleton variant="text" /> : props.articles.content.desc}
                                </Typography>
                            </Box>
                        </Container>
                        <Container maxWidth="lg">
                            <Grid container>
                                <Grid item xs={12} lg={6} className={classes.firstColumn}>
                                    <Card className={classes.info} elevation={3}>
                                        <CardContent className={classes.info}>
                                            <Box display="flex" flexDirection="column" height="100%" pt={2} px={2}>
                                                <Typography variant="h5" component="h2" gutterBottom={true}>
                                                    {props.isLoading ? <Skeleton variant="text" /> : props.articles.content.text1}
                                                </Typography>
                                                <Box mt="auto" mb={2}>
                                                    <Typography variant="body1" component="p" color="textSecondary">
                                                        {props.isLoading ? <Skeleton variant="text" /> : props.articles.content.text2}
                                                    </Typography>
                                                </Box>
                                                <Box textAlign="right">
                                                    <Link to="/articles" className={classes.link}>
                                                        <Button color="primary" endIcon={<ArrowRightAltIcon />}>
                                                            {props.isLoading ? <Skeleton variant="text" /> : props.articles.content.button}
                                                        </Button>
                                                    </Link>
                                                </Box>
                                            </Box>
                                        </CardContent>
                                    </Card>
                                </Grid>
                                <Grid item xs={12} lg={6}>
                                    <Grid container spacing={2}>
                                        <Grid item xs={12} md={8}>
                                            <Card elevation={4}>
                                                {props.isLoading 
                                                    ? <Skeleton variant="rect" height="128px" /> 
                                                    : renderCardMedia(props.articles.content.image1, classes.media)}
                                            </Card>
                                        </Grid>
                                        <Grid item xs={12} md={4}>
                                            <Card elevation={4}>
                                                {props.isLoading 
                                                    ? <Skeleton variant="rect" height="128px" /> 
                                                    : renderCardMedia(props.articles.content.image2, classes.media)}
                                            </Card>
                                        </Grid>
                                        <Grid item xs={12} md={4}>
                                            <Card elevation={4}>
                                                {props.isLoading 
                                                    ? <Skeleton variant="rect" height="128px" /> 
                                                    : renderCardMedia(props.articles.content.image3, classes.media)}
                                            </Card>
                                        </Grid> 
                                        <Grid item xs={12} md={8}>
                                            <Card elevation={4}>
                                                {props.isLoading 
                                                    ? <Skeleton variant="rect" height="128px" /> 
                                                    : renderCardMedia(props.articles.content.image4, classes.media)}
                                            </Card>
                                        </Grid>
                                    </Grid>
                                </Grid>
                            </Grid>
                        </Container>
                    </Box>
                </div>
            </Container>
        </section>
    );
}
