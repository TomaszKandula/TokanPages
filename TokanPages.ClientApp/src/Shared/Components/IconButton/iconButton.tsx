import * as React from "react";
import { ReactMouseEvent } from "../../../Shared/types";
import "./iconButton.css";

interface IconButtonProps {
    size?: number;
    children: React.ReactElement;
    onClick?: (event: ReactMouseEvent) => void;
}

export const IconButton = (props: IconButtonProps): React.ReactElement => { 
    const size = props.size ?? 48;
    return (
        <button 
            className="icon-button" 
            onClick={props.onClick} 
            style={{ height: size, width: size }}
        >
            {props.children}
        </button>
    )
};
