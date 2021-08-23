import * as React from "react";
import Box from "@material-ui/core/Box";
import Container from "@material-ui/core/Container";
import Typography from "@material-ui/core/Typography";
import Grid from "@material-ui/core/Grid";
import Card from "@material-ui/core/Card";
import CardActionArea from "@material-ui/core/CardActionArea";
import CardHeader from "@material-ui/core/CardHeader";
import Skeleton from "@material-ui/lab/Skeleton";
import { IGetFeaturedContent } from "../../Redux/States/Content/getFeaturedContentState";
import { renderCardMedia } from "../../Shared/Components/CustomCardMedia/customCardMedia";
import featuredStyle from "./featuredStyle";

const FeaturedView = (props: IGetFeaturedContent): JSX.Element => 
{
    const classes = featuredStyle();
    return (
        <section className={classes.section}>
            <Container maxWidth="lg">
                <div data-aos="fade-up">
                    <Box pt={8} pb={10}>
                        <Container maxWidth="sm">
                            <Box textAlign="center" mb={5}>
                                <Typography variant="h4" component="h2" gutterBottom={true}>
                                    {props.isLoading ? <Skeleton variant="text" /> : props.content?.caption}
                                </Typography>
                                <Typography variant="subtitle1" color="textSecondary">
                                    {props.isLoading ? <Skeleton variant="text" /> : props.content?.text}
                                </Typography>
                            </Box>
                        </Container>
                        <Container maxWidth="md">
                            <Box textAlign="center">
                                <Grid container spacing={4}>
                                    <Grid item xs={12} md={4}>
                                        <Card elevation={3}>
                                            <CardActionArea href={props.content?.link1} target="_blank">
                                                {props.isLoading 
                                                    ? <Skeleton variant="rect" height="256px" /> 
                                                    : renderCardMedia(props.content?.image1, classes.media)}
                                                <CardHeader 
                                                    title={props.content?.title1} 
                                                    subheader={props.content?.subtitle1} 
                                                    titleTypographyProps={{gutterBottom: true}} 
                                                />
                                            </CardActionArea>
                                        </Card>
                                    </Grid>
                                    <Grid item xs={12} md={4}>
                                        <Card elevation={3}>
                                            <CardActionArea href={props.content?.link2} target="_blank">
                                                {props.isLoading 
                                                    ? <Skeleton variant="rect" height="256px" /> 
                                                    : renderCardMedia(props.content?.image2, classes.media)}
                                                <CardHeader 
                                                    title={props.content?.title2} 
                                                    subheader={props.content?.subtitle2} 
                                                    titleTypographyProps={{gutterBottom: true}} 
                                                />
                                            </CardActionArea>
                                        </Card>
                                    </Grid>
                                    <Grid item xs={12} md={4}>
                                        <Card elevation={3}>
                                            <CardActionArea href={props.content?.link3} target="_blank">
                                                {props.isLoading 
                                                    ? <Skeleton variant="rect" height="256px" /> 
                                                    : renderCardMedia(props.content?.image3, classes.media)}
                                                <CardHeader 
                                                    title={props.content?.title3} 
                                                    subheader={props.content?.subtitle3} 
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

export default FeaturedView;
