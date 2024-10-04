import * as React from "react";
import { TextItem } from "../../Models";
import { Box, Card, CardMedia, Typography } from "@material-ui/core";
import { RenderSuperTitleStyle } from "./renderSuperTitleStyle";
import { ReactHtmlParser } from "Shared/Services/Renderers";
import { API_BASE_URI } from "Api/Request";

const NO_CONTENT = "EMPTY_CONTENT_PROVIDED";

export const RenderSuperTitle = (props: TextItem): React.ReactElement => {
    const classes = RenderSuperTitleStyle();

    let propUrl = props.propImg ?? "";
    if (!propUrl.includes("https://")) {
        propUrl = `${API_BASE_URI}${propUrl}`;
    }

    return (
        <Box className={classes.container}>
            <Box className={classes.contentText}>
                <Box mt={7} mb={0}>
                    <Typography variant="body1" component="span" className={`${classes.common} ${classes.title}`}>
                        <ReactHtmlParser html={props.propTitle ?? NO_CONTENT} />
                    </Typography>
                </Box>
                <Box mt={1} mb={5}>
                    <Typography variant="body1" component="span" className={`${classes.common} ${classes.subTitle}`}>
                        <ReactHtmlParser html={props.propSubtitle ?? NO_CONTENT} />
                    </Typography>
                </Box>
            </Box>
            <Card elevation={0} classes={{ root: classes.card }} className={classes.contentImage}>
                <CardMedia component="img" image={propUrl} alt="image" className={classes.image} />
            </Card>
        </Box>
    );
};
