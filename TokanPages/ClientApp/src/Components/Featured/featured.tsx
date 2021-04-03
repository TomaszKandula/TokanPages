import * as React from "react";
import Box from "@material-ui/core/Box";
import Container from "@material-ui/core/Container";
import Typography from "@material-ui/core/Typography";
import Grid from "@material-ui/core/Grid";
import Card from "@material-ui/core/Card";
import CardActionArea from "@material-ui/core/CardActionArea";
import CardHeader from "@material-ui/core/CardHeader";
import Skeleton from "@material-ui/lab/Skeleton";
import { renderCardMedia } from "../../Shared/Components/renderCardMedia";
import { IFeaturedContentDto } from "../../Api/Models";
import useStyles from "./styleFeatured";

export default function Featured(props: { featured: IFeaturedContentDto, isLoading: boolean }) 
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
                                    {props.isLoading ? <Skeleton variant="text" /> : props.featured.content.caption}
                                </Typography>
                                <Typography variant="subtitle1" color="textSecondary">
                                    {props.isLoading ? <Skeleton variant="text" /> : props.featured.content.text}
                                </Typography>
                            </Box>
                        </Container>
                        <Container maxWidth="md">
                            <Box textAlign="center">
                                <Grid container spacing={4}>
                                    <Grid item xs={12} md={4}>
                                        <Card elevation={3}>
                                            <CardActionArea href={props.featured.content.link1} target="_blank">
                                                {props.isLoading 
                                                    ? <Skeleton variant="rect" height="256px" /> 
                                                    : renderCardMedia(props.featured.content.image1, classes.media)}
                                                <CardHeader 
                                                    title={props.featured.content.title1} 
                                                    subheader={props.featured.content.subtitle1} 
                                                    titleTypographyProps={{gutterBottom: true}} 
                                                />
                                            </CardActionArea>
                                        </Card>
                                    </Grid>
                                    <Grid item xs={12} md={4}>
                                        <Card elevation={3}>
                                            <CardActionArea href={props.featured.content.link2} target="_blank">
                                                {props.isLoading 
                                                    ? <Skeleton variant="rect" height="256px" /> 
                                                    : renderCardMedia(props.featured.content.image2, classes.media)}
                                                <CardHeader 
                                                    title={props.featured.content.title2} 
                                                    subheader={props.featured.content.subtitle2} 
                                                    titleTypographyProps={{gutterBottom: true}} 
                                                />
                                            </CardActionArea>
                                        </Card>
                                    </Grid>
                                    <Grid item xs={12} md={4}>
                                        <Card elevation={3}>
                                            <CardActionArea href={props.featured.content.link3} target="_blank">
                                                {props.isLoading 
                                                    ? <Skeleton variant="rect" height="256px" /> 
                                                    : renderCardMedia(props.featured.content.image3, classes.media)}
                                                <CardHeader 
                                                    title={props.featured.content.title3} 
                                                    subheader={props.featured.content.subtitle3} 
                                                    titleTypographyProps={{gutterBottom: true}}
                                                />
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
