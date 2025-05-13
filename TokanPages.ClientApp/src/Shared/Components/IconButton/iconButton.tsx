import * as React from "react";
import { ReactMouseEvent } from "../../../Shared/types";
import "./iconButton.css";

interface IconButtonProps {
    size?: number;
    children: React.ReactElement;
    onClick?: (event: ReactMouseEvent) => void;
}

export const IconButton = (props: IconButtonProps): React.ReactElement => (
    <button className="icon-button" onClick={props.onClick} style={{ height: props.size ?? 48 }}>
        {props.children}
    </button>
);
