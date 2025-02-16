import * as React from "react";
import { useSelector } from "react-redux";
import { Card, CardActionArea, CardContent, CardMedia, Container, Grid } from "@material-ui/core";
import { Skeleton } from "@material-ui/lab";
import { GET_SOCIALS_URL } from "../../../Api/Request";
import { ApplicationState } from "../../../Store/Configuration";
import { Animated, GetIcon, RenderCardMedia } from "../../../Shared/Components";

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
    const socials = data?.components?.socials;
    const imageUrl = (name: string) => {
        if (name === "") return " ";
        return `${GET_SOCIALS_URL}/${name}`;
    };

    return (
        <section className={`section-grey ${props.background ?? ""}`}>
            <Container className="container-super-wide">

                <div className="text-centre pt-64 pb-40">
                    <Animated dataAos="fade-down">
                        <h1 className="socials-caption-text">
                            <RenderSkeletonOrElement 
                                isLoading={isLoading}
                                variant="text" 
                                object={socials?.caption?.toUpperCase()} 
                            />
                        </h1>
                    </Animated>
                </div>

                <div className="text-centre pb-120">
                    <Grid container spacing={6}>
                        <Grid item xs={12} md={4} className="socials-card-holder">
                            <Animated dataAos="fade-up" dataAosDelay={350}>
                                <Card elevation={0} className="card">
                                    <CardActionArea href={socials?.social1?.action?.href} target="_blank" rel="noopener">
                                        {RenderCardMedia(GET_SOCIALS_URL, socials?.social1?.images?.header, "socials-card-media")}
                                        <RenderSkeletonOrElement
                                            isLoading={isLoading}
                                            variant="rect"
                                            object={
                                                <div className="socials-card-image-holder">
                                                    <CardMedia
                                                        image={imageUrl(socials?.social1?.images?.avatar)}
                                                        component="img"
                                                        className="socials-card-image"
                                                        alt="Socials photo 1 of 3"
                                                    />
                                                </div>
                                            }
                                            className="socials-card-image"
                                        />
                                        <CardContent className="socials-card-content">
                                            <div className="socials-card-icon-holder">
                                                <GetIcon name={socials?.social1?.images?.icon} className="socials-card-icon" />
                                            </div>
                                            <h2 className="socials-card-title">
                                                <RenderSkeletonOrElement
                                                    isLoading={isLoading}
                                                    variant="text" 
                                                    object={socials?.social1?.textTitle} 
                                                />
                                            </h2>
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
                                    <CardActionArea href={socials?.social2?.action?.href} target="_blank" rel="noopener">
                                        {RenderCardMedia(GET_SOCIALS_URL, socials?.social2?.images?.header, "socials-card-media")}
                                        <RenderSkeletonOrElement
                                            isLoading={isLoading}
                                            variant="rect"
                                            object={
                                                <div className="socials-card-image-holder">
                                                    <CardMedia
                                                        image={imageUrl(socials?.social2?.images?.avatar)}
                                                        component="img"
                                                        className="socials-card-image"
                                                        alt="Socials photo 2 of 3"
                                                    />
                                                </div>
                                            }
                                            className="socials-card-image"
                                        />
                                        <CardContent className="socials-card-content">
                                            <div className="socials-card-icon-holder">
                                                <GetIcon name={socials?.social2?.images?.icon} className="socials-card-icon" />
                                            </div>
                                            <h2 className="socials-card-title">
                                                <RenderSkeletonOrElement 
                                                    isLoading={isLoading}
                                                    variant="text" 
                                                    object={socials?.social2?.textTitle} 
                                                />
                                            </h2>
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
                                    <CardActionArea href={socials?.social3?.action?.href} target="_blank" rel="noopener">
                                        {RenderCardMedia(GET_SOCIALS_URL, socials?.social3?.images?.header, "socials-card-media")}
                                        <RenderSkeletonOrElement
                                            isLoading={isLoading}
                                            variant="rect"
                                            object={
                                                <div className="socials-card-image-holder">
                                                    <CardMedia
                                                        image={imageUrl(socials?.social3?.images?.avatar)}
                                                        component="img"
                                                        className="socials-card-image"
                                                        alt="Socials photo 3 of 3"
                                                    />
                                                </div>
                                            }
                                            className="socials-card-image"
                                        />
                                        <CardContent className="socials-card-content">
                                            <div className="socials-card-icon-holder">
                                                <GetIcon name={socials?.social3?.images?.icon} className="socials-card-icon" />
                                            </div>
                                            <h2 className="socials-card-title">
                                                <RenderSkeletonOrElement 
                                                    isLoading={isLoading}
                                                    variant="text" 
                                                    object={socials?.social3?.textTitle} 
                                                />
                                            </h2>
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
}
