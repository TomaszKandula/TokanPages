import * as React from "react";
import { useSelector } from "react-redux";
import { Link } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import Skeleton from "@material-ui/lab/Skeleton";
import Button from "@material-ui/core/Button";
import Grid from "@material-ui/core/Grid/Grid";
import { ApplicationState } from "../../../Store/Configuration";
import { ContentHeaderState } from "../../../Store/States";
import { GET_IMAGES_URL } from "../../../Api/Request";
import { RenderImage } from "../../../Shared/Components";
import { HeaderStyle } from "./headerStyle";
import Validate from "validate.js";

const ActiveButton = (props: ContentHeaderState): JSX.Element => {
    const classes = HeaderStyle();

    if (Validate.isEmpty(props?.content?.action?.href)) {
        return (
            <Button variant="contained" className={classes.action_button}>
                {props?.content?.action?.text}
            </Button>
        );
    }

    return (
        <Link to={props?.content?.action?.href} className={classes.action_link}>
            <Button variant="contained" className={classes.action_button}>
                {props?.content?.action?.text}
            </Button>
        </Link>
    );
};

export const HeaderView = (): JSX.Element => {
    const classes = HeaderStyle();
    const header = useSelector((state: ApplicationState) => state.contentHeader);

    return (
        <section className={classes.section}>
            <Container maxWidth="lg">
                <Grid container className={classes.top_margin}>
                    <Grid item xs={12} sm={6}>
                        <Box className={classes.image_box}>
                            {header?.isLoading ? (
                                <Skeleton variant="circle" className={classes.image_skeleton} />
                            ) : (
                                RenderImage(GET_IMAGES_URL, header?.content?.photo, classes.image)
                            )}
                        </Box>
                    </Grid>
                    <Grid item xs={12} sm={6}>
                        <Box className={classes.content_box}>
                            <Typography variant="h3" gutterBottom={true}>
                                {header?.isLoading ? <Skeleton variant="text" /> : header?.content?.caption}
                            </Typography>
                            <Typography variant="h6" className={classes.content_description}>
                                {header?.isLoading ? <Skeleton variant="text" /> : header?.content?.description}
                            </Typography>
                            <Box mt={4}>
                                {header?.isLoading ? (
                                    <Skeleton variant="rect" height="48px" />
                                ) : (
                                    <ActiveButton {...header} />
                                )}
                            </Box>
                        </Box>
                    </Grid>
                </Grid>
            </Container>
        </section>
    );
};
