import * as React from "react";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import { Carousel } from "react-responsive-carousel";
import useStyles from "./styledTestimonials";
import { IMG_ADAMA, IMG_JOANNA, IMG_SCOTT } from "../../Shared/constants";
import "react-responsive-carousel/lib/styles/carousel.min.css";
import "./overrideCarousel.css";

export default function Testimonials()
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
                                    Testimonials
                                </Typography>
                                <Typography variant="subtitle1" color="textSecondary">
                                    Here you can read few commendations, a word from people I worked with.
                                </Typography>
                            </Box>
                        </Container>
                        <Carousel showArrows={true} infiniteLoop={true} showThumbs={false} showStatus={false} autoPlay={true} interval={6100}>
                            <Box className={classes.boxPadding}>
                                <img src={IMG_JOANNA} className={classes.img} alt="" />
                                <Typography variant="h4" component="h3" align="center" className={classes.title}>
                                    Joanna Strzyzewska
                                </Typography>
                                <Typography variant="subtitle1" component="h4" align="center" className={classes.subtitle}>
                                    Senior Digital Tax Specialist
                                </Typography>
                                <Typography variant="body1" component="p" align="center" color="textSecondary" className={classes.commendation}>
                                    Tomek is very motivated, has wide IT knowledge and is constantly developing in new technologies. 
                                    I have seen many examples of his talent. He is also a good and friendly co-worker and colleague. 
                                    I can recommend Tomek not only because of his technical and programming skillset but also
                                    because of his attitude towards changes and best practices.                                
                                </Typography>
                            </Box>
                            <Box className={classes.boxPadding}>
                                <img src={IMG_ADAMA} className={classes.img} alt="" />
                                <Typography variant="h4" component="h3" align="center" className={classes.title}>
                                    Adama Sow
                                </Typography>
                                <Typography variant="subtitle1" component="h4" align="center" className={classes.subtitle}>
                                    Full-stack Developer chez DFDS POLSKA
                                </Typography>
                                <Typography variant="body1" component="p" align="center" color="textSecondary" className={classes.commendation}>
                                    Tomek have done very good work in NET Core, Azure. He is really helpful. I would recommend him.
                                </Typography>
                            </Box>
                            <Box className={classes.boxPadding}>
                                <img src={IMG_SCOTT} className={classes.img} alt="" />
                                <Typography variant="h4" component="h3" align="center" className={classes.title}>
                                    Scott Lumsden
                                </Typography>
                                <Typography variant="subtitle1" component="h4" align="center" className={classes.subtitle}>
                                    Customer Service | Contact Center | Vendor Manager | BPO
                                </Typography>
                                <Typography variant="body1" component="p" align="center" color="textSecondary" className={classes.commendation}>
                                    Tomasz is one of those rare talents, he is extremely adaptable, and capable in turning ideas to reality. 
                                    His programming ability allows him to build scalable robust digital solutions.
                                    While working with him at the accounting shared service center for DFDS, he single handily was also able to 
                                    produce software solutions with ERP integration helping the AR, AP and GL departments work more effectively. 
                                </Typography>
                            </Box>
                        </Carousel>
                    </Box>
                </div>        
            </Container>
        </section>
    );

}
