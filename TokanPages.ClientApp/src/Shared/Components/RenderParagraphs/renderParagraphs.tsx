import * as React from "react";
import { Typography } from "@material-ui/core";

interface RenderParagraphsProps {
    text: string[];
    className?: string | undefined;
}

export const RenderParagraphs = (props: RenderParagraphsProps): React.ReactElement => {
    const result = props.text.map((value: string, index: number) => (
        <Typography key={index} component="p" className={props.className}>
            {value}
        </Typography>
    ));

    return <>{result}</>;
}
