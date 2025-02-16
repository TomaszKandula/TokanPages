import * as React from "react";
import { Card, CardContent, CardMedia, Container, Grid, Typography } from "@material-ui/core";
import { Skeleton } from "@material-ui/lab";
import { GET_SOCIALS_URL } from "../../../Api/Request";
import { ApplicationState } from "../../../Store/Configuration";
import { Animated } from "../../../Shared/Components";
import { useSelector } from "react-redux";

interface SocialsViewProps {
    background?: string;
}

interface RenderSkeletonOrElementProps extends SocialsViewProps {
    isLoading: boolean;
    className?: string;
    variant: "rect" | "text";
    object: React.ReactElement | string;
}

const RenderSkeletonOrElement = (props: RenderSkeletonOrElementProps): React.ReactElement => {
    return props.isLoading ? <Skeleton variant={props.variant} className={props.className} /> : <>{props.object}</>;
};

export const SocialsView = (props: SocialsViewProps): React.ReactElement => {
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const isLoading = data.isLoading;
    const socials = data.components?.socials;
    const imageUrl = (name: string) => {
        if (name === "") return " ";
        return `${GET_SOCIALS_URL}/${name}`;
    };

    return (
        <section className={`section-grey ${props.background ?? ""}`}>
            <Container className="container-super-wide">

                <div className="text-centre pt-64 pb-120">
                    <Animated dataAos="fade-down">
                        <h1 className="testimonials-caption-text">
                            <RenderSkeletonOrElement 
                                isLoading={isLoading}
                                variant="text" 
                                object={socials.caption?.toUpperCase()} 
                            />
                        </h1>
                    </Animated>
                </div>

                <div className="text-centre pb-120">
                    <Grid container spacing={6}>
                        <Grid item xs={12} md={4} className="testimonials-card-holder">
                            <Animated dataAos="fade-up" dataAosDelay={350}>
                                <Card elevation={0} className="testimonials-card">
                                    <RenderSkeletonOrElement
                                        isLoading={isLoading}
                                        variant="rect"
                                        object={
                                            <CardMedia
                                                image={imageUrl(socials.social1?.images?.avatar)}
                                                component="img"
                                                className="testimonials-card-image"
                                                alt="Testimonail photo 1 of 3"
                                            />
                                        }
                                        className="testimonials-card-image"
                                    />
                                    <CardContent className="testimonials-card-content">
                                        <Typography className="testimonials-card-title">
                                            <RenderSkeletonOrElement
                                                isLoading={isLoading}
                                                variant="text" 
                                                object={socials.social1?.textTitle} 
                                            />
                                        </Typography>
                                        <Typography className="testimonials-card-subheader">
                                            <RenderSkeletonOrElement
                                                isLoading={isLoading}
                                                variant="text"
                                                object={socials.social1?.textSubtitle}
                                            />
                                        </Typography>
                                    </CardContent>
                                </Card>
                            </Animated>
                        </Grid>

                        <Grid item xs={12} md={4} className="testimonials-card-holder">
                            <Animated dataAos="fade-up" dataAosDelay={150}>
                                <Card elevation={3} className="testimonials-card">
                                    <RenderSkeletonOrElement
                                        isLoading={isLoading}
                                        variant="rect"
                                        object={
                                            <CardMedia
                                                image={imageUrl(socials.social2?.images?.avatar)}
                                                component="img"
                                                className="testimonials-card-image"
                                                alt="Testimonail photo 2 of 3"
                                            />
                                        }
                                        className="testimonials-card-image"
                                    />
                                    <CardContent className="testimonials-card-content">
                                        <Typography className="testimonials-card-title">
                                            <RenderSkeletonOrElement 
                                                isLoading={isLoading}
                                                variant="text" 
                                                object={socials.social2?.textTitle} 
                                            />
                                        </Typography>
                                        <Typography className="testimonials-card-subheader">
                                            <RenderSkeletonOrElement
                                                isLoading={isLoading}
                                                variant="text"
                                                object={socials.social2?.textSubtitle}
                                            />
                                        </Typography>
                                    </CardContent>
                                </Card>
                            </Animated>
                        </Grid>

                        <Grid item xs={12} md={4} className="testimonials-card-holder">
                            <Animated dataAos="fade-up" dataAosDelay={250}>
                                <Card elevation={3} className="testimonials-card">
                                    <RenderSkeletonOrElement
                                        isLoading={isLoading}
                                        variant="rect"
                                        object={
                                            <CardMedia
                                                image={imageUrl(socials.social3?.images?.avatar)}
                                                component="img"
                                                className="testimonials-card-image"
                                                alt="Testimonail photo 3 of 3"
                                            />
                                        }
                                        className="testimonials-card-image"
                                    />
                                    <CardContent className="testimonials-card-content">
                                        <Typography className="testimonials-card-title">
                                            <RenderSkeletonOrElement 
                                                isLoading={isLoading}
                                                variant="text" 
                                                object={socials.social3?.textTitle} 
                                            />
                                        </Typography>
                                        <Typography className="testimonials-card-subheader">
                                            <RenderSkeletonOrElement
                                                isLoading={isLoading}
                                                variant="text"
                                                object={socials.social3?.textSubtitle} 
                                            />
                                        </Typography>
                                    </CardContent>
                                </Card>
                            </Animated>
                        </Grid>
                    </Grid>
                </div>

            </Container>
        </section>
    );
}
