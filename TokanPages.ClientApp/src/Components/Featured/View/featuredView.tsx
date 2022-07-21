import * as React from "react";
import Box from "@material-ui/core/Box";
import Container from "@material-ui/core/Container";
import Typography from "@material-ui/core/Typography";
import Grid from "@material-ui/core/Grid";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import CardActionArea from "@material-ui/core/CardActionArea";
import Skeleton from "@material-ui/lab/Skeleton";
import { IGetFeaturedContent } from "../../../Redux/States/Content/getFeaturedContentState";
import { renderCardMedia } from "../../../Shared/Components/CustomCardMedia/customCardMedia";
import { FEATURED_IMAGE_PATH } from "../../../Shared/constants";
import { FeaturedStyle } from "./featuredStyle";

export const FeaturedView = (props: IGetFeaturedContent): JSX.Element => 
{
    const classes = FeaturedStyle();
    return (
        <section className={classes.section}>
            <Container maxWidth="lg">
                <Box pt={8} pb={5} textAlign="center">
                    <Typography className={classes.caption_text} data-aos="fade-down">
                        {props.isLoading ? <Skeleton variant="text" /> : props.content?.caption?.toUpperCase()}
                    </Typography>
                </Box>
                <Box pb={15} textAlign="center">
                    <Grid container spacing={6}>
                        <Grid item xs={12} md={4} data-aos="fade-up" data-aos-delay="350">
                            <Card elevation={0} className={classes.card}>
                                <CardActionArea href={props.content?.link1} target="_blank">
                                    {props.isLoading 
                                        ? <Skeleton variant="rect" height="256px" /> 
                                        : renderCardMedia(FEATURED_IMAGE_PATH, props.content?.image1, classes.card_media)}
                                    <CardContent className={classes.card_content}>
                                        <Typography className={classes.card_title}>
                                            {props.isLoading ? <Skeleton variant="text" width="250px" /> : props.content?.title1}
                                        </Typography>
                                        <Typography className={classes.card_subtitle}>
                                            {props.isLoading ? <Skeleton variant="text" width="250px" /> : props.content?.subtitle1} 
                                        </Typography>
                                    </CardContent>
                                </CardActionArea>
                            </Card>
                        </Grid>
                        <Grid item xs={12} md={4} data-aos="fade-up" data-aos-delay="150">
                            <Card elevation={0} className={classes.card}>
                                <CardActionArea href={props.content?.link2} target="_blank">
                                    {props.isLoading 
                                        ? <Skeleton variant="rect" height="256px" /> 
                                        : renderCardMedia(FEATURED_IMAGE_PATH, props.content?.image2, classes.card_media)}
                                    <CardContent className={classes.card_content}>
                                        <Typography className={classes.card_title}>
                                            {props.isLoading ? <Skeleton variant="text" width="250px" /> : props.content?.title2}
                                        </Typography>
                                        <Typography className={classes.card_subtitle}>
                                            {props.isLoading ? <Skeleton variant="text" width="250px" /> : props.content?.subtitle2} 
                                        </Typography>
                                    </CardContent>
                                </CardActionArea>
                            </Card>
                        </Grid>
                        <Grid item xs={12} md={4} data-aos="fade-up" data-aos-delay="550">
                            <Card elevation={0} className={classes.card}>
                                <CardActionArea href={props.content?.link3} target="_blank">
                                    {props.isLoading 
                                        ? <Skeleton variant="rect" height="256px" /> 
                                        : renderCardMedia(FEATURED_IMAGE_PATH, props.content?.image3, classes.card_media)}
                                    <CardContent className={classes.card_content}>
                                        <Typography className={classes.card_title}>
                                            {props.isLoading ? <Skeleton variant="text" width="250px" /> : props.content?.title3}
                                        </Typography>
                                        <Typography className={classes.card_subtitle}>
                                            {props.isLoading ? <Skeleton variant="text" width="250px" /> : props.content?.subtitle3} 
                                        </Typography>
                                    </CardContent>
                                </CardActionArea>
                            </Card>
                        </Grid>
                    </Grid>
                </Box>
            </Container>
        </section>
    );
}
