import * as React from "react";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
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

export const TestimonialsView = (props: TestimonialsViewProps): React.ReactElement => {
    const imageUrl = (name: string) => {
        if (name === "") return " ";
        return `${GET_TESTIMONIALS_URL}/${name}`;
    };

    return (
        <section className="section" style={props.background}>
            <Container className="container-super-wide">
                <Box pt={8} pb={10} textAlign="center" mb={5}>
                    <Typography className="testimonials-caption-text" data-aos="fade-down">
                        {props.isLoading ? <Skeleton variant="text" /> : props.caption?.toUpperCase()}
                    </Typography>
                </Box>
                <Box pb={15} textAlign="center">
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
                                {props.isLoading ? (
                                    <Skeleton variant="rect" className="testimonials-card-image" />
                                ) : (
                                    <CardMedia
                                        image={imageUrl(props.photo1)}
                                        component="img"
                                        className="testimonials-card-image"
                                        alt={`photo of ${props.photo1}`}
                                    />
                                )}
                                <CardContent className="testimonials-card-content">
                                    <Typography className="testimonials-card-title">
                                        {props.isLoading ? <Skeleton variant="text" /> : props.name1}
                                    </Typography>
                                    <Typography className="testimonials-card-subheader">
                                        {props.isLoading ? <Skeleton variant="text" /> : props.occupation1}
                                    </Typography>
                                    <Collapse in={props.hasTestimonialOne} collapsedSize={120} timeout="auto">
                                        <Typography className="testimonials-card-text">
                                            {props.isLoading ? <Skeleton variant="text" /> : props.text1}
                                        </Typography>
                                    </Collapse>
                                    <IconButton
                                        className={props.hasTestimonialOne ? "testimonials-expand testimonials-expand-open" : "testimonials-expand"}
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
                                {props.isLoading ? (
                                    <Skeleton variant="rect" className="testimonials-card-image" />
                                ) : (
                                    <CardMedia
                                        image={imageUrl(props.photo2)}
                                        component="img"
                                        className="testimonials-card-image"
                                        alt={`photo of ${props.photo2}`}
                                    />
                                )}
                                <CardContent className="testimonials-card-content">
                                    <Typography className="testimonials-card-title">
                                        {props.isLoading ? <Skeleton variant="text" /> : props.name2}
                                    </Typography>
                                    <Typography className="testimonials-card-subheader">
                                        {props.isLoading ? <Skeleton variant="text" /> : props.occupation2}
                                    </Typography>
                                    <Collapse in={props.hasTestimonialTwo} collapsedSize={120} timeout="auto">
                                        <Typography className="testimonials-card-text">
                                            {props.isLoading ? <Skeleton variant="text" /> : props.text2}
                                        </Typography>
                                    </Collapse>
                                    <IconButton
                                        className={props.hasTestimonialTwo ? "testimonials-expand testimonials-expand-open" : "testimonials-expand"}
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
                                {props.isLoading ? (
                                    <Skeleton variant="rect" className="testimonials-card-image" />
                                ) : (
                                    <CardMedia
                                        image={imageUrl(props.photo3)}
                                        component="img"
                                        className="testimonials-card-image"
                                        alt={`photo of ${props.photo3}`}
                                    />
                                )}
                                <CardContent className="testimonials-card-content">
                                    <Typography className="testimonials-card-title">
                                        {props.isLoading ? <Skeleton variant="text" /> : props.name3}
                                    </Typography>
                                    <Typography className="testimonials-card-subheader">
                                        {props.isLoading ? <Skeleton variant="text" /> : props.occupation3}
                                    </Typography>
                                    <Collapse in={props.hasTestimonialThree} collapsedSize={120} timeout="auto">
                                        <Typography className="testimonials-card-text">
                                            {props.isLoading ? <Skeleton variant="text" /> : props.text3}
                                        </Typography>
                                    </Collapse>
                                    <IconButton
                                        className={props.hasTestimonialThree ? "testimonials-expand testimonials-expand-open" : "testimonials-expand"}
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
                </Box>
            </Container>
        </section>
    );
};
