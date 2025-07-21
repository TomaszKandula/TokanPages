import React from "react";
import { gitHubPath } from "../paths";
import { IconBaseProps } from "../types";

export const GithubIcon = (props: IconBaseProps): React.ReactElement => (
    <svg
        width={`${props.size}rem`}
        height={`${props.size}rem`}
        viewBox="0 0 48 48"
        fill="none"
        xmlns="http://www.w3.org/2000/svg"
        onClick={props.onClick}
    >
        <circle cx="24" cy="24" r="20" fill="#181717" />
        <path d={gitHubPath} fill="white" />
    </svg>
);
