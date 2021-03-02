import * as React from "react";
import { Typography } from "@material-ui/core";
import { ITextItem } from "../Model/textModel";
import ReactHtmlParser from 'react-html-parser';
import useStyles from "../Hooks/styleRenderText";

export function RenderText(props: ITextItem)
{
    const classes = useStyles();
    const data: string = props.value as string; 
    return(
        <Typography variant="body1" component="span" className={classes.typography}>
            {ReactHtmlParser(data)}
        </Typography>
    );
}
