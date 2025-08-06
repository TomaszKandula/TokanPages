import React from "react";
import { linkedInPath } from "../paths";
import { IconBaseProps } from "../types";

export const LinkedinIcon = (props: IconBaseProps): React.ReactElement => (
    <svg
        fill="#000000"
        viewBox="0 0 32 32"
        version="1.1"
        xmlns="http://www.w3.org/2000/svg"
        style={{ width: `${props.size}rem`, height: `${props.size}rem` }}
        className={props.className}
        onClick={props.onClick}
    >
        <path d={linkedInPath}></path>
    </svg>
);
