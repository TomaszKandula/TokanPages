import * as React from "react";
import { useSelector } from "react-redux";
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
import { ApplicationState } from "../../../Store/Configuration";
import { ContentArticleFeaturesState } from "../../../Store/States";
import { RenderCardMedia } from "../../../Shared/Components";
import { GET_ARTICLE_IMAGE_URL } from "../../../Api/Request";
import { FeaturesStyle } from "./featuresStyle";
import Validate from "validate.js";

const ActiveButton = (props: ContentArticleFeaturesState): JSX.Element => {
    const classes = FeaturesStyle();

    if (Validate.isEmpty(props.content?.action?.href)) {
        return (
            <Button endIcon={<ArrowRightAltIcon />} className={classes.button}>
                {props.isLoading ? <Skeleton variant="text" /> : props.content?.action?.text}
            </Button>
        );
    }

    return (
        <Link to={props.content?.action?.href} className={classes.link}>
            <Button endIcon={<ArrowRightAltIcon />} className={classes.button}>
                {props.isLoading ? <Skeleton variant="text" /> : props.content?.action?.text}
            </Button>
        </Link>
    );
};

export const FeaturesView = (): JSX.Element => {
    const classes = FeaturesStyle();
    const features = useSelector((state: ApplicationState) => state.contentArticleFeatures);

    return (
        <section className={classes.section}>
            <Container maxWidth="lg">
                <Box pt={8} pb={12}>
                    <Box textAlign="center" mb={6} data-aos="fade-down">
                        <Typography className={classes.title}>
                            {features.isLoading ? <Skeleton variant="text" /> : features.content?.title.toUpperCase()}
                        </Typography>
                    </Box>
                    <Container maxWidth="lg">
                        <div data-aos="fade-up">
                            <Grid container>
                                <Grid item xs={12} lg={6} className={classes.content}>
                                    <Card elevation={0} className={classes.card}>
                                        <CardContent className={classes.card_content}>
                                            <Box display="flex" flexDirection="column" height="100%" pt={2} px={2}>
                                                <Typography className={classes.text1}>
                                                    {features.isLoading ? (
                                                        <Skeleton variant="text" />
                                                    ) : (
                                                        features.content?.text1
                                                    )}
                                                </Typography>
                                                <Box mt="auto" mb={2}>
                                                    <Typography className={classes.text2}>
                                                        {features.isLoading ? (
                                                            <Skeleton variant="text" />
                                                        ) : (
                                                            features.content?.text2
                                                        )}
                                                    </Typography>
                                                </Box>
                                                <Box textAlign="right">
                                                    {features.isLoading ? (
                                                        <Skeleton variant="rect" width="100%" height="25px" />
                                                    ) : (
                                                        <ActiveButton {...features} />
                                                    )}
                                                </Box>
                                            </Box>
                                        </CardContent>
                                    </Card>
                                </Grid>
                                <Grid item xs={12} lg={6}>
                                    <Grid container spacing={2}>
                                        <Grid item xs={12} md={8}>
                                            <Card elevation={0} className={classes.card_image}>
                                                {features.isLoading ? (
                                                    <Skeleton variant="rect" height="128px" />
                                                ) : (
                                                    RenderCardMedia(
                                                        GET_ARTICLE_IMAGE_URL,
                                                        features.content?.image1,
                                                        classes.media
                                                    )
                                                )}
                                            </Card>
                                        </Grid>
                                        <Grid item xs={12} md={4}>
                                            <Card elevation={0} className={classes.card_image}>
                                                {features.isLoading ? (
                                                    <Skeleton variant="rect" height="128px" />
                                                ) : (
                                                    RenderCardMedia(
                                                        GET_ARTICLE_IMAGE_URL,
                                                        features.content?.image2,
                                                        classes.media
                                                    )
                                                )}
                                            </Card>
                                        </Grid>
                                        <Grid item xs={12} md={4}>
                                            <Card elevation={0} className={classes.card_image}>
                                                {features.isLoading ? (
                                                    <Skeleton variant="rect" height="128px" />
                                                ) : (
                                                    RenderCardMedia(
                                                        GET_ARTICLE_IMAGE_URL,
                                                        features.content?.image3,
                                                        classes.media
                                                    )
                                                )}
                                            </Card>
                                        </Grid>
                                        <Grid item xs={12} md={8}>
                                            <Card elevation={0} className={classes.card_image}>
                                                {features.isLoading ? (
                                                    <Skeleton variant="rect" height="128px" />
                                                ) : (
                                                    RenderCardMedia(
                                                        GET_ARTICLE_IMAGE_URL,
                                                        features.content?.image4,
                                                        classes.media
                                                    )
                                                )}
                                            </Card>
                                        </Grid>
                                    </Grid>
                                </Grid>
                            </Grid>
                        </div>
                    </Container>
                </Box>
            </Container>
        </section>
    );
};
