import * as React from "react";
import { Card, CardContent, CardMedia, Typography } from "@material-ui/core";
import { ITextItem } from "../Model/textModel";
import useStyles from "../Hooks/styleRenderImage";
import Validate from "validate.js";

export function RenderImage(props: ITextItem)
{
    const classes = useStyles();
    const renderDescription = () => 
    {
        return(
            <CardContent>
                <Typography component="p" variant="body2" className={classes.text}>
                    {props.text}
               </Typography>
           </CardContent>
       );
    }

    return(
        <Card elevation={3} classes={{ root: classes.card }}>
            <CardMedia component="img" image={props.value} alt="" />
            {Validate.isEmpty(props.text) ? null : renderDescription()}
        </Card>
    );
}
