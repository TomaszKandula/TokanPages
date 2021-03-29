import * as React from "react";
import { Typography } from "@material-ui/core";
import ReactHtmlParser from "react-html-parser";
import { ITextItem } from "../Model/textModel";
import useStyles from "../Hooks/styleRenderText";

export function RenderText(props: ITextItem)
{
    const classes = useStyles();
    const data: string = props.value as string; 

    let itemStyle: string;
    switch(props.prop)
    {
        case "title":
            itemStyle = classes.title;
            break;
        case "subtitle":
            itemStyle = classes.subTitle;
            break;
        default:
            itemStyle = classes.paragraph;
    };

    return(
        <Typography variant="body1" component="span" className={`${classes.common} ${itemStyle}`}>
            {ReactHtmlParser(data)}
        </Typography>
    );
}
