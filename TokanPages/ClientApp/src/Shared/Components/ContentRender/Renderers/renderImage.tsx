import * as React from "react";
import { Card, CardContent, CardMedia, Typography } from "@material-ui/core";
import { ITextItem } from "../Models/textModel";
import useStyles from "../Styles/renderImageStyle";
import Validate from "validate.js";

export function RenderImage(props: ITextItem)
{
    const classes = useStyles();
    const data: string = props.value as string; 
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
            {Validate.isEmpty(props.prop) 
                ? <CardMedia component="img" image={data} alt="image" /> 
                : <CardMedia component="img" image={data} alt="image" className={classes.image} onClick={() => { window.open(props.prop, "_blank") }}/> }
            {Validate.isEmpty(props.text) 
                ? null 
                : renderDescription()}
        </Card>
    );
}
