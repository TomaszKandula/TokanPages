import * as React from "react";
import { ProcessParagraphs } from "../RenderContent/Renderers";
import { Skeleton } from "../Skeleton";
import { RenderListProps } from "./Types";
import Validate from "validate.js";
import { v4 as uuidv4 } from "uuid";

const GetListItem = (props: RenderListProps): React.ReactElement => {
    const list = props.list.map((value: string, _index: number) => (
        <Skeleton key={uuidv4()} isLoading={props.isLoading ?? false} className="my-4">
            <ProcessParagraphs tag="li" html={value} />
        </Skeleton>
    ));

    return <>{list}</>;
};

export const RenderList = (props: RenderListProps): React.ReactElement => {
    if (!props.list) {
        return <></>;
    }

    const className = Validate.isEmpty(props.className) ? "list-box" : `list-box ${props.className}`;

    if (props.type === "ol") {
        return (
            <ol data-testid={props.dataTestId} className={className}>
                <GetListItem {...props} />
            </ol>
        );
    } else {
        return (
            <ul data-testid={props.dataTestId} className={className}>
                <GetListItem {...props} />
            </ul>
        );
    }
};
