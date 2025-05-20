import * as React from "react";
import { Link as RouterLink } from "react-router-dom";
import { ReactMouseEventHandler } from "../../../Shared/types";
import "./link.css";

interface LinkProps {
    to: string;
    className?: string;
    isDisabled?: boolean;
    onMouseEnter?: ReactMouseEventHandler;
    onMouseLeave?: ReactMouseEventHandler;
    children: React.ReactElement | React.ReactElement[];
}

export const Link = (props: LinkProps): React.ReactElement => {
    if (props.isDisabled === true) {
        const className = `${props.className} link-disabled`;
        return <div className={className}>{props.children}</div>;
    }

    const link: string = props.to;
    const isHref: boolean = link.includes("http://") || link.includes("https://");

    if (isHref) {
        return (
            <a 
                href={link} 
                className={`href ${props.className}`} 
                target="_blank" 
                rel="noopener nofollow"
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
            className={props.className} 
            onMouseEnter={props.onMouseEnter}
            onMouseLeave={props.onMouseLeave}
        >
            {props.children}
        </RouterLink>
    );
};
