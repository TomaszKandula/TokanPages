import * as React from "react";
import { Link } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Grid from "@material-ui/core/Grid";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import Button from "@material-ui/core/Button";
import ArrowRightAltIcon from "@material-ui/icons/ArrowRightAlt";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import Skeleton from "@material-ui/lab/Skeleton";
import { IGetArticleFeaturesContent } from "../../../Store/States/Content/getArticleFeaturesContentState";
import { RenderCardMedia } from "../../../Shared/Components";
import { ARTICLE_IMAGE_PATH } from "../../../Shared/constants";
import { FeaturesStyle } from "./featuresStyle";
import Validate from "validate.js";

export const FeaturesView = (props: IGetArticleFeaturesContent): JSX.Element =>
{
    const classes = FeaturesStyle();

    const ActiveButton = (): JSX.Element => 
    {
        if (Validate.isEmpty(props.content?.action?.href))
        {
            return (
                <Button endIcon={<ArrowRightAltIcon />} className={classes.button}>
                    {props.isLoading ? <Skeleton variant="text" /> : props.content?.action?.text}
                </Button>
            );
        }

        return(
            <Link to={props.content?.action?.href} className={classes.link}>
                <Button endIcon={<ArrowRightAltIcon />} className={classes.button}>
                    {props.isLoading ? <Skeleton variant="text" /> : props.content?.action?.text}
                </Button>
            </Link>
        );
    }

    return (
        <section className={classes.section}>
            <Container maxWidth="lg">
                <Box pt={8} pb={12}>
                    <Box textAlign="center" mb={6} data-aos="fade-down">
                        <Typography className={classes.title}>
                            {props.isLoading ? <Skeleton variant="text" /> : props.content?.title.toUpperCase()}
                        </Typography>
                    </Box>
                    <Container maxWidth="lg">
                        <div data-aos="fade-up">
                            <Grid container>
                                <Grid item xs={12} lg={6} className={classes.content}>
                                    <Card elevation={0} className={classes.card}>
                                        <CardContent className={classes.card_content}>
                                            <Box display="flex" flexDirection="column" height="100%" pt={2} px={2}>
                                                <Typography className={classes.text1}>
                                                    {props.isLoading ? <Skeleton variant="text" /> : props.content?.text1}
                                                </Typography>
                                                <Box mt="auto" mb={2}>
                                                    <Typography className={classes.text2}>
                                                        {props.isLoading ? <Skeleton variant="text" /> : props.content?.text2}
                                                    </Typography>
                                                </Box>
                                                <Box textAlign="right">
                                                    {props.isLoading ? <Skeleton variant="rect" width="100%" height="25px" /> : <ActiveButton />}
                                                </Box>
                                            </Box>
                                        </CardContent>
                                    </Card>
                                </Grid>
                                <Grid item xs={12} lg={6}>
                                    <Grid container spacing={2}>
                                        <Grid item xs={12} md={8}>
                                            <Card elevation={0} className={classes.card_image}>
                                                {props.isLoading 
                                                    ? <Skeleton variant="rect" height="128px" /> 
                                                    : RenderCardMedia(ARTICLE_IMAGE_PATH, props.content?.image1, classes.media)}
                                            </Card>
                                        </Grid>
                                        <Grid item xs={12} md={4}>
                                            <Card elevation={0} className={classes.card_image}>
                                                {props.isLoading 
                                                    ? <Skeleton variant="rect" height="128px" /> 
                                                    : RenderCardMedia(ARTICLE_IMAGE_PATH, props.content?.image2, classes.media)}
                                            </Card>
                                        </Grid>
                                        <Grid item xs={12} md={4}>
                                            <Card elevation={0} className={classes.card_image}>
                                                {props.isLoading 
                                                    ? <Skeleton variant="rect" height="128px" /> 
                                                    : RenderCardMedia(ARTICLE_IMAGE_PATH, props.content?.image3, classes.media)}
                                            </Card>
                                        </Grid> 
                                        <Grid item xs={12} md={8}>
                                            <Card elevation={0} className={classes.card_image}>
                                                {props.isLoading 
                                                    ? <Skeleton variant="rect" height="128px" /> 
                                                    : RenderCardMedia(ARTICLE_IMAGE_PATH, props.content?.image4, classes.media)}
                                            </Card>
                                        </Grid>
                                    </Grid>
                                </Grid>
                            </Grid>
                        </div>
                    </Container>
                </Box>
            </Container>
        </section>
    );
}
