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
import { ReactHtmlParser } from "../../../Shared/Services/Renderers";
import { ContentFeaturesState } from "../../../Store/States";
import { TechnologiesStyle } from "./technologiesStyle";

export const TechnologiesView = (props: ContentFeaturesState): JSX.Element =>
{
    const classes = TechnologiesStyle();
    return (
        <section className={classes.section}>
            <Container maxWidth="lg">
                <Box py={8}>
                    <Box mb={8}>
                        <div data-aos="fade-down">
                            <Typography className={classes.caption_text}>
                                {props.isLoading 
                                ? <Skeleton variant="text" /> 
                                : props.content?.caption?.toUpperCase()}
                            </Typography>
                        </div>
                    </Box>
                    <Grid container spacing={6}>
                        <Grid item xs={12} sm={6}>
                            <Box mb={2} display="flex" alignItems="center" data-aos="fade-up">
                                {props.isLoading 
                                    ? <Skeleton variant="circle" className={classes.skeleton_circle} /> 
                                    : <CodeIcon className={classes.icon} />}
                                <Typography className={classes.feature_title}>
                                    {props.isLoading 
                                    ? <Skeleton variant="text" width="250px" /> 
                                    : props.content?.title1}
                                </Typography>
                            </Box>
                            <Typography component="span" className={classes.feature_text} data-aos="fade-up">
                                {props.isLoading 
                                ? <Skeleton variant="text" /> 
                                : <ReactHtmlParser html={props.content?.text1} />}
                            </Typography>
                        </Grid>
                        <Grid item xs={12} sm={6}>
                            <Box mb={2} display="flex" alignItems="center" data-aos="fade-up">
                                {props.isLoading 
                                    ? <Skeleton variant="circle" className={classes.skeleton_circle} /> 
                                    : <LibraryBooksIcon className={classes.icon} />}
                                <Typography className={classes.feature_title}>
                                    {props.isLoading 
                                    ? <Skeleton variant="text" width="250px" /> 
                                    : props.content?.title2}
                                </Typography>
                            </Box>
                            <Typography component="span" className={classes.feature_text} data-aos="fade-up">
                                {props.isLoading 
                                ? <Skeleton variant="text" /> 
                                : <ReactHtmlParser html={props.content?.text2} />}
                            </Typography>
                        </Grid>
                        <Grid item xs={12} sm={6}>
                            <Box mb={2} display="flex" alignItems="center" data-aos="fade-up">
                                {props.isLoading 
                                    ? <Skeleton variant="circle" className={classes.skeleton_circle} /> 
                                    : <StorageIcon className={classes.icon} />}
                                <Typography className={classes.feature_title}>
                                    {props.isLoading ? <Skeleton variant="text" width="250px" /> : props.content?.title3}
                                </Typography>
                            </Box>
                            <Typography component="span" className={classes.feature_text} data-aos="fade-up">
                                {props.isLoading 
                                ? <Skeleton variant="text" /> 
                                : <ReactHtmlParser html={props.content?.text3} />}
                            </Typography>
                        </Grid>
                        <Grid item xs={12} sm={6}>
                            <Box mb={2} display="flex" alignItems="center" data-aos="fade-up">
                                {props.isLoading 
                                    ? <Skeleton variant="circle" className={classes.skeleton_circle} /> 
                                    : <CloudIcon color="primary" className={classes.icon} />}
                                <Typography className={classes.feature_title}>
                                    {props.isLoading 
                                    ? <Skeleton variant="text" width="250px" /> 
                                    : props.content?.title4}
                                </Typography>
                            </Box>
                            <Typography component="span" className={classes.feature_text} data-aos="fade-up">
                                {props.isLoading 
                                ? <Skeleton variant="text" /> 
                                : <ReactHtmlParser html={props.content?.text4} />}
                            </Typography>
                        </Grid>
                    </Grid>
                </Box>
            </Container>
        </section>
    );
}
