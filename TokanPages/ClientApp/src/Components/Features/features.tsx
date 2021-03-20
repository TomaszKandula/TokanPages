import * as React from "react";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import Grid from "@material-ui/core/Grid";
import CodeIcon from "@material-ui/icons/Code";
import LibraryBooksIcon from "@material-ui/icons/LibraryBooks";
import StorageIcon from "@material-ui/icons/Storage";
import CloudIcon from "@material-ui/icons/Cloud";
import ReactHtmlParser from 'react-html-parser';
import { IFeatures } from "../../Api/Models";
import useStyles from "./styleFeatures";

export default function Features(props: { features: IFeatures, isLoading: boolean })
{
    const classes = useStyles();
    return (
        <section className={classes.section}>
            <Container maxWidth="lg">
                <div data-aos="fade-up">
                    <Box py={8}>
                        <Box mb={8}>
                            <Typography color="primary" variant="button" component="h3" align="center" gutterBottom={true}>
                                {props.features.content.caption}
                            </Typography>
                            <Typography variant="h4" component="h2" align="center">
                                {props.features.content.header}
                            </Typography>
                        </Box>
                        <Grid container spacing={6}>
                            <Grid item xs={12} sm={6}>
                                <Box mb={2} display="flex" alignItems="center">
                                    <CodeIcon color="primary" className={classes.icon} />
                                    <Typography variant="h5" component="h3">
                                        {props.features.content.title1}
                                    </Typography>
                                </Box>
                                <Typography variant="body1" component="p" color="textSecondary">
                                    {ReactHtmlParser(props.features.content.text1)}
                                </Typography>
                            </Grid>
                            <Grid item xs={12} sm={6}>
                                <Box mb={2} display="flex" alignItems="center">
                                    <LibraryBooksIcon color="primary" className={classes.icon} />
                                    <Typography variant="h5" component="h3">
                                        {props.features.content.title2}
                                    </Typography>
                                </Box>
                                <Typography variant="body1" component="p" color="textSecondary">
                                    {ReactHtmlParser(props.features.content.text2)}
                                </Typography>
                            </Grid>
                            <Grid item xs={12} sm={6}>
                                <Box mb={2} display="flex" alignItems="center">
                                    <StorageIcon color="primary" className={classes.icon} />
                                    <Typography variant="h5" component="h3">
                                        {props.features.content.title3}
                                    </Typography>
                                </Box>
                                <Typography variant="body1" component="p" color="textSecondary">
                                    {ReactHtmlParser(props.features.content.text3)}
                                </Typography>
                            </Grid>
                            <Grid item xs={12} sm={6}>         
                                <Box mb={2} display="flex" alignItems="center">
                                    <CloudIcon color="primary" className={classes.icon} />
                                    <Typography variant="h5" component="h3">
                                        {props.features.content.title4}
                                    </Typography>
                                </Box>
                                <Typography variant="body1" component="p" color="textSecondary">
                                    {ReactHtmlParser(props.features.content.text4)}
                                </Typography>
                            </Grid>
                        </Grid>
                    </Box>
                </div>
            </Container>
        </section>
    );
}