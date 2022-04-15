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
import { IGetArticleFeatContent } from "../../Redux/States/Content/getArticleFeatContentState";
import { renderCardMedia } from "../../Shared/Components/CustomCardMedia/customCardMedia";
import { ARTICLE_IMAGE_PATH } from "../../Shared/constants";
import articleFeatStyle from "./Styles/articleFeatStyle";

const ArticleFeatView = (props: IGetArticleFeatContent): JSX.Element =>
{
    const classes = articleFeatStyle();

    const ActiveButton = (): JSX.Element => 
    {
        return(
            <Link to="/articles" className={classes.link}>
                <Button endIcon={<ArrowRightAltIcon />} className={classes.button}>
                    {props.isLoading ? <Skeleton variant="text" /> : props.content?.button}
                </Button>
            </Link>
        );
    }

    return (
        <section className={classes.section}>
            <Container maxWidth="lg">
                <Box pt={8} pb={10}>
                    <Container maxWidth="sm">
                        <Box textAlign="center" mb={5} data-aos="fade-down">
                            <Typography gutterBottom={true} className={classes.title}>
                                {props.isLoading ? <Skeleton variant="text" /> : props.content?.title.toUpperCase()}
                            </Typography>
                        </Box>
                    </Container>
                    <Container maxWidth="lg">
                        <div data-aos="fade-up">
                            <Grid container>
                                <Grid item xs={12} lg={6} className={classes.content}>
                                    <Card className={classes.info} elevation={3}>
                                        <CardContent className={classes.info} >
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
                                            <Card elevation={4}>
                                                {props.isLoading 
                                                    ? <Skeleton variant="rect" height="128px" /> 
                                                    : renderCardMedia(ARTICLE_IMAGE_PATH, props.content?.image1, classes.media)}
                                            </Card>
                                        </Grid>
                                        <Grid item xs={12} md={4}>
                                            <Card elevation={4}>
                                                {props.isLoading 
                                                    ? <Skeleton variant="rect" height="128px" /> 
                                                    : renderCardMedia(ARTICLE_IMAGE_PATH, props.content?.image2, classes.media)}
                                            </Card>
                                        </Grid>
                                        <Grid item xs={12} md={4}>
                                            <Card elevation={4}>
                                                {props.isLoading 
                                                    ? <Skeleton variant="rect" height="128px" /> 
                                                    : renderCardMedia(ARTICLE_IMAGE_PATH, props.content?.image3, classes.media)}
                                            </Card>
                                        </Grid> 
                                        <Grid item xs={12} md={8}>
                                            <Card elevation={4}>
                                                {props.isLoading 
                                                    ? <Skeleton variant="rect" height="128px" /> 
                                                    : renderCardMedia(ARTICLE_IMAGE_PATH, props.content?.image4, classes.media)}
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

export default ArticleFeatView;
