import * as React from "react";
import { useSelector } from "react-redux";
import { Link } from "react-router-dom";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import Skeleton from "@material-ui/lab/Skeleton";
import Button from "@material-ui/core/Button";
import Grid from "@material-ui/core/Grid/Grid";
import { CardMedia } from "@material-ui/core";
import { GET_IMAGES_URL } from "../../../Api/Request";
import { HeaderContentDto } from "../../../Api/Models";
import { ApplicationState } from "../../../Store/Configuration";
import { ReactHtmlParser } from "../../../Shared/Services/Renderers";
import { HeaderStyle } from "./headerStyle";
import Validate from "validate.js";

interface HeaderViewProps {
    background?: React.CSSProperties;
}

const OpenLinkButton = (props: HeaderContentDto): React.ReactElement => {
    const classes = HeaderStyle();

    return (
        <Link to={props?.resume?.href ?? ""} className={classes.action_link}>
            <Button variant="contained" className={classes.resume_button}>
                {props?.resume?.text}
            </Button>
        </Link>
    );
};

const ActiveButton = (props: HeaderContentDto): React.ReactElement => {
    const classes = HeaderStyle();

    if (Validate.isEmpty(props?.action?.href)) {
        return (
            <Button variant="contained" className={classes.action_button}>
                {props?.action?.text}
            </Button>
        );
    }

    return (
        <Link to={props?.action?.href ?? ""} className={classes.action_link}>
            <Button variant="contained" className={classes.action_button}>
                {props?.action?.text}
            </Button>
        </Link>
    );
};

export const HeaderView = (props: HeaderViewProps): React.ReactElement => {
    const classes = HeaderStyle();
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const header = data.components.header;
    const imageUrl = (name: string) => {
        if (name === "") return " ";
        return `${GET_IMAGES_URL}/${name}`;
    };

    return (
        <section className={classes.section} style={props.background}>
            <Grid container spacing={3}>
                <Grid item xs={12} md={7}>
                    {data?.isLoading ? (
                        <Skeleton variant="rect" className={classes.image_skeleton} />
                    ) : (
                        <CardMedia
                            image={imageUrl(header?.photo)}
                            component="img"
                            className={classes.image_card}
                            alt={`photo of ${header?.photo}`}
                        />
                    )}
                </Grid>
                <Grid item xs={12} md={5} className={classes.section_container}>
                    <Box className={classes.content_box}>
                        <Typography component="span" className={classes.content_caption}>
                            {data?.isLoading ? <Skeleton variant="text" /> : <ReactHtmlParser html={header?.caption} />}
                        </Typography>
                        <Typography component="span" className={classes.content_subtitle}>
                            {data?.isLoading ? (
                                <Skeleton variant="text" />
                            ) : (
                                <ReactHtmlParser html={header?.subtitle} />
                            )}
                        </Typography>
                        <Typography component="span" className={classes.content_description}>
                            {data?.isLoading ? (
                                <Skeleton variant="text" />
                            ) : (
                                <ReactHtmlParser html={header?.description} />
                            )}
                        </Typography>
                        <Box mt={4}>
                            {data?.isLoading ? (
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
