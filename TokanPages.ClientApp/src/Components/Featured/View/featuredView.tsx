import * as React from "react";
import { useSelector } from "react-redux";
import Box from "@material-ui/core/Box";
import Container from "@material-ui/core/Container";
import Typography from "@material-ui/core/Typography";
import Grid from "@material-ui/core/Grid";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import CardActionArea from "@material-ui/core/CardActionArea";
import Skeleton from "@material-ui/lab/Skeleton";
import { ApplicationState } from "../../../Store/Configuration";
import { RenderCardMedia } from "../../../Shared/Components";
import { GET_FEATURED_IMAGE_URL } from "../../../Api/Request";
import { FeaturedStyle } from "./featuredStyle";

interface FeaturedViewProps {
    background?: React.CSSProperties;
}

export const FeaturedView = (props: FeaturedViewProps): React.ReactElement => {
    const classes = FeaturedStyle();
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const featured = data?.components?.featured;
    return (
        <section className={classes.section} style={props.background}>
            <Container maxWidth="lg">
                <Box pt={8} pb={5} textAlign="center">
                    <Typography className={classes.caption_text} data-aos="fade-down">
                        {data?.isLoading ? <Skeleton variant="text" /> : featured?.caption?.toUpperCase()}
                    </Typography>
                </Box>
                <Box pb={15} textAlign="center">
                    <Grid container spacing={6}>
                        <Grid item xs={12} md={4} data-aos="fade-up" data-aos-delay="350">
                            <Card elevation={0} className={classes.card}>
                                <CardActionArea href={featured?.link1} target="_blank" rel="noopener">
                                    {data?.isLoading ? (
                                        <Skeleton variant="rect" height="256px" />
                                    ) : (
                                        RenderCardMedia(GET_FEATURED_IMAGE_URL, featured?.image1, classes.card_media)
                                    )}
                                    <CardContent className={classes.card_content}>
                                        <Typography className={classes.card_title}>
                                            {data?.isLoading ? (
                                                <Skeleton variant="text" width="250px" />
                                            ) : (
                                                featured?.title1
                                            )}
                                        </Typography>
                                        <Typography className={classes.card_subtitle}>
                                            {data?.isLoading ? (
                                                <Skeleton variant="text" width="250px" />
                                            ) : (
                                                featured?.subtitle1
                                            )}
                                        </Typography>
                                    </CardContent>
                                </CardActionArea>
                            </Card>
                        </Grid>
                        <Grid item xs={12} md={4} data-aos="fade-up" data-aos-delay="150">
                            <Card elevation={0} className={classes.card}>
                                <CardActionArea href={featured?.link2} target="_blank" rel="noopener">
                                    {data?.isLoading ? (
                                        <Skeleton variant="rect" height="256px" />
                                    ) : (
                                        RenderCardMedia(GET_FEATURED_IMAGE_URL, featured?.image2, classes.card_media)
                                    )}
                                    <CardContent className={classes.card_content}>
                                        <Typography className={classes.card_title}>
                                            {data?.isLoading ? (
                                                <Skeleton variant="text" width="250px" />
                                            ) : (
                                                featured?.title2
                                            )}
                                        </Typography>
                                        <Typography className={classes.card_subtitle}>
                                            {data?.isLoading ? (
                                                <Skeleton variant="text" width="250px" />
                                            ) : (
                                                featured?.subtitle2
                                            )}
                                        </Typography>
                                    </CardContent>
                                </CardActionArea>
                            </Card>
                        </Grid>
                        <Grid item xs={12} md={4} data-aos="fade-up" data-aos-delay="550">
                            <Card elevation={0} className={classes.card}>
                                <CardActionArea href={featured?.link3} target="_blank" rel="noopener">
                                    {data?.isLoading ? (
                                        <Skeleton variant="rect" height="256px" />
                                    ) : (
                                        RenderCardMedia(GET_FEATURED_IMAGE_URL, featured?.image3, classes.card_media)
                                    )}
                                    <CardContent className={classes.card_content}>
                                        <Typography className={classes.card_title}>
                                            {data?.isLoading ? (
                                                <Skeleton variant="text" width="250px" />
                                            ) : (
                                                featured?.title3
                                            )}
                                        </Typography>
                                        <Typography className={classes.card_subtitle}>
                                            {data?.isLoading ? (
                                                <Skeleton variant="text" width="250px" />
                                            ) : (
                                                featured?.subtitle3
                                            )}
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
};
