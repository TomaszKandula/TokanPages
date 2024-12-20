import * as React from "react";
import { TextItem } from "../../Models";
import { Card, CardMedia, Typography } from "@material-ui/core";
import { API_BASE_URI } from "../../../../../Api/Request";
import { ReactHtmlParser } from "../../../../../Shared/Services/Renderers";

const NO_CONTENT = "EMPTY_CONTENT_PROVIDED";

export const RenderSuperTitle = (props: TextItem): React.ReactElement => {
    let propUrl = props.propImg ?? "";
    if (!propUrl.includes("https://")) {
        propUrl = `${API_BASE_URI}${propUrl}`;
    }

    return (
        <div className="render-super-title-container">
            <div className="render-super-title-content-text">
                <div style={{ marginTop: 56, marginBottom: 0 }}>
                    <Typography
                        variant="body1"
                        component="span"
                        className="render-super-title-common render-super-title-title"
                    >
                        <ReactHtmlParser html={props.propTitle ?? NO_CONTENT} />
                    </Typography>
                </div>
                <div style={{ marginTop: 8, marginBottom: 40 }}>
                    <Typography
                        variant="body1"
                        component="span"
                        className="render-super-title-common render-super-title-sub-title"
                    >
                        <ReactHtmlParser html={props.propSubtitle ?? NO_CONTENT} />
                    </Typography>
                </div>
            </div>
            <Card elevation={0} className="render-super-title-card render-super-title-content-image">
                <CardMedia
                    component="img"
                    image={propUrl}
                    alt="image"
                    className="render-super-title-image"
                    style={{
                        width: props.constraint?.width,
                        height: props.constraint?.height,
                    }}
                />
            </Card>
        </div>
    );
};
