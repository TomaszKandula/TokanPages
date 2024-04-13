import * as React from "react";
import { Card, CardContent, CardMedia, Typography } from "@material-ui/core";
import { API_BASE_URI } from "../../../../../Api/Request";
import { TextItem } from "../../Models/TextModel";
import { RenderImageStyle } from "./renderImageStyle";
import Validate from "validate.js";

export const RenderImage = (props: TextItem): JSX.Element => {
    const classes = RenderImageStyle();

    let url = props.value as string;
    if (!url.includes("https://")) {
        url = `${API_BASE_URI}${url}`;
    }

    const onClickEvent = React.useCallback(() => {
        window.open(props.prop, "_blank");
    }, [props.prop]);

    const renderDescription = () => {
        return (
            <CardContent>
                <Typography component="p" variant="body2" className={classes.text}>
                    {props.text}
                </Typography>
            </CardContent>
        );
    };

    return (
        <Card elevation={3} classes={{ root: classes.card }}>
            {Validate.isEmpty(props.prop) ? (
                <CardMedia component="img" image={url} alt="image" />
            ) : (
                <CardMedia component="img" image={url} alt="image" className={classes.image} onClick={onClickEvent} />
            )}
            {Validate.isEmpty(props.text) ? null : renderDescription()}
        </Card>
    );
};
