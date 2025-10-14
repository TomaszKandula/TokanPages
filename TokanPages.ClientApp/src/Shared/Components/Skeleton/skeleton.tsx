import * as React from "react";
import { SkeletonProps } from "./Types";
import "./skeleton.css";

export const Skeleton = (props: SkeletonProps): React.ReactElement => {
    const marginClass = props.disableMarginY ? "" : "my-2";
    const baseClass = `base ${props.hasSkeletonCentered ? "centered" : ""} skeleton`;
    const className = props.className ?? "";
    const styleProps: React.CSSProperties = {
        width: props.width,
        height: props.height,
    };

    if (props.isLoading) {
        switch (props.mode) {
            case "Circle":
                return <div className={`${baseClass}-circle ${marginClass} ${className}`} style={styleProps}></div>;
            case "Rect":
                return <div className={`${baseClass}-rect ${marginClass} ${className}`} style={styleProps}></div>;
            case "Text":
                return <div className={`${baseClass}-text ${marginClass} ${className}`} style={styleProps}></div>;
            default:
                return <div className={`${baseClass} ${marginClass} ${className}`} style={styleProps}></div>;
        }
    }

    return <>{props.children}</>;
};
