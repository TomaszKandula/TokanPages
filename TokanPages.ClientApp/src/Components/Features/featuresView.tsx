import * as React from "react";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import Grid from "@material-ui/core/Grid";
import CodeIcon from "@material-ui/icons/Code";
import LibraryBooksIcon from "@material-ui/icons/LibraryBooks";
import StorageIcon from "@material-ui/icons/Storage";
import CloudIcon from "@material-ui/icons/Cloud";
import Skeleton from "@material-ui/lab/Skeleton";
import ReactHtmlParser from "react-html-parser";
import { IGetFeaturesContent } from "../../Redux/States/Content/getFeaturesContentState";
import featuresStyle from "./featuresStyle";

const FeaturesView = (props: IGetFeaturesContent): JSX.Element =>
{
    const classes = featuresStyle();
    return (
        <section className={classes.section}>
            <Container maxWidth="lg">
                <Box py={8}>
                    <Box mb={8}>
                        <div data-aos="fade-down">
                            <Typography gutterBottom={true} className={classes.caption_text}>
                                {props.isLoading ? <Skeleton variant="text" /> : props.content?.caption?.toUpperCase()}
                            </Typography>
                        </div>
                    </Box>
                    <Grid container spacing={6}>
                        <Grid item xs={12} sm={6}>
                            <Box mb={2} display="flex" alignItems="center" data-aos="zoom-in">
                                <CodeIcon className={classes.icon} />
                                <Typography className={classes.feature_title}>
                                    {props.isLoading ? <Skeleton variant="text" /> : props.content?.title1}
                                </Typography>
                            </Box>
                            <Typography className={classes.feature_text} data-aos="fade-left">
                                {props.isLoading ? <Skeleton variant="text" /> : ReactHtmlParser(props.content?.text1)}
                            </Typography>
                        </Grid>
                        <Grid item xs={12} sm={6}>
                            <Box mb={2} display="flex" alignItems="center" data-aos="zoom-in">
                                <LibraryBooksIcon className={classes.icon} />
                                <Typography className={classes.feature_title}>
                                    {props.isLoading ? <Skeleton variant="text" /> : props.content?.title2}
                                </Typography>
                            </Box>
                            <Typography className={classes.feature_text} data-aos="fade-left">
                                {props.isLoading ? <Skeleton variant="text" /> : ReactHtmlParser(props.content?.text2)}
                            </Typography>
                        </Grid>
                        <Grid item xs={12} sm={6}>
                            <Box mb={2} display="flex" alignItems="center" data-aos="zoom-in">
                                <StorageIcon className={classes.icon} />
                                <Typography className={classes.feature_title}>
                                    {props.isLoading ? <Skeleton variant="text" /> : props.content?.title3}
                                </Typography>
                            </Box>
                            <Typography className={classes.feature_text} data-aos="fade-right">
                                {props.isLoading ? <Skeleton variant="text" /> : ReactHtmlParser(props.content?.text3)}
                            </Typography>
                        </Grid>
                        <Grid item xs={12} sm={6}>         
                            <Box mb={2} display="flex" alignItems="center" data-aos="zoom-in">
                                <CloudIcon color="primary" className={classes.icon} />
                                <Typography className={classes.feature_title}>
                                    {props.isLoading ? <Skeleton variant="text" /> : props.content?.title4}
                                </Typography>
                            </Box>
                            <Typography className={classes.feature_text} data-aos="fade-right">
                                {props.isLoading ? <Skeleton variant="text" /> : ReactHtmlParser(props.content?.text4)}
                            </Typography>
                        </Grid>
                    </Grid>
                </Box>
            </Container>
        </section>
    );
}

export default FeaturesView;
