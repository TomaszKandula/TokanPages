import * as React from "react";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import { Carousel } from "react-responsive-carousel";
import { ITestimonials } from "../../Api/Models";
import { renderImage } from "../../Shared/Components/renderImage";
import useStyles from "./styledTestimonials";
import "react-responsive-carousel/lib/styles/carousel.min.css";
import "./overrideCarousel.css";

export default function Testimonials(props: ITestimonials)
{
    const classes = useStyles();
    return(
        <section className={classes.section}>
            <Container maxWidth="lg"> 
                <div data-aos="fade-up">
                    <Box pt={8} pb={10}>
                        <Container maxWidth="sm">
                            <Box textAlign="center" mb={5}>
                                <Typography variant="h4" component="h2" gutterBottom={true}>
                                    {props.content.caption}
                                </Typography>
                                <Typography variant="subtitle1" color="textSecondary">
                                    {props.content.subtitle}
                                </Typography>
                            </Box>
                        </Container>
                        <Carousel showArrows={true} infiniteLoop={true} showThumbs={false} showStatus={false} autoPlay={true} interval={6100}>
                            <Box className={classes.boxPadding}>
                                {renderImage("", props.content.photo1, classes.img)}
                                <Typography variant="h4" component="h3" align="center" className={classes.title}>
                                    {props.content.name1}
                                </Typography>
                                <Typography variant="subtitle1" component="h4" align="center" className={classes.subtitle}>
                                    {props.content.occupation1}
                                </Typography>
                                <Typography variant="body1" component="p" align="center" color="textSecondary" className={classes.commendation}>
                                    {props.content.text1}
                                </Typography>
                            </Box>
                            <Box className={classes.boxPadding}>
                                {renderImage("", props.content.photo2, classes.img)}
                                <Typography variant="h4" component="h3" align="center" className={classes.title}>
                                    {props.content.name2}
                                </Typography>
                                <Typography variant="subtitle1" component="h4" align="center" className={classes.subtitle}>
                                    {props.content.occupation2}
                                </Typography>
                                <Typography variant="body1" component="p" align="center" color="textSecondary" className={classes.commendation}>
                                    {props.content.text2}
                                </Typography>
                            </Box>
                            <Box className={classes.boxPadding}>
                                {renderImage("", props.content.photo3, classes.img)}
                                <Typography variant="h4" component="h3" align="center" className={classes.title}>
                                    {props.content.name3}
                                </Typography>
                                <Typography variant="subtitle1" component="h4" align="center" className={classes.subtitle}>
                                    {props.content.occupation3}
                                </Typography>
                                <Typography variant="body1" component="p" align="center" color="textSecondary" className={classes.commendation}>
                                    {props.content.text3}
                                </Typography>
                            </Box>
                        </Carousel>
                    </Box>
                </div>        
            </Container>
        </section>
    );
}
