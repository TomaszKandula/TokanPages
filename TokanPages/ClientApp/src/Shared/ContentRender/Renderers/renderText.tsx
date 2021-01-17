import * as React from "react";
import { Typography } from "@material-ui/core";
import { ITextItem } from "../Model/textModel";
import ReactHtmlParser from 'react-html-parser';

export function RenderText(props: ITextItem)
{
    return(
        <div key={props.id}>
            <Typography variant="body1" component="span">
                {ReactHtmlParser(props.value)}
            </Typography>
        </div>
    );
}
