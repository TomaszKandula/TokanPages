import * as React from "react";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import { Carousel } from "react-responsive-carousel";
import useStyles from "./styledTestimonials";
import "react-responsive-carousel/lib/styles/carousel.min.css";
import "./overrideCarousel.css";

export default function Testimonials()
{

    const classes = useStyles();
    const content = 
    {
        cpation: "Testimonials",
        subtitle: "Here you can read few commendations, a word from people I worked with.",
        photo1: "https://maindbstorage.blob.core.windows.net/tokanpages/images/section_testimonials/joanna.jpg",
        name1: "Joanna Strzyzewska",
        occupation1: "Senior Digital Tax Specialist",
        text1: `Tomek is very motivated, has wide IT knowledge and is constantly developing in new technologies. 
                I have seen many examples of his talent. He is also a good and friendly co-worker and colleague. 
                I can recommend Tomek not only because of his technical and programming skillset but also
                because of his attitude towards changes and best practices.`,
        photo2: "https://maindbstorage.blob.core.windows.net/tokanpages/images/section_testimonials/adama.jpg",
        name2: "Adama Sow",
        occupation2: "Full-stack Developer chez DFDS POLSKA",
        text2: `Tomek have done very good work in NET Core, Azure. He is really helpful. I would recommend him.`,
        photo3: "https://maindbstorage.blob.core.windows.net/tokanpages/images/section_testimonials/scott.jpg",
        name3: "Scott Lumsden",
        occupation3: "Customer Service | Contact Center | Vendor Manager | BPO",
        text3: `Tomasz is one of those rare talents, he is extremely adaptable, and capable in turning ideas to reality. 
                His programming ability allows him to build scalable robust digital solutions.
                While working with him at the accounting shared service center for DFDS, he single handily was also able to 
                produce software solutions with ERP integration helping the AR, AP and GL departments work more effectively.`
    };

    return(
        <section className={classes.section}>
            <Container maxWidth="lg"> 
                <div data-aos="fade-up">
                    <Box pt={8} pb={10}>
                        <Container maxWidth="sm">
                            <Box textAlign="center" mb={5}>
                                <Typography variant="h4" component="h2" gutterBottom={true}>
                                    {content.cpation}
                                </Typography>
                                <Typography variant="subtitle1" color="textSecondary">
                                    {content.subtitle}
                                </Typography>
                            </Box>
                        </Container>
                        <Carousel showArrows={true} infiniteLoop={true} showThumbs={false} showStatus={false} autoPlay={true} interval={6100}>
                            <Box className={classes.boxPadding}>
                                <img src={content.photo1} className={classes.img} alt="" />
                                <Typography variant="h4" component="h3" align="center" className={classes.title}>
                                    {content.name1}
                                </Typography>
                                <Typography variant="subtitle1" component="h4" align="center" className={classes.subtitle}>
                                    {content.occupation1}
                                </Typography>
                                <Typography variant="body1" component="p" align="center" color="textSecondary" className={classes.commendation}>
                                    {content.text1}
                                </Typography>
                            </Box>
                            <Box className={classes.boxPadding}>
                                <img src={content.photo2} className={classes.img} alt="" />
                                <Typography variant="h4" component="h3" align="center" className={classes.title}>
                                    {content.name2}
                                </Typography>
                                <Typography variant="subtitle1" component="h4" align="center" className={classes.subtitle}>
                                    {content.occupation2}
                                </Typography>
                                <Typography variant="body1" component="p" align="center" color="textSecondary" className={classes.commendation}>
                                    {content.text2}
                                </Typography>
                            </Box>
                            <Box className={classes.boxPadding}>
                                <img src={content.photo3} className={classes.img} alt="" />
                                <Typography variant="h4" component="h3" align="center" className={classes.title}>
                                    {content.name3}
                                </Typography>
                                <Typography variant="subtitle1" component="h4" align="center" className={classes.subtitle}>
                                    {content.occupation3}
                                </Typography>
                                <Typography variant="body1" component="p" align="center" color="textSecondary" className={classes.commendation}>
                                    {content.text3}
                                </Typography>
                            </Box>
                        </Carousel>
                    </Box>
                </div>        
            </Container>
        </section>
    );

}
