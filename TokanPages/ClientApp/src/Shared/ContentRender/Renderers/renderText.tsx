import * as React from "react";
import { Typography } from "@material-ui/core";
import { ITextItem } from "../Model/textModel";
import ReactHtmlParser from 'react-html-parser';

export function RenderText(props: ITextItem, textStyleName: string)
{
    return(
        <div key={props.id}>
            <Typography variant="body1" component="span" className={textStyleName}>
                {ReactHtmlParser(props.value)}
            </Typography>
        </div>
    );
}
