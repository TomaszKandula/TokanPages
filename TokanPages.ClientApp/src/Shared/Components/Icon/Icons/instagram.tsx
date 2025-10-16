import React from "react";
import { instagramPath } from "../paths";
import { IconBaseProps } from "../types";

export const InstgramIcon = (props: IconBaseProps): React.ReactElement => (
    <svg
        viewBox="0 0 24 24"
        xmlns="http://www.w3.org/2000/svg"
        style={{ width: `${props.size}rem`, height: `${props.size}rem` }}
        className={props.className}
        onClick={props.onClick}
    >
        <path fillRule="evenodd" clipRule="evenodd" d={instagramPath} style={{ fill: "currentcolor" }} />
    </svg>
);
