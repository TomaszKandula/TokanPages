import * as React from "react";
import { Card, CardMedia } from "@material-ui/core";
import { ITextItem } from "../Model/textModel";
import useStyles from "../Hooks/styleRenderImage";

export function RenderImage(props: ITextItem)
{
    const classes = useStyles();
    return(
        <Card elevation={3} classes={{ root: classes.card }}>
            <CardMedia component="img" image={props.value} alt="" />
        </Card>
    );
}
