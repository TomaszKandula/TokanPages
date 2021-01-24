import * as React from "react";
import { Card, CardMedia } from "@material-ui/core";
import { ITextItem } from "../Model/textModel";
import useStyles from "../Hooks/styleRenderVideo";

export function RenderVideo(props: ITextItem)
{
    const classes = useStyles();
    const [ImageState, setImageState] = React.useState(true);
    const imageClick = () => 
    {
        setImageState(false);
    }
    return(
        <Card elevation={3} classes={{ root: classes.card }}>
            {ImageState 
                ? <CardMedia component="img" image={props.prop} onClick={imageClick} className={classes.image} /> 
                : <CardMedia component="video" src={props.value} controls />}
        </Card>
    );
}
