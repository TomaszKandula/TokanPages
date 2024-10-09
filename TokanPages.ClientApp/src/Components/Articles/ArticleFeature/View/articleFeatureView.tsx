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
import { ArticleFeaturesContentDto } from "../../../../Api/Models";
import { ApplicationState } from "../../../../Store/Configuration";
import { RenderCardMedia } from "../../../../Shared/Components";
import { GET_ARTICLE_IMAGE_URL } from "../../../../Api/Request";
import { ArticleFeatureStyle } from "./articleFeatureStyle";
import Validate from "validate.js";

interface ArticleFeatureViewProps {
    background?: React.CSSProperties;
}

interface ArticleFeaturesContentProps extends ArticleFeaturesContentDto {
    isLoading: boolean;
}

const ActiveButton = (props: ArticleFeaturesContentProps): React.ReactElement => {
    const classes = ArticleFeatureStyle();

    if (Validate.isEmpty(props?.action?.href)) {
        return (
            <Button endIcon={<ArrowRightAltIcon />} className={classes.button}>
                {props?.isLoading ? <Skeleton variant="text" /> : props?.action?.text}
            </Button>
        );
    }

    return (
        <Link to={props?.action?.href ?? ""} className={classes.link}>
            <Button endIcon={<ArrowRightAltIcon />} className={classes.button}>
                {props?.isLoading ? <Skeleton variant="text" /> : props?.action?.text}
            </Button>
        </Link>
    );
};

export const ArticleFeatureView = (props: ArticleFeatureViewProps): React.ReactElement => {
    const classes = ArticleFeatureStyle();
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const features = data?.components?.articleFeatures;

    return (
        <section className={classes.section} style={props.background}>
            <Container maxWidth="lg">
                <Box pt={8} pb={12}>
                    <Box textAlign="center" mb={6} data-aos="fade-down">
                        <Typography className={classes.title}>
                            {data?.isLoading ? <Skeleton variant="text" /> : features?.title.toUpperCase()}
                        </Typography>
                    </Box>
                    <div data-aos="fade-up">
                        <Grid container>
                            <Grid item xs={12} lg={6} className={classes.content}>
                                <Card elevation={0} className={classes.card}>
                                    <CardContent className={classes.card_content}>
                                        <Box display="flex" flexDirection="column" pt={2} px={2}>
                                            <Typography className={classes.text1}>
                                                {data?.isLoading ? <Skeleton variant="text" /> : features?.text1}
                                            </Typography>
                                            <Box mt={2} mb={5}>
                                                <Typography className={classes.text2}>
                                                    {data?.isLoading ? <Skeleton variant="text" /> : features?.text2}
                                                </Typography>
                                            </Box>
                                            <Box pt={0} textAlign="right">
                                                {data?.isLoading ? (
                                                    <Skeleton variant="rect" width="100%" height="25px" />
                                                ) : (
                                                    <ActiveButton isLoading={data?.isLoading} {...features} />
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
                                            {data?.isLoading ? (
                                                <Skeleton variant="rect" height="128px" />
                                            ) : (
                                                RenderCardMedia(GET_ARTICLE_IMAGE_URL, features?.image1, classes.media)
                                            )}
                                        </Card>
                                    </Grid>
                                    <Grid item xs={12} md={4}>
                                        <Card elevation={0} className={classes.card_image}>
                                            {data?.isLoading ? (
                                                <Skeleton variant="rect" height="128px" />
                                            ) : (
                                                RenderCardMedia(GET_ARTICLE_IMAGE_URL, features?.image2, classes.media)
                                            )}
                                        </Card>
                                    </Grid>
                                    <Grid item xs={12} md={4}>
                                        <Card elevation={0} className={classes.card_image}>
                                            {data?.isLoading ? (
                                                <Skeleton variant="rect" height="128px" />
                                            ) : (
                                                RenderCardMedia(GET_ARTICLE_IMAGE_URL, features?.image3, classes.media)
                                            )}
                                        </Card>
                                    </Grid>
                                    <Grid item xs={12} md={8}>
                                        <Card elevation={0} className={classes.card_image}>
                                            {data?.isLoading ? (
                                                <Skeleton variant="rect" height="128px" />
                                            ) : (
                                                RenderCardMedia(GET_ARTICLE_IMAGE_URL, features?.image4, classes.media)
                                            )}
                                        </Card>
                                    </Grid>
                                </Grid>
                            </Grid>
                        </Grid>
                    </div>
                </Box>
            </Container>
        </section>
    );
};
