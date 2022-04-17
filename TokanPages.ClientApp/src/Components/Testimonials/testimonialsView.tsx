import * as React from "react";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Grid from "@material-ui/core/Grid";
import Card from "@material-ui/core/Card";
import CardMedia from "@material-ui/core/CardMedia";
import CardContent from "@material-ui/core/CardContent";
import Typography from "@material-ui/core/Typography";
import Skeleton from "@material-ui/lab/Skeleton";
import { IGetTestimonialsContent } from "../../Redux/States/Content/getTestimonialsContentState";
import { TESTIMONIALS_PATH } from "../../Shared/constants";
import { GetShortText } from "../../Shared/helpers";
import testimonialsStyle from "./testimonialsStyle";
import "react-responsive-carousel/lib/styles/carousel.min.css";
import "../../Theme/CustomCss/overrideCarousel.css";

const TestimonialsView = (props: IGetTestimonialsContent): JSX.Element =>
{
    const classes = testimonialsStyle();
    const limit = 29;
    const imageUrl = (name: string) => 
    {
        if (name === "") return " ";
        return `${TESTIMONIALS_PATH}${name}`;
    }

    return(
        <section className={classes.section}>
            <Container maxWidth="lg"> 
                <Box pt={8} pb={10} textAlign="center" mb={5}>
                    <Typography gutterBottom={true} className={classes.caption_text} data-aos="fade-down">
                        {props.isLoading ? <Skeleton variant="text" /> : props.content?.caption?.toUpperCase()}
                    </Typography>
                </Box>
                <Box pb={15} textAlign="center">
                    <Grid container spacing={6}>
                        <Grid item xs={12} md={4} data-aos="fade-left">
                            <Card elevation={0} className={classes.card}>
                                <CardMedia image={imageUrl(props.content?.photo1)} component="img" className={classes.card_image} />
                                <CardContent className={classes.card_content}>
                                    <Typography className={classes.card_title}>
                                        {props.content?.name1}
                                    </Typography>
                                    <Typography className={classes.card_subheader}>
                                        {props.content?.occupation1}
                                    </Typography>
                                    <Typography className={classes.card_text}>
                                        {GetShortText(props.content?.text1, limit)}
                                    </Typography>
                                </CardContent>
                            </Card>
                        </Grid>
                        <Grid item xs={12} md={4} data-aos="fade-up">
                            <Card elevation={3} className={classes.card}>
                                <CardMedia image={imageUrl(props.content?.photo2)} component="img" className={classes.card_image} />
                                <CardContent className={classes.card_content}>
                                <Typography className={classes.card_title}>
                                        {props.content?.name2}
                                    </Typography>
                                    <Typography className={classes.card_subheader}>
                                        {props.content?.occupation2}
                                    </Typography>
                                    <Typography className={classes.card_text}>
                                        {GetShortText(props.content?.text2, limit)}
                                    </Typography>
                                </CardContent>
                            </Card>
                        </Grid>
                        <Grid item xs={12} md={4} data-aos="fade-right">
                            <Card elevation={3} className={classes.card}>
                                <CardMedia image={imageUrl(props.content?.photo3)} component="img" className={classes.card_image} />
                                <CardContent className={classes.card_content}>
                                <Typography className={classes.card_title}>
                                        {props.content?.name3}
                                    </Typography>
                                    <Typography className={classes.card_subheader}>
                                        {props.content?.occupation3}
                                    </Typography>
                                    <Typography className={classes.card_text}>
                                        {GetShortText(props.content?.text3, limit)}
                                    </Typography>
                                </CardContent>
                            </Card>
                        </Grid>
                    </Grid>
                </Box>
            </Container>
        </section>
    );
}

export default TestimonialsView;
