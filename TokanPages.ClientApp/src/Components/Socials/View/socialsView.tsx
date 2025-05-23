import * as React from "react";
import { useSelector } from "react-redux";
import { Card, CardActionArea, CardContent, CardMedia, Container, Grid, Typography } from "@material-ui/core";
import { Skeleton } from "@material-ui/lab";
import { GET_SOCIALS_URL } from "../../../Api";
import { ApplicationState } from "../../../Store/Configuration";
import { Animated, GetIcon } from "../../../Shared/Components";
import { GetImageUrl } from "../../../Shared/Services/Utilities";
import "./socialsView.css";

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
    const isLoading = data?.isLoading;
    const socials = data?.components?.sectionSocials;

    return (
        <section className={`section ${props.background ?? ""}`}>
            <Container className="container-super-wide">
                <div className="text-centre pt-64 pb-40">
                    <Animated dataAos="fade-down">
                        <Typography className="socials-caption-text">
                            <RenderSkeletonOrElement
                                isLoading={isLoading}
                                variant="text"
                                object={socials?.caption?.toUpperCase()}
                            />
                        </Typography>
                    </Animated>
                </div>

                <div className="text-centre pb-120">
                    <Grid container spacing={6}>
                        <Grid item xs={12} md={4} className="socials-card-holder">
                            <Animated dataAos="fade-up" dataAosDelay={350}>
                                <Card elevation={0} className="card">
                                    <CardActionArea
                                        href={socials?.social1?.action?.href}
                                        target="_blank"
                                        rel="noopener"
                                    >
                                        <CardMedia
                                            component="img"
                                            loading="lazy"
                                            image={GetImageUrl({
                                                base: GET_SOCIALS_URL,
                                                name: socials?.social1?.images?.header,
                                            })}
                                            className="socials-card-media lazyloaded"
                                            title="Illustration"
                                            alt={socials?.social1?.textTitle}
                                        />
                                        <RenderSkeletonOrElement
                                            isLoading={isLoading}
                                            variant="rect"
                                            object={
                                                <div className="socials-card-image-holder">
                                                    <CardMedia
                                                        image={GetImageUrl({
                                                            base: GET_SOCIALS_URL,
                                                            name: socials?.social1?.images?.avatar,
                                                        })}
                                                        component="img"
                                                        loading="lazy"
                                                        className="socials-card-image lazyloaded"
                                                        title="Socials"
                                                        alt={socials?.social1?.textTitle}
                                                    />
                                                </div>
                                            }
                                            className="socials-card-image"
                                        />
                                        <CardContent className="socials-card-content">
                                            <div className="socials-card-icon-holder">
                                                <GetIcon
                                                    name={socials?.social1?.images?.icon}
                                                    className="socials-card-icon"
                                                />
                                            </div>
                                            <div className="socials-card-title">
                                                <RenderSkeletonOrElement
                                                    isLoading={isLoading}
                                                    variant="text"
                                                    object={socials?.social1?.textTitle}
                                                />
                                            </div>
                                            <h3 className="socials-card-subheader">
                                                <RenderSkeletonOrElement
                                                    isLoading={isLoading}
                                                    variant="text"
                                                    object={socials?.social1?.textSubtitle}
                                                />
                                            </h3>
                                            <h4 className="socials-card-subtext">
                                                <RenderSkeletonOrElement
                                                    isLoading={isLoading}
                                                    variant="text"
                                                    object={socials?.social1?.textComment}
                                                />
                                            </h4>
                                        </CardContent>
                                    </CardActionArea>
                                </Card>
                            </Animated>
                        </Grid>

                        <Grid item xs={12} md={4} className="socials-card-holder">
                            <Animated dataAos="fade-up" dataAosDelay={150}>
                                <Card elevation={3} className="card">
                                    <CardActionArea
                                        href={socials?.social2?.action?.href}
                                        target="_blank"
                                        rel="noopener"
                                    >
                                        <CardMedia
                                            component="img"
                                            loading="lazy"
                                            image={GetImageUrl({
                                                base: GET_SOCIALS_URL,
                                                name: socials?.social2?.images?.header,
                                            })}
                                            className="socials-card-media lazyloaded"
                                            title="Illustration"
                                            alt={socials?.social2?.textTitle}
                                        />
                                        <RenderSkeletonOrElement
                                            isLoading={isLoading}
                                            variant="rect"
                                            object={
                                                <div className="socials-card-image-holder">
                                                    <CardMedia
                                                        image={GetImageUrl({
                                                            base: GET_SOCIALS_URL,
                                                            name: socials?.social2?.images?.avatar,
                                                        })}
                                                        component="img"
                                                        loading="lazy"
                                                        className="socials-card-image lazyloaded"
                                                        title="Socials"
                                                        alt={socials?.social2?.textTitle}
                                                    />
                                                </div>
                                            }
                                            className="socials-card-image"
                                        />
                                        <CardContent className="socials-card-content">
                                            <div className="socials-card-icon-holder">
                                                <GetIcon
                                                    name={socials?.social2?.images?.icon}
                                                    className="socials-card-icon"
                                                />
                                            </div>
                                            <div className="socials-card-title">
                                                <RenderSkeletonOrElement
                                                    isLoading={isLoading}
                                                    variant="text"
                                                    object={socials?.social2?.textTitle}
                                                />
                                            </div>
                                            <h3 className="socials-card-subheader">
                                                <RenderSkeletonOrElement
                                                    isLoading={isLoading}
                                                    variant="text"
                                                    object={socials?.social2?.textSubtitle}
                                                />
                                            </h3>
                                            <h4 className="socials-card-subtext">
                                                <RenderSkeletonOrElement
                                                    isLoading={isLoading}
                                                    variant="text"
                                                    object={socials?.social2?.textComment}
                                                />
                                            </h4>
                                        </CardContent>
                                    </CardActionArea>
                                </Card>
                            </Animated>
                        </Grid>

                        <Grid item xs={12} md={4} className="socials-card-holder">
                            <Animated dataAos="fade-up" dataAosDelay={250}>
                                <Card elevation={3} className="card">
                                    <CardActionArea
                                        href={socials?.social3?.action?.href}
                                        target="_blank"
                                        rel="noopener"
                                    >
                                        <CardMedia
                                            component="img"
                                            loading="lazy"
                                            image={GetImageUrl({
                                                base: GET_SOCIALS_URL,
                                                name: socials?.social3?.images?.header,
                                            })}
                                            className="socials-card-media lazyloaded"
                                            title="Illustration"
                                            alt={socials?.social3?.textTitle}
                                        />
                                        <RenderSkeletonOrElement
                                            isLoading={isLoading}
                                            variant="rect"
                                            object={
                                                <div className="socials-card-image-holder">
                                                    <CardMedia
                                                        image={GetImageUrl({
                                                            base: GET_SOCIALS_URL,
                                                            name: socials?.social3?.images?.avatar,
                                                        })}
                                                        component="img"
                                                        loading="lazy"
                                                        className="socials-card-image lazyloaded"
                                                        title="Socials"
                                                        alt={socials?.social3?.textTitle}
                                                    />
                                                </div>
                                            }
                                            className="socials-card-image"
                                        />
                                        <CardContent className="socials-card-content">
                                            <div className="socials-card-icon-holder">
                                                <GetIcon
                                                    name={socials?.social3?.images?.icon}
                                                    className="socials-card-icon"
                                                />
                                            </div>
                                            <div className="socials-card-title">
                                                <RenderSkeletonOrElement
                                                    isLoading={isLoading}
                                                    variant="text"
                                                    object={socials?.social3?.textTitle}
                                                />
                                            </div>
                                            <h3 className="socials-card-subheader">
                                                <RenderSkeletonOrElement
                                                    isLoading={isLoading}
                                                    variant="text"
                                                    object={socials?.social3?.textSubtitle}
                                                />
                                            </h3>
                                            <h4 className="socials-card-subtext">
                                                <RenderSkeletonOrElement
                                                    isLoading={isLoading}
                                                    variant="text"
                                                    object={socials?.social3?.textComment}
                                                />
                                            </h4>
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
