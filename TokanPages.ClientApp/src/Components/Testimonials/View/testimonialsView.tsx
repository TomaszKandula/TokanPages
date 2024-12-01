import * as React from "react";
import Container from "@material-ui/core/Container";
import Grid from "@material-ui/core/Grid";
import Card from "@material-ui/core/Card";
import CardMedia from "@material-ui/core/CardMedia";
import CardContent from "@material-ui/core/CardContent";
import Typography from "@material-ui/core/Typography";
import Skeleton from "@material-ui/lab/Skeleton";
import { Collapse, IconButton } from "@material-ui/core";
import ExpandMoreIcon from "@material-ui/icons/ExpandMore";
import { GET_TESTIMONIALS_URL } from "../../../Api/Request";
import { ViewProperties } from "../../../Shared/Abstractions";

interface TestimonialsViewProps extends ViewProperties {
    hasTestimonialOne: boolean;
    hasTestimonialTwo: boolean;
    hasTestimonialThree: boolean;
    buttonTestimonialOne: () => void;
    buttonTestimonialTwo: () => void;
    buttonTestimonialThree: () => void;
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
    background?: React.CSSProperties;
}

interface RenderSkeletonOrElementProps extends TestimonialsViewProps {
    className?: string;
    variant: "rect" | "text";
    object: React.ReactElement | string;
}

const RenderSkeletonOrElement = (props: RenderSkeletonOrElementProps): React.ReactElement => {
    return props.isLoading ? <Skeleton variant={props.variant} className={props.className} /> : <>{props.object}</>;
}

export const TestimonialsView = (props: TestimonialsViewProps): React.ReactElement => {
    const imageUrl = (name: string) => {
        if (name === "") return " ";
        return `${GET_TESTIMONIALS_URL}/${name}`;
    };

    return (
        <section className="section" style={props.background}>
            <Container className="container-super-wide">
                <div style={{ textAlign: "center", paddingTop: 64, paddingBottom: 120 }}>
                    <Typography className="testimonials-caption-text" data-aos="fade-down">
                        <RenderSkeletonOrElement {...props} variant="text" object={props.caption?.toUpperCase()} />
                    </Typography>
                </div>
                <div style={{ textAlign: "center", paddingBottom: 120 }}>
                    <Grid container spacing={6}>
                        <Grid
                            item
                            xs={12}
                            md={4}
                            data-aos="fade-up"
                            data-aos-delay="350"
                            className="testimonials-card-holder"
                        >
                            <Card elevation={0} className="testimonials-card">
                                <RenderSkeletonOrElement {...props} variant="rect" object={<CardMedia
                                        image={imageUrl(props.photo1)}
                                        component="img"
                                        className="testimonials-card-image"
                                        alt="Testimonail photo 1 of 3"
                                    />} className="testimonials-card-image" 
                                />
                                <CardContent className="testimonials-card-content">
                                    <Typography className="testimonials-card-title">
                                        <RenderSkeletonOrElement {...props} variant="text" object={props.name1} />
                                    </Typography>
                                    <Typography className="testimonials-card-subheader">
                                        <RenderSkeletonOrElement {...props} variant="text" object={props.occupation1} />
                                    </Typography>
                                    <Collapse in={props.hasTestimonialOne} collapsedSize={120} timeout="auto">
                                        <Typography className="testimonials-card-text">
                                            <RenderSkeletonOrElement {...props} variant="text" object={props.text1} />
                                        </Typography>
                                    </Collapse>
                                    <IconButton
                                        className={
                                            props.hasTestimonialOne
                                                ? "testimonials-expand testimonials-expand-open"
                                                : "testimonials-expand"
                                        }
                                        onClick={props.buttonTestimonialOne}
                                        aria-expanded={props.hasTestimonialOne}
                                        aria-label="show more"
                                    >
                                        <ExpandMoreIcon />
                                    </IconButton>
                                </CardContent>
                            </Card>
                        </Grid>
                        <Grid
                            item
                            xs={12}
                            md={4}
                            data-aos="fade-up"
                            data-aos-delay="150"
                            className="testimonials-card-holder"
                        >
                            <Card elevation={3} className="testimonials-card">
                                <RenderSkeletonOrElement {...props} variant="rect" object={<CardMedia
                                        image={imageUrl(props.photo2)}
                                        component="img"
                                        className="testimonials-card-image"
                                        alt="Testimonail photo 2 of 3"
                                    />} className="testimonials-card-image" 
                                />
                                <CardContent className="testimonials-card-content">
                                    <Typography className="testimonials-card-title">
                                        <RenderSkeletonOrElement {...props} variant="text" object={props.name2} />
                                    </Typography>
                                    <Typography className="testimonials-card-subheader">
                                        <RenderSkeletonOrElement {...props} variant="text" object={props.occupation2} />
                                    </Typography>
                                    <Collapse in={props.hasTestimonialTwo} collapsedSize={120} timeout="auto">
                                        <Typography className="testimonials-card-text">
                                            <RenderSkeletonOrElement {...props} variant="text" object={props.text2} />
                                        </Typography>
                                    </Collapse>
                                    <IconButton
                                        className={
                                            props.hasTestimonialTwo
                                                ? "testimonials-expand testimonials-expand-open"
                                                : "testimonials-expand"
                                        }
                                        onClick={props.buttonTestimonialTwo}
                                        aria-expanded={props.hasTestimonialTwo}
                                        aria-label="show more"
                                    >
                                        <ExpandMoreIcon />
                                    </IconButton>
                                </CardContent>
                            </Card>
                        </Grid>
                        <Grid
                            item
                            xs={12}
                            md={4}
                            data-aos="fade-up"
                            data-aos-delay="250"
                            className="testimonials-card-holder"
                        >
                            <Card elevation={3} className="testimonials-card">
                                <RenderSkeletonOrElement {...props} variant="rect" object={<CardMedia
                                        image={imageUrl(props.photo3)}
                                        component="img"
                                        className="testimonials-card-image"
                                        alt="Testimonail photo 3 of 3"
                                    />} className="testimonials-card-image" 
                                />
                                <CardContent className="testimonials-card-content">
                                    <Typography className="testimonials-card-title">
                                        <RenderSkeletonOrElement {...props} variant="text" object={props.name3} />
                                    </Typography>
                                    <Typography className="testimonials-card-subheader">
                                        <RenderSkeletonOrElement {...props} variant="text" object={props.occupation3} />
                                    </Typography>
                                    <Collapse in={props.hasTestimonialThree} collapsedSize={120} timeout="auto">
                                        <Typography className="testimonials-card-text">
                                            <RenderSkeletonOrElement {...props} variant="text" object={props.text3} />
                                        </Typography>
                                    </Collapse>
                                    <IconButton
                                        className={
                                            props.hasTestimonialThree
                                                ? "testimonials-expand testimonials-expand-open"
                                                : "testimonials-expand"
                                        }
                                        onClick={props.buttonTestimonialThree}
                                        aria-expanded={props.hasTestimonialThree}
                                        aria-label="show more"
                                    >
                                        <ExpandMoreIcon />
                                    </IconButton>
                                </CardContent>
                            </Card>
                        </Grid>
                    </Grid>
                </div>
            </Container>
        </section>
    );
};
