import * as React from "react";
import { Card, CardContent, CardMedia, Typography } from "@material-ui/core";
import { API_BASE_URI } from "../../../../../Api/Request";
import { TextItem } from "../../Models/TextModel";
import { RenderImageStyle } from "./renderImageStyle";
import Validate from "validate.js";

const RenderDescription = (props: { text: string }): JSX.Element => {
    const classes = RenderImageStyle();
    return (
        <CardContent>
            <Typography component="p" variant="body2" className={classes.text}>
                {props.text}
            </Typography>
        </CardContent>
    );
};

export const RenderImage = (props: TextItem): JSX.Element => {
    const classes = RenderImageStyle();

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
        <Card elevation={3} classes={{ root: classes.card }}>
            {hasPropAndValue ? (
                <CardMedia
                    component="img"
                    image={propUrl}
                    alt="image"
                    className={classes.image}
                    onClick={onClickEvent}
                />
            ) : null}
            {hasValueOnly ? <CardMedia component="img" image={valueUrl} alt="image" /> : null}
            {hasText ? <RenderDescription text={props.text} /> : null}
        </Card>
    );
};
