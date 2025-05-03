import * as React from "react";
import { Typography } from "@material-ui/core";
import { v4 as uuidv4 } from "uuid";

interface ReplaceProps {
    key: string;
    object: React.ReactElement;
}

interface RenderParagraphsProps {
    text: string[];
    className?: string | undefined;
    replace?: ReplaceProps | undefined;
}

export const RenderParagraphs = (props: RenderParagraphsProps): React.ReactElement => {
    if (props.replace) {
        const render: React.ReactElement[] = [];
        props.text.forEach(item => {
            if (item === props.replace?.key) {
                render.push(
                    <div key={uuidv4()} className={props.className}>
                        {props.replace.object}
                    </div>
                );
            } else {
                render.push(
                    <Typography key={uuidv4()} component="p" className={props.className}>
                        {item}
                    </Typography>
                );
            }
        });

        return <>{render}</>;
    }

    const result = props.text.map((value: string, index: number) => (
        <Typography key={index} component="p" className={props.className}>
            {value}
        </Typography>
    ));

    return <>{result}</>;
};
