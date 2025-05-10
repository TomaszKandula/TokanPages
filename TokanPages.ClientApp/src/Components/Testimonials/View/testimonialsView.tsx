import * as React from "react";
import Container from "@material-ui/core/Container";
import Grid from "@material-ui/core/Grid";
import Card from "@material-ui/core/Card";
import CardMedia from "@material-ui/core/CardMedia";
import CardContent from "@material-ui/core/CardContent";
import Typography from "@material-ui/core/Typography";
import Skeleton from "@material-ui/lab/Skeleton";
import { GET_TESTIMONIALS_URL } from "../../../Api";
import { ViewProperties } from "../../../Shared/Abstractions";
import { Animated } from "../../../Shared/Components";
import { Collapsible } from "../../../Shared/Components";
import "./testimonialsView.css";

interface TestimonialsViewProps extends ViewProperties {
    caption: string;
    subtitle: string;
    photo1: string;
    name1: string;
    occupation1: string;
    text1: string;
    photo2: string;
    name2: string;
    occupation2: string;
    text2: string;
    photo3: string;
    name3: string;
    occupation3: string;
    text3: string;
    background?: string;
}

interface RenderSkeletonOrElementProps extends TestimonialsViewProps {
    className?: string;
    variant: "rect" | "text";
    object: React.ReactElement | string;
}

const RenderSkeletonOrElement = (props: RenderSkeletonOrElementProps): React.ReactElement => {
    return props.isLoading ? <Skeleton variant={props.variant} className={props.className} /> : <>{props.object}</>;
};

export const TestimonialsView = (props: TestimonialsViewProps): React.ReactElement => {
    const imageUrl = (name: string) => {
        if (name === "") return " ";
        return `${GET_TESTIMONIALS_URL}/${name}`;
    };

    return (
        <section className={`section ${props.background ?? ""}`}>
            <Container className="container-super-wide">
                <div className="text-centre pt-64 testimonials-bottom-padding">
                    <Animated dataAos="fade-down">
                        <Typography className="testimonials-caption-text">
                            <RenderSkeletonOrElement {...props} variant="text" object={props.caption?.toUpperCase()} />
                        </Typography>
                    </Animated>
                </div>
                <div className="text-centre pb-120">
                    <Grid container spacing={6}>
                        <Grid item xs={12} md={4} className="testimonials-card-holder">
                            <Animated dataAos="fade-up" dataAosDelay={350}>
                                <Card elevation={0} className="testimonials-card">
                                    <RenderSkeletonOrElement
                                        {...props}
                                        variant="rect"
                                        object={
                                            <CardMedia
                                                image={imageUrl(props.photo1)}
                                                component="img"
                                                loading="lazy"
                                                className="testimonials-card-image lazyloaded"
                                                title="Testimonials"
                                                alt={`Picture of ${props.name1}`}
                                            />
                                        }
                                        className="testimonials-card-image"
                                    />
                                    <CardContent className="testimonials-card-content">
                                        <Typography className="testimonials-card-title">
                                            <RenderSkeletonOrElement {...props} variant="text" object={props.name1} />
                                        </Typography>
                                        <Typography className="testimonials-card-subheader">
                                            <RenderSkeletonOrElement
                                                {...props}
                                                variant="text"
                                                object={props.occupation1}
                                            />
                                        </Typography>
                                        <Collapsible minHeight={120}>
                                            <h4 className="testimonials-card-text">
                                                <RenderSkeletonOrElement
                                                    {...props}
                                                    variant="text"
                                                    object={props.text1}
                                                />
                                            </h4>
                                        </Collapsible>
                                    </CardContent>
                                </Card>
                            </Animated>
                        </Grid>

                        <Grid item xs={12} md={4} className="testimonials-card-holder">
                            <Animated dataAos="fade-up" dataAosDelay={150}>
                                <Card elevation={3} className="testimonials-card">
                                    <RenderSkeletonOrElement
                                        {...props}
                                        variant="rect"
                                        object={
                                            <CardMedia
                                                image={imageUrl(props.photo2)}
                                                component="img"
                                                loading="lazy"
                                                className="testimonials-card-image lazyloaded"
                                                title="Testimonials"
                                                alt={`Picture of ${props.name2}`}
                                            />
                                        }
                                        className="testimonials-card-image"
                                    />
                                    <CardContent className="testimonials-card-content">
                                        <Typography className="testimonials-card-title">
                                            <RenderSkeletonOrElement {...props} variant="text" object={props.name2} />
                                        </Typography>
                                        <Typography className="testimonials-card-subheader">
                                            <RenderSkeletonOrElement
                                                {...props}
                                                variant="text"
                                                object={props.occupation2}
                                            />
                                        </Typography>
                                        <Collapsible minHeight={120}>
                                            <h4 className="testimonials-card-text">
                                                <RenderSkeletonOrElement
                                                    {...props}
                                                    variant="text"
                                                    object={props.text2}
                                                />
                                            </h4>
                                        </Collapsible>
                                    </CardContent>
                                </Card>
                            </Animated>
                        </Grid>

                        <Grid item xs={12} md={4} className="testimonials-card-holder">
                            <Animated dataAos="fade-up" dataAosDelay={250}>
                                <Card elevation={3} className="testimonials-card">
                                    <RenderSkeletonOrElement
                                        {...props}
                                        variant="rect"
                                        object={
                                            <CardMedia
                                                image={imageUrl(props.photo3)}
                                                component="img"
                                                loading="lazy"
                                                className="testimonials-card-image lazyloaded"
                                                title="Testimonials"
                                                alt={`Picture of ${props.name3}`}
                                            />
                                        }
                                        className="testimonials-card-image"
                                    />
                                    <CardContent className="testimonials-card-content">
                                        <Typography className="testimonials-card-title">
                                            <RenderSkeletonOrElement {...props} variant="text" object={props.name3} />
                                        </Typography>
                                        <Typography className="testimonials-card-subheader">
                                            <RenderSkeletonOrElement
                                                {...props}
                                                variant="text"
                                                object={props.occupation3}
                                            />
                                        </Typography>
                                        <Collapsible minHeight={120}>
                                            <h4 className="testimonials-card-text">
                                                <RenderSkeletonOrElement
                                                    {...props}
                                                    variant="text"
                                                    object={props.text3}
                                                />
                                            </h4>
                                        </Collapsible>
                                    </CardContent>
                                </Card>
                            </Animated>
                        </Grid>
                    </Grid>
                </div>
            </Container>
        </section>
    );
};
