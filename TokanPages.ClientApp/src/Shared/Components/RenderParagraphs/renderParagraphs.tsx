import * as React from "react";
import { Skeleton } from "../Skeleton";
import { RenderParagraphsProps } from "./Types";
import { v4 as uuidv4 } from "uuid";

export const RenderParagraphs = (props: RenderParagraphsProps): React.ReactElement => {
    if (props.replace) {
        const render: React.ReactElement[] = [];
        props.text.forEach(item => {
            if (item === props.replace?.key) {
                render.push(
                    <Skeleton key={uuidv4()} isLoading={props.isLoading ?? false}>
                        <div className={props.className}>{props.replace.object}</div>
                    </Skeleton>
                );
            } else {
                render.push(
                    <Skeleton key={uuidv4()} isLoading={props.isLoading ?? false}>
                        <p className={props.className}>{item}</p>
                    </Skeleton>
                );
            }
        });

        return <>{render}</>;
    }

    const result = props.text.map((value: string, _index: number) => (
        <Skeleton key={uuidv4()} isLoading={props.isLoading ?? false}>
            <p className={props.className}>{value}</p>
        </Skeleton>
    ));

    return <>{result}</>;
};
