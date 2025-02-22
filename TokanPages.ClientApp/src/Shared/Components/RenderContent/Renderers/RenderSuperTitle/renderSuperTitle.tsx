import * as React from "react";
import { TextItem } from "../../Models";
import { Card, CardMedia } from "@material-ui/core";
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
                <ReactHtmlParser 
                    html={props.propTitle ?? NO_CONTENT} 
                    component="p" 
                    className="render-super-title-common render-super-title-title mt-40 mb-8" 
                />
                <ReactHtmlParser 
                    html={props.propSubtitle ?? NO_CONTENT} 
                    component="p"
                    className="render-super-title-common render-super-title-sub-title mt-8 mb-40"
                />
            </div>
            <Card elevation={0} className="render-super-title-card render-super-title-content-image">
                <CardMedia
                    component="img"
                    loading="lazy"
                    image={propUrl}
                    title="Illustration"
                    alt="An illustration of a presented article text title"
                    className="render-super-title-image lazyloaded"
                    style={{
                        width: props.constraint?.width,
                        height: props.constraint?.height,
                    }}
                />
            </Card>
        </div>
    );
};
