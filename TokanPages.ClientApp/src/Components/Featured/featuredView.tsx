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
import { FEATURED_IMAGE_PATH } from "../../Shared/constants";
import featuredStyle from "./featuredStyle";

const FeaturedView = (props: IGetFeaturedContent): JSX.Element => 
{
    const classes = featuredStyle();
    return (
        <section className={classes.section}>
            <Container maxWidth="lg">
                <Box pt={8} pb={5} textAlign="center">
                    <Typography gutterBottom={true} className={classes.caption_text} data-aos="fade-down">
                        {props.isLoading ? <Skeleton variant="text" /> : props.content?.caption?.toUpperCase()}
                    </Typography>
                </Box>
                <Box pb={15} textAlign="center">
                    <Grid container spacing={6}>
                        <Grid item xs={12} md={4} data-aos="fade-left">
                            <Card elevation={0} className={classes.card}>
                                <CardActionArea href={props.content?.link1} target="_blank">
                                    {props.isLoading 
                                        ? <Skeleton variant="rect" height="256px" /> 
                                        : renderCardMedia(FEATURED_IMAGE_PATH, props.content?.image1, classes.media)}
                                    <CardHeader 
                                        title={props.content?.title1} 
                                        subheader={props.content?.subtitle1} 
                                        titleTypographyProps={{gutterBottom: true}} className={classes.card_header} />
                                </CardActionArea>
                            </Card>
                        </Grid>
                        <Grid item xs={12} md={4} data-aos="fade-up">
                            <Card elevation={0} className={classes.card}>
                                <CardActionArea href={props.content?.link2} target="_blank">
                                    {props.isLoading 
                                        ? <Skeleton variant="rect" height="256px" /> 
                                        : renderCardMedia(FEATURED_IMAGE_PATH, props.content?.image2, classes.media)}
                                    <CardHeader 
                                        title={props.content?.title2} 
                                        subheader={props.content?.subtitle2} 
                                        titleTypographyProps={{gutterBottom: true}} className={classes.card_header} />
                                </CardActionArea>
                            </Card>
                        </Grid>
                        <Grid item xs={12} md={4} data-aos="fade-right">
                            <Card elevation={0} className={classes.card}>
                                <CardActionArea href={props.content?.link3} target="_blank">
                                    {props.isLoading 
                                        ? <Skeleton variant="rect" height="256px" /> 
                                        : renderCardMedia(FEATURED_IMAGE_PATH, props.content?.image3, classes.media)}
                                    <CardHeader 
                                        title={props.content?.title3} 
                                        subheader={props.content?.subtitle3} 
                                        titleTypographyProps={{gutterBottom: true}} className={classes.card_header} />
                                    </CardActionArea>
                            </Card>
                        </Grid>
                    </Grid>
                </Box>
            </Container>
        </section>
    );
}

export default FeaturedView;
