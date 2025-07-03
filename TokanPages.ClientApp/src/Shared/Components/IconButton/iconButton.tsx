import * as React from "react";
import { ReactMouseEvent } from "../../../Shared/types";
import "./iconButton.css";

interface IconButtonProps {
    size?: number;
    children: React.ReactElement | React.ReactElement[];
    hasNoHoverEffect?: boolean;
    className?: string;
    onClick?: (event: ReactMouseEvent) => void;
    onMouseDown?: (event: ReactMouseEvent) => void;
}

export const IconButton = (props: IconButtonProps): React.ReactElement => {
    const size = props.size ?? 48;
    const baseClassName = props.hasNoHoverEffect ? "icon-button-no-hover" : "icon-button";
    const className = `${baseClassName} ${props.className ?? ""}`;

    return (
        <button 
            className={className}
            onClick={props.onClick}
            onMouseDown={props.onMouseDown}
            style={{ height: size, width: size }}
        >
            {props.children}
        </button>
    );
};
