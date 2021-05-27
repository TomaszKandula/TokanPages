import * as React from "react";
import { Card, CardContent, CardMedia, Typography } from "@material-ui/core";
import { ITextItem } from "../Models/textModel";
import useStyles from "../Styles/renderVideoStyle";
import Validate from "validate.js";

export function RenderVideo(props: ITextItem)
{
    const classes = useStyles();
    const data: string = props.value as string; 
    const [ImageState, setImageState] = React.useState(true);
    const imageClick = () => 
    {
        setImageState(false);
    };

    const renderDescription = () => 
    {
        return(
            <CardContent>
                <Typography component="p" variant="body2" className={classes.text}>
                    {props.text}
               </Typography>
           </CardContent>
       );
    };

    return(
        <Card elevation={3} classes={{ root: classes.card }}>
            {ImageState 
                ? <CardMedia component="img" image={props.prop} onClick={imageClick} className={classes.image} /> 
                : <CardMedia component="video" src={data} controls autoPlay />}
            {Validate.isEmpty(props.text) 
                ? null 
                : renderDescription()}
       </Card>
    );
}
