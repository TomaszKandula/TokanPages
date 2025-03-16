import * as React from "react";
import Validate from "validate.js";
import { ProcessParagraphs } from "../RenderContent/Renderers";

type TList = "ul" | "ol";

interface RenderListProps {
    list: string[];
    type?: TList;
    className?: string;
    dataTestId?: string | undefined;
}

const GetListItem = (props: RenderListProps): React.ReactElement => {
    const list = props.list.map((value: string, index: number) => (
        <li key={index}>
            <ProcessParagraphs html={value} />
        </li>
    ));

    return <>{list}</>;
};

export const RenderList = (props: RenderListProps): React.ReactElement => {
    if (!props.list) {
        return <></>;
    }

    const className = Validate.isEmpty(props.className) ? "list-box" : `list-box ${props.className}`;

    switch (props.type) {
        case "ul":
            return (
                <ul data-testid={props.dataTestId} className={className}>
                    <GetListItem {...props} />
                </ul>
            );
        case "ol":
            return (
                <ol data-testid={props.dataTestId} className={className}>
                    <GetListItem {...props} />
                </ol>
            );
        default:
            return (
                <ul data-testid={props.dataTestId} className={className}>
                    <GetListItem {...props} />
                </ul>
            );
    }
};
