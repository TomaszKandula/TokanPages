import * as React from "react";
import { useSelector } from "react-redux";
import { Link } from "react-router-dom";
import { Button, Card, CardMedia, Container, Grid, Typography } from "@material-ui/core";
import { Skeleton } from "@material-ui/lab";
import ArrowRightAltIcon from "@material-ui/icons/ArrowRightAlt";
import { GET_SHOWCASE_IMAGE_URL } from "../../../Api";
import { FeatureShowcaseContentDto } from "../../../Api/Models";
import { ApplicationState } from "../../../Store/Configuration";
import { GetImageUrl } from "../../../Shared/Services/Utilities";
import { Animated } from "../../../Shared/Components";
import Validate from "validate.js";

interface ShowcaseViewProps {
    background?: string;
}

interface ActiveButtonProps extends FeatureShowcaseContentDto {
    isLoading: boolean;
}

interface RenderSkeletonOrElementProps extends ShowcaseViewProps {
    isLoading: boolean;
    className?: string;
    variant: "rect" | "text";
    object: React.ReactElement | string;
}

const ActiveButton = (props: ActiveButtonProps): React.ReactElement => {
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

const RenderSkeletonOrElement = (props: RenderSkeletonOrElementProps): React.ReactElement => {
    return props.isLoading ? <Skeleton variant={props.variant} className={props.className} /> : <>{props.object}</>;
};

export const ShowcaseView = (props: ShowcaseViewProps): React.ReactElement => {
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const isLoading = data?.isLoading;
    const showcase = data?.components?.sectionShowcase;

    return (
        <section className={`section ${props.background ?? ""}`}>
            <Container className="container-super-wide">
                <div className="pt-64 pb-96">
                    <Animated dataAos="fade-down" className="text-centre mb-48">
                        <Typography className="showcase-caption-text">
                            <RenderSkeletonOrElement
                                isLoading={isLoading}
                                variant="text"
                                object={showcase?.caption?.toUpperCase()}
                            />
                        </Typography>
                    </Animated>
                    <Animated dataAos="fade-up">
                        <Grid container spacing={8}>
                            <Grid item xs={12} md={6}>
                                <div className="showcase-feature-box">
                                    <div className="showcase-feature-textbox">
                                        <h2 className="showcase-feature-text1">
                                            {isLoading ? <Skeleton variant="text" /> : showcase?.heading}
                                        </h2>
                                        <div className="showcase-feature-text2 mt-15 mb-32">
                                            {isLoading ? <Skeleton variant="text" /> : <p>{showcase?.text}</p>}
                                        </div>
                                        <div className="text-left">
                                            {data?.isLoading ? (
                                                <Skeleton variant="rect" width="100%" height="25px" />
                                            ) : (
                                                <ActiveButton isLoading={isLoading} {...showcase} />
                                            )}
                                        </div>
                                    </div>
                                </div>
                            </Grid>
                            <Grid item xs={12} md={6}>
                                <Card elevation={0} className="card-image">
                                    <CardMedia
                                        component="img"
                                        loading="lazy"
                                        title="Illustration"
                                        alt="An image illustrating showcase page"
                                        className="showcase-feature-image lazyloaded"
                                        image={GetImageUrl({ base: GET_SHOWCASE_IMAGE_URL, name: showcase?.image })}
                                    />
                                </Card>
                            </Grid>
                        </Grid>
                    </Animated>
                </div>
            </Container>
        </section>
    );
};
