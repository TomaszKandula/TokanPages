import * as React from "react";
import { Link } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Grid from "@material-ui/core/Grid";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import Button from "@material-ui/core/Button";
import ArrowRightAltIcon from "@material-ui/icons/ArrowRightAlt";
import Card from "@material-ui/core/Card";
import CardMedia from "@material-ui/core/CardMedia";
import CardContent from "@material-ui/core/CardContent";
import useStyles from "./Hooks/styleArticleFeat";

export default function ArticleFeat() 
{
    const classes = useStyles();
    const content = 
    {
        title: "Articles",
        desc: "I write on regular basis on various technologies I work with. I share the knowledge and experience. I believe everyone will find something interesting.",
        text1: "Write-ups on .NET Core, Azure and databases.",
        text2: "Let's dive into Microsoft technology and programming in general. Read about architecture, design patterns, best practices, C#, SQL and other languages.",
        button: "View list of articles",
        image1: "https://maindbstorage.blob.core.windows.net/tokanpages/images/section_articles/image1.jpg",
        image2: "https://maindbstorage.blob.core.windows.net/tokanpages/images/section_articles/image2.jpg",
        image3: "https://maindbstorage.blob.core.windows.net/tokanpages/images/section_articles/image3.jpg",
        image4: "https://maindbstorage.blob.core.windows.net/tokanpages/images/section_articles/image4.jpg"
    };

    return (
        <section className={classes.section}>
            <Container maxWidth="lg">
                <div data-aos="fade-up">
                    <Box pt={8} pb={10}>
                        <Container maxWidth="sm">
                            <Box textAlign="center" mb={5}>
                                <Typography variant="h4" component="h2" gutterBottom={true}>
                                    {content.title}
                                </Typography>
                                <Typography variant="subtitle1" color="textSecondary">
                                    {content.desc}
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
                                                    {content.text1}
                                                </Typography>
                                                <Box mt="auto" mb={2}>
                                                    <Typography variant="body1" component="p" color="textSecondary">
                                                        {content.text2}
                                                    </Typography>
                                                </Box>
                                                <Box textAlign="right">
                                                    <Link to="/articles" className={classes.link}>
                                                        <Button color="primary" endIcon={<ArrowRightAltIcon />}>
                                                            {content.button}
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
                                                <CardMedia className={classes.media} image={content.image1} />
                                            </Card>
                                        </Grid>
                                        <Grid item xs={12} md={4}>
                                            <Card elevation={4}>
                                                <CardMedia className={classes.media} image={content.image2} />
                                            </Card>
                                        </Grid>
                                        <Grid item xs={12} md={4}>
                                            <Card elevation={4}>
                                                <CardMedia className={classes.media} image={content.image3} />
                                            </Card>
                                        </Grid> 
                                        <Grid item xs={12} md={8}>
                                            <Card elevation={4}>
                                                <CardMedia className={classes.media} image={content.image4} />
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
