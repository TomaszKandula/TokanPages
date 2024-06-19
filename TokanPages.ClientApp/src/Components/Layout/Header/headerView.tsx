import * as React from "react";
import { useSelector } from "react-redux";
import { Link } from "react-router-dom";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import Skeleton from "@material-ui/lab/Skeleton";
import Button from "@material-ui/core/Button";
import Grid from "@material-ui/core/Grid/Grid";
import { CardMedia } from "@material-ui/core";
import { API_BASE_URI, GET_IMAGES_URL } from "../../../Api/Request";
import { ApplicationState } from "../../../Store/Configuration";
import { ContentHeaderState } from "../../../Store/States";
import { ReactHtmlParser } from "../../../Shared/Services/Renderers";
import { HeaderStyle } from "./headerStyle";
import Validate from "validate.js";

const OpenLinkButton = (props: ContentHeaderState): JSX.Element => {
    const classes = HeaderStyle();
    const href = props?.content?.resume?.href;

    if (Validate.isEmpty(href)) {
        return <></>;
    }

    const url = API_BASE_URI + href;
    const onClickEvent = React.useCallback(() => {
        window.open(url, "_blank");
    }, [url]);

    return (
        <Button 
            variant="contained" 
            className={classes.resume_button}
            onClick={onClickEvent}
        >
            {props?.content?.resume?.text}
        </Button>
    );
}

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
    const imageUrl = (name: string) => {
        if (name === "") return " ";
        return `${GET_IMAGES_URL}/${name}`;
    };

    return (
        <section className={classes.section}>
            <Grid container spacing={3}>
                <Grid item xs={12} md={7}>
                    {header?.isLoading ? (
                        <Skeleton variant="rect" className={classes.image_skeleton} />
                    ) : (
                        <CardMedia
                            image={imageUrl(header?.content?.photo)}
                            component="img"
                            className={classes.image_card}
                        />
                    )}
                </Grid>
                <Grid item xs={12} md={5} className={classes.section_container}>
                    <Box className={classes.content_box}>
                        <Typography component="span" className={classes.content_caption}>
                            {header?.isLoading ? (
                                <Skeleton variant="text" />
                            ) : (
                                <ReactHtmlParser html={header?.content?.caption} />
                            )}
                        </Typography>
                        <Typography component="span" className={classes.content_subtitle}>
                            {header?.isLoading ? (
                                <Skeleton variant="text" />
                            ) : (
                                <ReactHtmlParser html={header?.content?.subtitle} />
                            )}
                        </Typography>
                        <Typography component="span" className={classes.content_description}>
                            {header?.isLoading ? (
                                <Skeleton variant="text" />
                            ) : (
                                <ReactHtmlParser html={header?.content?.description} />
                            )}
                        </Typography>
                        <Box mt={4}>
                            {header?.isLoading ? (
                                <Skeleton variant="rect" height="48px" />
                            ) : (
                                <>
                                    <ActiveButton {...header} />
                                    <OpenLinkButton {...header} />
                                </>
                            )}
                        </Box>
                    </Box>
                </Grid>
            </Grid>
        </section>
    );
};
