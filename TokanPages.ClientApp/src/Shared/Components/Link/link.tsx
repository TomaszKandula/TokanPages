import * as React from "react";
import { Link as RouterLink } from "react-router-dom";
import { ReactMouseEventHandler } from "../../../Shared/Types";
import "./link.css";

interface LinkProps {
    to: string;
    className?: string;
    isDisabled?: boolean;
    rel?: string;
    onMouseEnter?: ReactMouseEventHandler;
    onMouseLeave?: ReactMouseEventHandler;
    children: React.ReactElement | React.ReactElement[];
}

export const Link = (props: LinkProps): React.ReactElement => {
    if (props.isDisabled === true) {
        const className = `${props.className ?? ""} link-disabled`;
        return <a className={className}>{props.children}</a>;
    }

    const link: string = props.to;
    const isHref: boolean = link?.includes("http://") || link?.includes("https://") || link?.includes("mailto:");

    if (isHref) {
        return (
            <a
                href={link}
                className={`href ${props.className ?? ""}`}
                target="_blank"
                rel={props.rel ?? "noopener nofollow"}
                onMouseEnter={props.onMouseEnter}
                onMouseLeave={props.onMouseLeave}
            >
                {props.children}
            </a>
        );
    }

    return (
        <RouterLink
            to={props.to}
            className={props.className ?? ""}
            onMouseEnter={props.onMouseEnter}
            onMouseLeave={props.onMouseLeave}
        >
            {props.children}
        </RouterLink>
    );
};
