import * as React from "react";
import { useSelector } from "react-redux";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import Grid from "@material-ui/core/Grid";
import CodeIcon from "@material-ui/icons/Code";
import LibraryBooksIcon from "@material-ui/icons/LibraryBooks";
import StorageIcon from "@material-ui/icons/Storage";
import CloudIcon from "@material-ui/icons/Cloud";
import Skeleton from "@material-ui/lab/Skeleton";
import { ApplicationState } from "../../../Store/Configuration";
import { ReactHtmlParser } from "../../../Shared/Services/Renderers";
import { TechnologiesStyle } from "./technologiesStyle";

interface TechnologiesViewProps {
    background?: React.CSSProperties;
}

export const TechnologiesView = (props: TechnologiesViewProps): React.ReactElement => {
    const classes = TechnologiesStyle();
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const technology = data.components.technologies;

    return (
        <section className={classes.section} style={props.background}>
            <Container maxWidth="lg">
                <Box py={8}>
                    <Box mb={8}>
                        <div data-aos="fade-down">
                            <Typography className={classes.caption_text}>
                                {data?.isLoading ? <Skeleton variant="text" /> : technology?.caption?.toUpperCase()}
                            </Typography>
                        </div>
                    </Box>
                    <Grid container spacing={6}>
                        <Grid item xs={12} sm={6}>
                            <Box mb={2} display="flex" alignItems="center" data-aos="fade-up">
                                {data?.isLoading ? (
                                    <Skeleton variant="circle" className={classes.skeleton_circle} />
                                ) : (
                                    <CodeIcon className={classes.icon} />
                                )}
                                <Typography className={classes.feature_title}>
                                    {data?.isLoading ? <Skeleton variant="text" width="250px" /> : technology?.title1}
                                </Typography>
                            </Box>
                            <Typography component="span" className={classes.feature_text} data-aos="fade-up">
                                {data?.isLoading ? (
                                    <Skeleton variant="text" />
                                ) : (
                                    <ReactHtmlParser html={technology?.text1} />
                                )}
                            </Typography>
                        </Grid>
                        <Grid item xs={12} sm={6}>
                            <Box mb={2} display="flex" alignItems="center" data-aos="fade-up">
                                {data?.isLoading ? (
                                    <Skeleton variant="circle" className={classes.skeleton_circle} />
                                ) : (
                                    <LibraryBooksIcon className={classes.icon} />
                                )}
                                <Typography className={classes.feature_title}>
                                    {data?.isLoading ? <Skeleton variant="text" width="250px" /> : technology?.title2}
                                </Typography>
                            </Box>
                            <Typography component="span" className={classes.feature_text} data-aos="fade-up">
                                {data?.isLoading ? (
                                    <Skeleton variant="text" />
                                ) : (
                                    <ReactHtmlParser html={technology?.text2} />
                                )}
                            </Typography>
                        </Grid>
                        <Grid item xs={12} sm={6}>
                            <Box mb={2} display="flex" alignItems="center" data-aos="fade-up">
                                {data?.isLoading ? (
                                    <Skeleton variant="circle" className={classes.skeleton_circle} />
                                ) : (
                                    <StorageIcon className={classes.icon} />
                                )}
                                <Typography className={classes.feature_title}>
                                    {data?.isLoading ? <Skeleton variant="text" width="250px" /> : technology?.title3}
                                </Typography>
                            </Box>
                            <Typography component="span" className={classes.feature_text} data-aos="fade-up">
                                {data?.isLoading ? (
                                    <Skeleton variant="text" />
                                ) : (
                                    <ReactHtmlParser html={technology?.text3} />
                                )}
                            </Typography>
                        </Grid>
                        <Grid item xs={12} sm={6}>
                            <Box mb={2} display="flex" alignItems="center" data-aos="fade-up">
                                {data?.isLoading ? (
                                    <Skeleton variant="circle" className={classes.skeleton_circle} />
                                ) : (
                                    <CloudIcon color="primary" className={classes.icon} />
                                )}
                                <Typography className={classes.feature_title}>
                                    {data?.isLoading ? <Skeleton variant="text" width="250px" /> : technology?.title4}
                                </Typography>
                            </Box>
                            <Typography component="span" className={classes.feature_text} data-aos="fade-up">
                                {data?.isLoading ? (
                                    <Skeleton variant="text" />
                                ) : (
                                    <ReactHtmlParser html={technology?.text4} />
                                )}
                            </Typography>
                        </Grid>
                    </Grid>
                </Box>
            </Container>
        </section>
    );
};
