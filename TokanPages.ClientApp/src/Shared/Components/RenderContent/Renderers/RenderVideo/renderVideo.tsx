import * as React from "react";
import { Card, CardContent, CardMedia, Typography } from "@material-ui/core";
import { TextItem } from "../../Models/TextModel";
import { API_BASE_URI } from "../../../../../Api/Request";
import Validate from "validate.js";

const RenderDescription = (props: { text: string }): React.ReactElement => {
    return (
        <CardContent>
            <Typography component="span" className="render-video-text">
                {props.text}
            </Typography>
        </CardContent>
    );
};

export const RenderVideo = (props: TextItem): React.ReactElement => {
    let valueUrl = props.value as string;
    if (!valueUrl.includes("https://")) {
        valueUrl = `${API_BASE_URI}${valueUrl}`;
    }

    let propUrl = props.prop;
    if (!propUrl.includes("https://")) {
        propUrl = `${API_BASE_URI}${propUrl}`;
    }

    const [hasImage, setHasImage] = React.useState(true);
    const onClickEvent = React.useCallback(() => setHasImage(false), []);

    return (
        <Card elevation={3} className="render-video-card">
            {hasImage ? (
                <CardMedia
                    component="img"
                    loading="lazy"
                    image={propUrl}
                    onClick={onClickEvent}
                    className="render-video-image lazyloaded"
                    title="Video"
                    alt="Video related to the presented article text"
                    style={{
                        width: props.constraint?.width,
                        height: props.constraint?.height,
                    }}
                />
            ) : (
                <CardMedia component="video" src={valueUrl} controls autoPlay />
            )}
            {Validate.isEmpty(props.text) ? null : <RenderDescription text={props.text} />}
        </Card>
    );
};
