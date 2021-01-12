import * as React from "react";
import Box from "@material-ui/core/Box";
import Container from "@material-ui/core/Container";
import Typography from "@material-ui/core/Typography";
import Grid from "@material-ui/core/Grid";
import Card from "@material-ui/core/Card";
import CardActionArea from "@material-ui/core/CardActionArea";
import CardMedia from "@material-ui/core/CardMedia";
import CardHeader from "@material-ui/core/CardHeader";
import useStyles from "./styleFeatured";

export default function Featured() 
{
  
    const classes = useStyles();
    const content = 
    {
        caption: "Featured",
        text: "My pick of three articles that I wrote and published elsewhere or articles where I'm featured. So far I can be find on Medium.com, LinkedIn and JustJoinIT.",
        title1: "Stored Procedures",
        subtitle1: "I explain why I do not need them that much...",
        link1: "http://geek.justjoin.it/wysokie-widelki-clickbait-devdebata",
        image1: "https://maindbstorage.blob.core.windows.net/tokanpages/images/section_featured/article1.jpg",
        title2: "SQL Injection",
        subtitle2: "This article will explore the issue in greater detail...",
        link2: "https://medium.com/@tomasz.kandula/sql-injection-1bde8bb76ebc",
        image2: "https://maindbstorage.blob.core.windows.net/tokanpages/images/section_featured/article2.jpg",
        title3: "Just Geek IT",
        subtitle3: "Widełki płacowe stanowią standard w branży...",
        link3: "https://medium.com/@tomasz.kandula/i-said-goodbye-to-stored-procedures-539d56350486",
        image3: "https://maindbstorage.blob.core.windows.net/tokanpages/images/section_featured/article3.jpg",
    };

    return (
        <section className={classes.section}>
            <Container maxWidth="lg">
                <div data-aos="fade-up">
                    <Box pt={8} pb={10}>
                        <Container maxWidth="sm">
                            <Box textAlign="center" mb={5}>
                                <Typography variant="h4" component="h2" gutterBottom={true}>
                                    {content.caption}
                                </Typography>
                                <Typography variant="subtitle1" color="textSecondary">
                                    {content.text}
                                </Typography>
                            </Box>
                        </Container>
                        <Container maxWidth="md">
                            <Box textAlign="center">
                                <Grid container spacing={4}>
                                    <Grid item xs={12} md={4}>
                                        <Card elevation={3}>
                                            <CardActionArea href={content.link1} target="_blank">
                                                <CardMedia className={classes.media} image={content.image1} />
                                                <CardHeader title={content.title1} subheader={content.subtitle1} titleTypographyProps={{gutterBottom: true}} />
                                            </CardActionArea>
                                        </Card>
                                    </Grid>
                                    <Grid item xs={12} md={4}>
                                        <Card elevation={3}>
                                            <CardActionArea href={content.link2} target="_blank">
                                                <CardMedia className={classes.media} image={content.image2} />
                                                <CardHeader title={content.title2} subheader={content.subtitle2} titleTypographyProps={{gutterBottom: true}} />
                                            </CardActionArea>
                                        </Card>
                                    </Grid>
                                    <Grid item xs={12} md={4}>
                                        <Card elevation={3}>
                                            <CardActionArea href={content.link3} target="_blank">
                                                <CardMedia className={classes.media} image={content.image3} />
                                                <CardHeader title={content.title3} subheader={content.subtitle3} titleTypographyProps={{gutterBottom: true}}/>
                                            </CardActionArea>
                                        </Card>
                                    </Grid>
                                </Grid>
                            </Box>
                        </Container>
                    </Box>
                </div>
            </Container>
        </section>
    );

}
