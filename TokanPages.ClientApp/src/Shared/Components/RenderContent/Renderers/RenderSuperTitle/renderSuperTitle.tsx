import * as React from "react";
import { TextItem } from "../../Models";
import { Box, Card, CardMedia, Typography } from "@material-ui/core";
import { API_BASE_URI } from "../../../../../Api/Request";
import { ReactHtmlParser } from "../../../../../Shared/Services/Renderers";

const NO_CONTENT = "EMPTY_CONTENT_PROVIDED";

export const RenderSuperTitle = (props: TextItem): React.ReactElement => {
    let propUrl = props.propImg ?? "";
    if (!propUrl.includes("https://")) {
        propUrl = `${API_BASE_URI}${propUrl}`;
    }

    return (
        <Box className="render-super-title-container">
            <Box className="render-super-title-content-text">
                <Box mt={7} mb={0}>
                    <Typography variant="body1" component="span" className="render-super-title-common render-super-title-title">
                        <ReactHtmlParser html={props.propTitle ?? NO_CONTENT} />
                    </Typography>
                </Box>
                <Box mt={1} mb={5}>
                    <Typography variant="body1" component="span" className="render-super-title-common render-super-title-sub-title">
                        <ReactHtmlParser html={props.propSubtitle ?? NO_CONTENT} />
                    </Typography>
                </Box>
            </Box>
            <Card elevation={0} className="render-super-title-card render-super-title-content-image">
                <CardMedia component="img" image={propUrl} alt="image" className="render-super-title-image" />
            </Card>
        </Box>
    );
};
