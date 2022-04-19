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
import { GetShortText } from "../../Shared/Services/Utilities";
import testimonialsStyle from "./testimonialsStyle";

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
                                {props.isLoading 
                                ? <Skeleton variant="rect" className={classes.card_image} /> 
                                : <CardMedia image={imageUrl(props.content?.photo1)} component="img" className={classes.card_image} />}
                                <CardContent className={classes.card_content}>
                                    <Typography className={classes.card_title}>
                                        {props.isLoading ? <Skeleton variant="text" /> : props.content?.name1}
                                    </Typography>
                                    <Typography className={classes.card_subheader}>
                                        {props.isLoading ? <Skeleton variant="text" /> : props.content?.occupation1}
                                    </Typography>
                                    <Typography className={classes.card_text}>
                                        {props.isLoading ? <Skeleton variant="text" /> : GetShortText({ value: props.content?.text1, limit: limit })}
                                    </Typography>
                                </CardContent>
                            </Card>
                        </Grid>
                        <Grid item xs={12} md={4} data-aos="fade-up"  className={classes.card_space}>
                            <Card elevation={3} className={classes.card}>
                                {props.isLoading 
                                ? <Skeleton variant="rect" className={classes.card_image} /> 
                                : <CardMedia image={imageUrl(props.content?.photo2)} component="img" className={classes.card_image} />}
                                <CardContent className={classes.card_content}>
                                <Typography className={classes.card_title}>
                                        {props.isLoading ? <Skeleton variant="text" /> : props.content?.name2}
                                    </Typography>
                                    <Typography className={classes.card_subheader}>
                                        {props.isLoading ? <Skeleton variant="text" /> : props.content?.occupation2}
                                    </Typography>
                                    <Typography className={classes.card_text}>
                                        {props.isLoading ? <Skeleton variant="text" /> : GetShortText({ value: props.content?.text2, limit: limit })}
                                    </Typography>
                                </CardContent>
                            </Card>
                        </Grid>
                        <Grid item xs={12} md={4} data-aos="fade-right"  className={classes.card_space}>
                            <Card elevation={3} className={classes.card}>
                                {props.isLoading 
                                ? <Skeleton variant="rect" className={classes.card_image} /> 
                                : <CardMedia image={imageUrl(props.content?.photo3)} component="img" className={classes.card_image} />}
                                <CardContent className={classes.card_content}>
                                <Typography className={classes.card_title}>
                                        {props.isLoading ? <Skeleton variant="text" /> : props.content?.name3}
                                    </Typography>
                                    <Typography className={classes.card_subheader}>
                                        {props.isLoading ? <Skeleton variant="text" /> : props.content?.occupation3}
                                    </Typography>
                                    <Typography className={classes.card_text}>
                                        {props.isLoading ? <Skeleton variant="text" /> : GetShortText({ value: props.content?.text3, limit: limit })}
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
