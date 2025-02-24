import * as React from "react";
import Validate from "validate.js";

type TList = "ul" | "ol";

interface RenderListProps {
    list: string[];
    type?: TList;
    className?: string;
}

const GetListItem = (props: RenderListProps): React.ReactElement => {
    const list = props.list.map((value: string, index: number) => (
        <li key={index}>
            {value}
        </li>
    ));

    return <>{list}</>;
}

export const RenderList = (props: RenderListProps): React.ReactElement => {
    const className = Validate.isEmpty(props.className) 
    ? "list-box" 
    : `list-box ${props.className}`;

    switch(props.type) {
        case "ul": return <ul className={className}><GetListItem {...props} /></ul>;
        case "ol": return <ol className={className}><GetListItem {...props} /></ol>;
        default: return <ul className={className}><GetListItem {...props} /></ul>;
    }
}
