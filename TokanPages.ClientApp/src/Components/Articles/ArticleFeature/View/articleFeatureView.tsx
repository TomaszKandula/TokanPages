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

interface DisplayParagraphsProps {
    text: string[];
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
        <Link to={props?.action?.href ?? ""} className="link" rel="noopener nofollow">
            <Button endIcon={<ArrowRightAltIcon />} className="button">
                {props?.isLoading ? <Skeleton variant="text" /> : props?.action?.text}
            </Button>
        </Link>
    );
};

const DisplayParagraphs = (props: DisplayParagraphsProps): React.ReactElement => {
    const RenderLine = (props: { value: string, index: number }) => {
        if (props.index === 0) {
            return (
                <h3 className="article-features-text2-heading">
                    {props.value}
                </h3>
            );
        } else {
            return (
                <p>
                    {props.value}
                </p>
            );
        }
    }

    const lines = props.text?.map((value: string, index: number) => (
        <RenderLine key={index}  value={value} index={index} />
    ));

    return <>{lines}</>;
}

export const ArticleFeatureView = (props: ArticleFeatureViewProps): React.ReactElement => {
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const features = data?.components?.articleFeatures;
    return (
        <section className={`section ${props.background ?? ""}`}>
            <Container className="container-super-wide">
                <div className="pt-64 pb-96">
                    <Animated dataAos="fade-down" className="text-centre mb-48">
                        <Typography className="article-features-title">
                            {data?.isLoading ? <Skeleton variant="text" /> : features?.title.toUpperCase()}
                        </Typography>
                    </Animated>
                    <Animated dataAos="fade-up">
                        <Grid container>
                            <Grid item xs={12} lg={6} className="article-features-content">
                                <Card elevation={0} className="article-features-card">
                                    <CardContent className="article-features-card-content">
                                        <div className="article-features-card-box">
                                            <h2 className="article-features-text1">
                                                {data?.isLoading ? <Skeleton variant="text" /> : features?.text1}
                                            </h2>
                                            <div className="article-features-text2 mt-15 mb-40">
                                                {data?.isLoading ? <Skeleton variant="text" /> : <DisplayParagraphs text={features?.text2} />}
                                            </div>
                                            <div className="text-right">
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
                                                <CardMedia
                                                    component="img"
                                                    loading="lazy"
                                                    image={GetImageUrl({
                                                        base: GET_ARTICLE_IMAGE_URL,
                                                        name: features?.image1,
                                                    })}
                                                    className="article-features-media lazyloaded"
                                                    title="Illustration"
                                                    alt="An image illustrating listed features"
                                                />
                                            )}
                                        </Card>
                                    </Grid>
                                    <Grid item xs={12} md={4}>
                                        <Card elevation={0} className="card-image">
                                            {data?.isLoading ? (
                                                <Skeleton variant="rect" height="128px" />
                                            ) : (
                                                <CardMedia
                                                    component="img"
                                                    loading="lazy"
                                                    image={GetImageUrl({
                                                        base: GET_ARTICLE_IMAGE_URL,
                                                        name: features?.image2,
                                                    })}
                                                    className="article-features-media lazyloaded"
                                                    title="Illustration"
                                                    alt="An image illustrating listed features"
                                                />
                                            )}
                                        </Card>
                                    </Grid>

                                    <Grid item xs={12} md={4}>
                                        <Card elevation={0} className="card-image">
                                            {data?.isLoading ? (
                                                <Skeleton variant="rect" height="128px" />
                                            ) : (
                                                <CardMedia
                                                    component="img"
                                                    loading="lazy"
                                                    image={GetImageUrl({
                                                        base: GET_ARTICLE_IMAGE_URL,
                                                        name: features?.image3,
                                                    })}
                                                    className="article-features-media article-features-media-visibility lazyloaded"
                                                    title="Illustration"
                                                    alt="An image illustrating listed features"
                                                />
                                            )}
                                        </Card>
                                    </Grid>
                                    <Grid item xs={12} md={8}>
                                        <Card elevation={0} className="card-image">
                                            {data?.isLoading ? (
                                                <Skeleton variant="rect" height="128px" />
                                            ) : (
                                                <CardMedia
                                                    component="img"
                                                    loading="lazy"
                                                    image={GetImageUrl({
                                                        base: GET_ARTICLE_IMAGE_URL,
                                                        name: features?.image4,
                                                    })}
                                                    className="article-features-media article-features-media-visibility lazyloaded"
                                                    title="Illustration"
                                                    alt="An image illustrating listed features"
                                                />
                                            )}
                                        </Card>
                                    </Grid>

                                </Grid>
                            </Grid>
                        </Grid>
                    </Animated>
                </div>
            </Container>
        </section>
    );
};
