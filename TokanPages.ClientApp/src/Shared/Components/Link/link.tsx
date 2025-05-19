import * as React from "react";
import { Link as RouterLink } from "react-router-dom";
import { ReactMouseEventHandler } from "../../../Shared/types";
import "./link.css";

interface LinkProps {
    key?: string | number | null | undefined;
    to: string;
    className?: string;
    onMouseEnter?: ReactMouseEventHandler;
    isDisabled?: boolean;
    children: React.ReactElement | React.ReactElement[];
}

export const Link = (props: LinkProps): React.ReactElement => {
    if (props.isDisabled === true) {
        const className = `${props.className} link-disabled`;
        return <div className={className}>{props.children}</div>;
    }

    return (
        <RouterLink key={props.key} to={props.to} className={props.className} onMouseEnter={props.onMouseEnter}>
            {props.children}
        </RouterLink>
    );
};
