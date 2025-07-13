import * as React from "react";
import { v4 as uuidv4 } from "uuid";

interface ReplaceProps {
    key: string;
    object: React.ReactElement;
}

interface RenderParagraphsProps {
    text: string[];
    className?: string;
    replace?: ReplaceProps;
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
                    <p key={uuidv4()} className={props.className}>
                        {item}
                    </p>
                );
            }
        });

        return <>{render}</>;
    }

    const result = props.text.map((value: string, _index: number) => (
        <p key={uuidv4()} className={props.className}>
            {value}
        </p>
    ));

    return <>{result}</>;
};
