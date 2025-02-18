import * as React from "react";
import { Card, CardContent, CardMedia, Typography } from "@material-ui/core";
import { API_BASE_URI } from "../../../../../Api/Request";
import { ReactHtmlParser } from "../../../../../Shared/Services/Renderers";
import { TextItem } from "../../Models/TextModel";
import Validate from "validate.js";

const RenderDescription = (props: { text: string }): React.ReactElement => {
    return (
        <CardContent>
            <Typography component="span" className="render-image-text">
                <ReactHtmlParser html={props.text} />
            </Typography>
        </CardContent>
    );
};

export const RenderImage = (props: TextItem): React.ReactElement => {
    const hasProp = !Validate.isEmpty(props.prop);
    const hasValue = !Validate.isEmpty(props.value);
    const hasText = !Validate.isEmpty(props.text);
    const hasPropAndValue = hasProp && hasValue;
    const hasValueOnly = !hasProp && hasValue;

    let valueUrl = props.value as string;
    if (!valueUrl.includes("https://")) {
        valueUrl = `${API_BASE_URI}${valueUrl}`;
    }

    let propUrl = props.prop;
    if (!propUrl.includes("https://")) {
        propUrl = `${API_BASE_URI}${propUrl}`;
    }

    const onClickEvent = React.useCallback(() => {
        window.open(valueUrl, "_blank");
    }, [valueUrl]);

    return (
        <Card elevation={3} className="render-image-card">
            {hasPropAndValue ? (
                <CardMedia
                    component="img"
                    image={propUrl}
                    alt="An image of presented article text"
                    className="render-image-image"
                    onClick={onClickEvent}
                    style={{
                        width: props.constraint?.width,
                        height: props.constraint?.height,
                    }}
                />
            ) : null}
            {hasValueOnly ? (
                <CardMedia
                    component="img"
                    image={valueUrl}
                    alt="An image of presented article text"
                    style={{
                        width: props.constraint?.width,
                        height: props.constraint?.height,
                    }}
                />
            ) : null}
            {hasText ? <RenderDescription text={props.text} /> : null}
        </Card>
    );
};
