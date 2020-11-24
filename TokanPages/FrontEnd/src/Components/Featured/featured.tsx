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
import { IMG_FEAT1, IMG_FEAT2, IMG_FEAT3 } from "../../Shared/constants";

export default function Featured() 
{
  
    const classes = useStyles();

    return (
        <section className={classes.section}>
            <Container maxWidth="lg">
                <div data-aos="fade-up">
                    <Box pt={8} pb={10}>
                        <Container maxWidth="sm">
                            <Box textAlign="center" mb={5}>
                                <Typography variant="h4" component="h2" gutterBottom={true}>Featured</Typography>
                                <Typography variant="subtitle1" color="textSecondary">
                                    My pick of three articles that I wrote and published elsewhere or articles where I'm featured. So far I can be find on Medium.com, LinkedIn and JustJoinIT.
                                </Typography>
                            </Box>
                        </Container>
                        <Container maxWidth="md">
                            <Box textAlign="center">
                                <Grid container spacing={4}>
                                    <Grid item xs={12} md={4}>
                                        <Card elevation={3}>
                                            <CardActionArea href="http://geek.justjoin.it/wysokie-widelki-clickbait-devdebata" target="_blank">
                                                <CardMedia className={classes.media} image={IMG_FEAT1} />
                                                <CardHeader title="Stored Procedures" subheader="I explain why I do not need them that much..." titleTypographyProps={{gutterBottom: true}} />
                                            </CardActionArea>
                                        </Card>
                                    </Grid>
                                    <Grid item xs={12} md={4}>
                                        <Card elevation={3}>
                                            <CardActionArea href="https://medium.com/@tomasz.kandula/sql-injection-1bde8bb76ebc" target="_blank">
                                                <CardMedia className={classes.media} image={IMG_FEAT2} />
                                                <CardHeader title="SQL Injection" subheader="This article will explore the issue in greater detail..." titleTypographyProps={{gutterBottom: true}} />
                                            </CardActionArea>
                                        </Card>
                                    </Grid>
                                    <Grid item xs={12} md={4}>
                                        <Card elevation={3}>
                                            <CardActionArea href="https://medium.com/@tomasz.kandula/i-said-goodbye-to-stored-procedures-539d56350486" target="_blank">
                                                <CardMedia className={classes.media} image={IMG_FEAT3} />
                                                <CardHeader title="Just Geek IT" subheader="Widełki płacowe stanowią standard w branży..." titleTypographyProps={{gutterBottom: true}}/>
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
