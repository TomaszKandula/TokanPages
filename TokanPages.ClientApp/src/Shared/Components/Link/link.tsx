import * as React from "react";
import { Link as RouterLink } from "react-router-dom";
import { ReactMouseEventHandler } from "../../../Shared/types";
import "./link.css";

interface LinkProps {
    key?: string | number | null | undefined;
    to: string;
    className?: string;
    onMouseEnter?: ReactMouseEventHandler;
    isDisabled: boolean;
    children: React.ReactElement | React.ReactElement[];
}

export const Link = (props: LinkProps): React.ReactElement => {
    const className = `${props.className} link-disabled`;

    if (props.isDisabled) {
        return (<div className={className}>{props.children}</div>);
    }    

    return (
        <RouterLink
            key={props.key}
            to={props.to}
            className={className}
            onMouseEnter={props.onMouseEnter}
        >
            {props.children}
        </RouterLink>
    );
}
