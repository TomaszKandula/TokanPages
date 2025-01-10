import * as React from "react";
import { useSelector } from "react-redux";
import Container from "@material-ui/core/Container";
import Typography from "@material-ui/core/Typography";
import Grid from "@material-ui/core/Grid";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import CardActionArea from "@material-ui/core/CardActionArea";
import Skeleton from "@material-ui/lab/Skeleton";
import { ApplicationState } from "../../../Store/Configuration";
import { Animated, RenderCardMedia } from "../../../Shared/Components";
import { GET_FEATURED_IMAGE_URL } from "../../../Api/Request";

interface FeaturedViewProps {
    background?: React.CSSProperties;
}

export const FeaturedView = (props: FeaturedViewProps): React.ReactElement => {
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const featured = data?.components?.featured;
    return (
        <section className="section-grey" style={props.background}>
            <Container maxWidth="lg">
                <div style={{ textAlign: "center", paddingTop: 64, paddingBottom: 40 }}>
                    <Animated dataAos="fade-down">
                        <Typography className="featured-caption-text">
                            {data?.isLoading ? <Skeleton variant="text" /> : featured?.caption?.toUpperCase()}
                        </Typography>
                    </Animated>
                </div>

                <div style={{ textAlign: "center", paddingBottom: 120 }}>
                    <Grid container spacing={6}>
                        <Grid item xs={12} md={4}>
                            <Animated dataAos="fade-up" dataAosDelay={350}>
                                <Card elevation={0} className="card">
                                    <CardActionArea href={featured?.link1} target="_blank" rel="noopener">
                                        {data?.isLoading ? (
                                            <Skeleton variant="rect" height="256px" />
                                        ) : (
                                            RenderCardMedia(
                                                GET_FEATURED_IMAGE_URL,
                                                featured?.image1,
                                                "featured-card-media"
                                            )
                                        )}
                                        <CardContent className="featured-card-content">
                                            <Typography className="featured-card-title">
                                                {data?.isLoading ? (
                                                    <Skeleton variant="text" width="250px" />
                                                ) : (
                                                    featured?.title1
                                                )}
                                            </Typography>
                                            <Typography className="featured-card-subtitle">
                                                {data?.isLoading ? (
                                                    <Skeleton variant="text" width="250px" />
                                                ) : (
                                                    featured?.subtitle1
                                                )}
                                            </Typography>
                                        </CardContent>
                                    </CardActionArea>
                                </Card>
                            </Animated>
                        </Grid>

                        <Grid item xs={12} md={4}>
                            <Animated dataAos="fade-up" dataAosDelay={150}>
                                <Card elevation={0} className="card">
                                    <CardActionArea href={featured?.link2} target="_blank" rel="noopener">
                                        {data?.isLoading ? (
                                            <Skeleton variant="rect" height="256px" />
                                        ) : (
                                            RenderCardMedia(
                                                GET_FEATURED_IMAGE_URL,
                                                featured?.image2,
                                                "featured-card-media"
                                            )
                                        )}
                                        <CardContent className="featured-card-content">
                                            <Typography className="featured-card-title">
                                                {data?.isLoading ? (
                                                    <Skeleton variant="text" width="250px" />
                                                ) : (
                                                    featured?.title2
                                                )}
                                            </Typography>
                                            <Typography className="featured-card-subtitle">
                                                {data?.isLoading ? (
                                                    <Skeleton variant="text" width="250px" />
                                                ) : (
                                                    featured?.subtitle2
                                                )}
                                            </Typography>
                                        </CardContent>
                                    </CardActionArea>
                                </Card>
                            </Animated>
                        </Grid>

                        <Grid item xs={12} md={4}>
                            <Animated dataAos="fade-up" dataAosDelay={550}>
                                <Card elevation={0} className="card">
                                    <CardActionArea href={featured?.link3} target="_blank" rel="noopener">
                                        {data?.isLoading ? (
                                            <Skeleton variant="rect" height="256px" />
                                        ) : (
                                            RenderCardMedia(
                                                GET_FEATURED_IMAGE_URL,
                                                featured?.image3,
                                                "featured-card-media"
                                            )
                                        )}
                                        <CardContent className="featured-card-content">
                                            <Typography className="featured-card-title">
                                                {data?.isLoading ? (
                                                    <Skeleton variant="text" width="250px" />
                                                ) : (
                                                    featured?.title3
                                                )}
                                            </Typography>
                                            <Typography className="featured-card-subtitle">
                                                {data?.isLoading ? (
                                                    <Skeleton variant="text" width="250px" />
                                                ) : (
                                                    featured?.subtitle3
                                                )}
                                            </Typography>
                                        </CardContent>
                                    </CardActionArea>
                                </Card>
                            </Animated>
                        </Grid>
                    </Grid>
                </div>
            </Container>
        </section>
    );
};
