import * as React from "react";
import clsx from "clsx";
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
import { TestimonialsStyle } from "./testimonialsStyle";

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
}

export const TestimonialsView = (props: TestimonialsViewProps): JSX.Element => {
    const classes = TestimonialsStyle();
    const imageUrl = (name: string) => {
        if (name === "") return " ";
        return `${GET_TESTIMONIALS_URL}/${name}`;
    };

    return (
        <section className={classes.section}>
            <Container maxWidth="lg">
                <Box pt={8} pb={10} textAlign="center" mb={5}>
                    <Typography className={classes.caption_text} data-aos="fade-down">
                        {props.isLoading ? <Skeleton variant="text" /> : props.caption?.toUpperCase()}
                    </Typography>
                </Box>
                <Box pb={15} textAlign="center">
                    <Grid container spacing={6}>
                        <Grid item xs={12} md={4} data-aos="fade-up" data-aos-delay="350">
                            <Card elevation={0} className={classes.card}>
                                {props.isLoading ? (
                                    <Skeleton variant="rect" className={classes.card_image} />
                                ) : (
                                    <CardMedia
                                        image={imageUrl(props.photo1)}
                                        component="img"
                                        className={classes.card_image}
                                    />
                                )}
                                <CardContent className={classes.card_content}>
                                    <Typography className={classes.card_title}>
                                        {props.isLoading ? <Skeleton variant="text" /> : props.name1}
                                    </Typography>
                                    <Typography className={classes.card_subheader}>
                                        {props.isLoading ? <Skeleton variant="text" /> : props.occupation1}
                                    </Typography>
                                    <Collapse in={props.hasTestimonialOne} collapsedSize={120} timeout="auto">
                                        <Typography className={classes.card_text}>
                                            {props.isLoading ? <Skeleton variant="text" /> : props.text1}
                                        </Typography>
                                    </Collapse>
                                    <IconButton
                                        className={clsx(classes.expand, {
                                            [classes.expand_open]: props.hasTestimonialOne,
                                        })}
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
                            className={classes.card_space}
                            data-aos="fade-up"
                            data-aos-delay="150"
                        >
                            <Card elevation={3} className={classes.card}>
                                {props.isLoading ? (
                                    <Skeleton variant="rect" className={classes.card_image} />
                                ) : (
                                    <CardMedia
                                        image={imageUrl(props.photo2)}
                                        component="img"
                                        className={classes.card_image}
                                    />
                                )}
                                <CardContent className={classes.card_content}>
                                    <Typography className={classes.card_title}>
                                        {props.isLoading ? <Skeleton variant="text" /> : props.name2}
                                    </Typography>
                                    <Typography className={classes.card_subheader}>
                                        {props.isLoading ? <Skeleton variant="text" /> : props.occupation2}
                                    </Typography>
                                    <Collapse in={props.hasTestimonialTwo} collapsedSize={120} timeout="auto">
                                        <Typography className={classes.card_text}>
                                            {props.isLoading ? <Skeleton variant="text" /> : props.text2}
                                        </Typography>
                                    </Collapse>
                                    <IconButton
                                        className={clsx(classes.expand, {
                                            [classes.expand_open]: props.hasTestimonialTwo,
                                        })}
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
                            className={classes.card_space}
                            data-aos="fade-up"
                            data-aos-delay="550"
                        >
                            <Card elevation={3} className={classes.card}>
                                {props.isLoading ? (
                                    <Skeleton variant="rect" className={classes.card_image} />
                                ) : (
                                    <CardMedia
                                        image={imageUrl(props.photo3)}
                                        component="img"
                                        className={classes.card_image}
                                    />
                                )}
                                <CardContent className={classes.card_content}>
                                    <Typography className={classes.card_title}>
                                        {props.isLoading ? <Skeleton variant="text" /> : props.name3}
                                    </Typography>
                                    <Typography className={classes.card_subheader}>
                                        {props.isLoading ? <Skeleton variant="text" /> : props.occupation3}
                                    </Typography>
                                    <Collapse in={props.hasTestimonialThree} collapsedSize={120} timeout="auto">
                                        <Typography className={classes.card_text}>
                                            {props.isLoading ? <Skeleton variant="text" /> : props.text3}
                                        </Typography>
                                    </Collapse>
                                    <IconButton
                                        className={clsx(classes.expand, {
                                            [classes.expand_open]: props.hasTestimonialThree,
                                        })}
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
