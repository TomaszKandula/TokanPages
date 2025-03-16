import * as React from "react";
import { useSelector } from "react-redux";
import { Link } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Grid from "@material-ui/core/Grid";
import Typography from "@material-ui/core/Typography";
import Button from "@material-ui/core/Button";
import ArrowRightAltIcon from "@material-ui/icons/ArrowRightAlt";
import Card from "@material-ui/core/Card";
import Skeleton from "@material-ui/lab/Skeleton";
import { CardMedia } from "@material-ui/core";
import { GET_ARTICLE_IMAGE_URL } from "../../../../Api/Request";
import { ArticleFeaturesContentDto } from "../../../../Api/Models";
import { ApplicationState } from "../../../../Store/Configuration";
import { Animated } from "../../../../Shared/Components";
import { GetImageUrl } from "../../../../Shared/Services/Utilities";
import Validate from "validate.js";

interface ArticleFeatureViewProps {
    background?: string;
}

interface ArticleFeaturesContentProps extends ArticleFeaturesContentDto {
    isLoading: boolean;
}

const ActiveButton = (props: ArticleFeaturesContentProps): React.ReactElement => {
    if (Validate.isEmpty(props?.action?.href)) {
        return (
            <Button variant="contained" endIcon={<ArrowRightAltIcon />} className="button">
                {props?.isLoading ? <Skeleton variant="text" /> : props?.action?.text}
            </Button>
        );
    }

    return (
        <Link to={props?.action?.href ?? ""} className="link" rel="noopener nofollow">
            <Button variant="contained" endIcon={<ArrowRightAltIcon />} className="button">
                {props?.isLoading ? <Skeleton variant="text" /> : props?.action?.text}
            </Button>
        </Link>
    );
};

export const ArticleFeatureView = (props: ArticleFeatureViewProps): React.ReactElement => {
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const features = data?.components?.sectionArticle;
    return (
        <section className={`section ${props.background ?? ""}`}>
            <Container className="container-super-wide">
                <div className="pt-64 pb-96">
                    <Animated dataAos="fade-down" className="text-centre mb-48">
                        <Typography className="article-feature-caption">
                            {data?.isLoading ? <Skeleton variant="text" /> : features?.caption.toUpperCase()}
                        </Typography>
                    </Animated>
                    <Animated dataAos="fade-up">
                        <Grid container spacing={8}>
                            <Grid item xs={12} md={6} className="article-feature-images-column">
                                <Grid container spacing={2}>
                                    <Grid item xs={12} md={8}>
                                        <div className="article-feature-image-box">
                                            <Card className="article-feature-card card-image" elevation={0}>
                                                {data?.isLoading ? (
                                                    <Skeleton variant="rect" height="256px" />
                                                ) : (
                                                    <CardMedia
                                                        component="img"
                                                        loading="lazy"
                                                        title="Illustration"
                                                        alt="An image illustrating listed features"
                                                        className="article-feature-card-media-large lazyloaded"
                                                        image={GetImageUrl({
                                                            base: GET_ARTICLE_IMAGE_URL,
                                                            name: features?.image1,
                                                        })}
                                                    />
                                                )}
                                            </Card>
                                        </div>
                                    </Grid>
                                    <Grid item xs={12} md={4}>
                                        <Grid container spacing={2}>
                                            <Grid item xs={12} className="article-feature-empty-card-container">
                                                <Card className="article-feature-empty-card" elevation={0}></Card>
                                            </Grid>
                                            <Grid item xs={12}>
                                                <Card className="card-image">
                                                    {data?.isLoading ? (
                                                        <Skeleton variant="rect" height="256px" />
                                                    ) : (
                                                        <CardMedia
                                                            component="img"
                                                            loading="lazy"
                                                            title="Illustration"
                                                            alt="An image illustrating listed features"
                                                            className="article-feature-card-media lazyloaded"
                                                            image={GetImageUrl({
                                                                base: GET_ARTICLE_IMAGE_URL,
                                                                name: features?.image2,
                                                            })}
                                                        />
                                                    )}
                                                </Card>
                                            </Grid>
                                        </Grid>
                                    </Grid>
                                </Grid>

                                <Grid container spacing={2}>
                                    <Grid item xs={12} md={4}>
                                        <Grid container spacing={2}>
                                            <Grid item xs={12}>
                                                <Card className="card-image" elevation={0}>
                                                    {data?.isLoading ? (
                                                        <Skeleton variant="rect" height="256px" />
                                                    ) : (
                                                        <CardMedia
                                                            component="img"
                                                            loading="lazy"
                                                            title="Illustration"
                                                            alt="An image illustrating listed features"
                                                            className="article-feature-card-media lazyloaded"
                                                            image={GetImageUrl({
                                                                base: GET_ARTICLE_IMAGE_URL,
                                                                name: features?.image3,
                                                            })}
                                                        />
                                                    )}
                                                </Card>
                                            </Grid>
                                            <Grid item xs={12} className="article-feature-empty-card-container">
                                                <Card className="article-feature-empty-card" elevation={0}></Card>
                                            </Grid>
                                        </Grid>
                                    </Grid>
                                    <Grid item xs={12} md={8}>
                                        <div className="article-feature-image-box">
                                            <Card className="article-feature-card card-image" elevation={0}>
                                                {data?.isLoading ? (
                                                    <Skeleton variant="rect" height="256px" />
                                                ) : (
                                                    <CardMedia
                                                        component="img"
                                                        loading="lazy"
                                                        title="Illustration"
                                                        alt="An image illustrating listed features"
                                                        className="article-feature-card-media-large lazyloaded"
                                                        image={GetImageUrl({
                                                            base: GET_ARTICLE_IMAGE_URL,
                                                            name: features?.image4,
                                                        })}
                                                    />
                                                )}
                                            </Card>
                                        </div>
                                    </Grid>
                                </Grid>
                            </Grid>

                            <Grid item xs={12} md={6} className="article-feature-content-container">
                                <div className="article-feature-content-box">
                                    <h2 className="article-feature-title">
                                        {data?.isLoading ? <Skeleton variant="text" /> : features?.title}
                                    </h2>
                                    <div className="article-feature-description mt-15">
                                        {data?.isLoading ? <Skeleton variant="text" /> : <p>{features?.description}</p>}
                                    </div>
                                    <div className="article-feature-description mb-32">
                                        {data?.isLoading ? <Skeleton variant="text" /> : <p>{features?.text}</p>}
                                    </div>
                                    <div className="text-left">
                                        {data?.isLoading ? (
                                            <Skeleton variant="rect" width="100%" height="25px" />
                                        ) : (
                                            <ActiveButton isLoading={data?.isLoading} {...features} />
                                        )}
                                    </div>
                                </div>
                            </Grid>
                        </Grid>
                    </Animated>
                </div>
            </Container>
        </section>
    );
};
