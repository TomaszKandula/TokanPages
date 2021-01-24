import * as React from "react";
import { Card, CardContent, CardMedia, Typography } from "@material-ui/core";
import { ITextItem } from "../Model/textModel";
import useStyles from "../Hooks/styleRenderImage";
import Validate from "validate.js";

export function RenderImage(props: ITextItem)
{
    const classes = useStyles();

    return(
        <Card elevation={3} classes={{ root: classes.card }}>
            <CardMedia component="img" image={props.value} alt="" />
            <CardContent>
                <Typography component="p" variant="body2" className={classes.text}>
                    {Validate.isEmpty(props.text) ? null : props.text}
                </Typography>
            </CardContent>
        </Card>
    );

}
