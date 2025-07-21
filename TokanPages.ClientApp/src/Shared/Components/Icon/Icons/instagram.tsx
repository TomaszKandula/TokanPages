import React from "react";
import { instagramPath } from "../paths";
import { IconBaseProps } from "../types";

export const InstgramIcon = (props: IconBaseProps): React.ReactElement => (
    <svg
        width={`${props.size}rem`}
        height={`${props.size}rem`}
        viewBox="0 0 24 24"
        fill="none"
        xmlns="http://www.w3.org/2000/svg"
        onClick={props.onClick}
    >
        <path fillRule="evenodd" clipRule="evenodd" d={instagramPath} fill="#000000" />
    </svg>
);
