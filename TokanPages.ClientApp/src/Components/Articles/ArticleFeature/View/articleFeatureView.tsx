import * as React from "react";
import { useSelector } from "react-redux";
import { Link } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Grid from "@material-ui/core/Grid";
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
import Validate from "validate.js";

interface ArticleFeatureViewProps {
    background?: React.CSSProperties;
}

interface ArticleFeaturesContentProps extends ArticleFeaturesContentDto {
    isLoading: boolean;
}

const ActiveButton = (props: ArticleFeaturesContentProps): React.ReactElement => {
    if (Validate.isEmpty(props?.action?.href)) {
        return (
            <Button endIcon={<ArrowRightAltIcon />} className="button">
                {props?.isLoading ? <Skeleton variant="text" /> : props?.action?.text}
            </Button>
        );
    }

    return (
        <Link to={props?.action?.href ?? ""} className="link">
            <Button endIcon={<ArrowRightAltIcon />} className="button">
                {props?.isLoading ? <Skeleton variant="text" /> : props?.action?.text}
            </Button>
        </Link>
    );
};

export const ArticleFeatureView = (props: ArticleFeatureViewProps): React.ReactElement => {
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const features = data?.components?.articleFeatures;
    return (
        <section className="section" style={props.background}>
            <Container className="container-super-wide">
                <div style={{ paddingTop: 64, paddingBottom: 96 }}>
                    <div data-aos="fade-down" style={{ textAlign: "center", marginBottom: 48 }}>
                        <Typography className="article-features-title">
                            {data?.isLoading ? <Skeleton variant="text" /> : features?.title.toUpperCase()}
                        </Typography>
                    </div>
                    <div data-aos="fade-up">
                        <Grid container>
                            <Grid item xs={12} lg={6} className="article-features-content">
                                <Card elevation={0} className="article-features-card">
                                    <CardContent className="article-features-card-content">
                                        <div
                                            style={{
                                                display: "flex",
                                                flexDirection: "column",
                                                paddingTop: 16,
                                                paddingLeft: 16,
                                                paddingRight: 16,
                                            }}
                                        >
                                            <Typography className="article-features-text1">
                                                {data?.isLoading ? <Skeleton variant="text" /> : features?.text1}
                                            </Typography>
                                            <div style={{ marginTop: 16, marginBottom: 40 }}>
                                                <Typography className="article-features-text2">
                                                    {data?.isLoading ? <Skeleton variant="text" /> : features?.text2}
                                                </Typography>
                                            </div>
                                            <div style={{ paddingTop: 0, textAlign: "right" }}>
                                                {data?.isLoading ? (
                                                    <Skeleton variant="rect" width="100%" height="25px" />
                                                ) : (
                                                    <ActiveButton isLoading={data?.isLoading} {...features} />
                                                )}
                                            </div>
                                        </div>
                                    </CardContent>
                                </Card>
                            </Grid>
                            <Grid item xs={12} lg={6}>
                                <Grid container spacing={2}>
                                    <Grid item xs={12} md={8}>
                                        <Card elevation={0} className="card-image">
                                            {data?.isLoading ? (
                                                <Skeleton variant="rect" height="128px" />
                                            ) : (
                                                RenderCardMedia(
                                                    GET_ARTICLE_IMAGE_URL,
                                                    features?.image1,
                                                    "article-features-media"
                                                )
                                            )}
                                        </Card>
                                    </Grid>
                                    <Grid item xs={12} md={4}>
                                        <Card elevation={0} className="card-image">
                                            {data?.isLoading ? (
                                                <Skeleton variant="rect" height="128px" />
                                            ) : (
                                                RenderCardMedia(
                                                    GET_ARTICLE_IMAGE_URL,
                                                    features?.image2,
                                                    "article-features-media"
                                                )
                                            )}
                                        </Card>
                                    </Grid>
                                    <Grid item xs={12} md={4}>
                                        <Card elevation={0} className="card-image">
                                            {data?.isLoading ? (
                                                <Skeleton variant="rect" height="128px" />
                                            ) : (
                                                RenderCardMedia(
                                                    GET_ARTICLE_IMAGE_URL,
                                                    features?.image3,
                                                    "article-features-media"
                                                )
                                            )}
                                        </Card>
                                    </Grid>
                                    <Grid item xs={12} md={8}>
                                        <Card elevation={0} className="card-image">
                                            {data?.isLoading ? (
                                                <Skeleton variant="rect" height="128px" />
                                            ) : (
                                                RenderCardMedia(
                                                    GET_ARTICLE_IMAGE_URL,
                                                    features?.image4,
                                                    "article-features-media"
                                                )
                                            )}
                                        </Card>
                                    </Grid>
                                </Grid>
                            </Grid>
                        </Grid>
                    </div>
                </div>
            </Container>
        </section>
    );
};
