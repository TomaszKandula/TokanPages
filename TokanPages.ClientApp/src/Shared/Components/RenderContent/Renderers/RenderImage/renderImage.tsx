import * as React from "react";
import { Card, CardContent, CardMedia, Typography } from "@material-ui/core";
import { ITextItem } from "../../Models/TextModel";
import { RenderImageStyle } from "./renderImageStyle";
import Validate from "validate.js";

export const RenderImage = (props: ITextItem): JSX.Element =>
{
    const classes = RenderImageStyle();
    const data: string = props.value as string; 

    const onClickEvent = React.useCallback(() => 
    { 
        window.open(props.prop, "_blank") 
    }, [props.prop]);

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
                : <CardMedia component="img" image={data} alt="image" className={classes.image} onClick={onClickEvent}/> }
            {Validate.isEmpty(props.text) 
                ? null 
                : renderDescription()}
        </Card>
    );
}
