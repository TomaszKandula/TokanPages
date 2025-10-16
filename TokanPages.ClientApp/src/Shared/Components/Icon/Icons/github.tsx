import React from "react";
import { gitHubPath } from "../paths";
import { IconBaseProps } from "../types";

export const GithubIcon = (props: IconBaseProps): React.ReactElement => (
    <svg
        viewBox="0 0 48 48"
        xmlns="http://www.w3.org/2000/svg"
        style={{ width: `${props.size}rem`, height: `${props.size}rem` }}
        className={props.className}
        onClick={props.onClick}
    >
        <circle cx="24" cy="24" r="20" style={{ fill: "currentcolor" }} />
        <path d={gitHubPath} fill="white" />
    </svg>
);
