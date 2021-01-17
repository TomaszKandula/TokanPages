import * as React from "react";
import { Card, CardMedia } from "@material-ui/core";
import { ITextItem } from "../Model/textModel";

export function RenderImage(props: ITextItem)
{
    return(
        <div key={props.id}>
            <Card elevation={2}>
                <CardMedia component="img" image={props.value} alt="" />
            </Card>
        </div>
    );
}
