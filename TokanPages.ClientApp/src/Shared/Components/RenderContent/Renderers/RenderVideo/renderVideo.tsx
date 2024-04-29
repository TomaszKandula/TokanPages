import * as React from "react";
import { Card, CardContent, CardMedia, Typography } from "@material-ui/core";
import { TextItem } from "../../Models/TextModel";
import { RenderVideoStyle } from "./renderVideoStyle";
import { API_BASE_URI } from "../../../../../Api/Request";
import Validate from "validate.js";

const RenderDescription = (props: { text: string }): JSX.Element => {
    const classes = RenderVideoStyle();
    return (
        <CardContent>
            <Typography component="p" variant="body2" className={classes.text}>
                {props.text}
            </Typography>
        </CardContent>
    );
};

export const RenderVideo = (props: TextItem): JSX.Element => {
    const classes = RenderVideoStyle();

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
        <Card elevation={3} classes={{ root: classes.card }}>
            {hasImage ? (
                <CardMedia component="img" image={propUrl} onClick={onClickEvent} className={classes.image} />
            ) : (
                <CardMedia component="video" src={valueUrl} controls autoPlay />
            )}
            {Validate.isEmpty(props.text) ? null : <RenderDescription text={props.text} />}
        </Card>
    );
};
