import * as React from "react";
import "./skeleton.css";

type Mode = "Text" | "Rect" | "Circle";

interface SkeletonProps {
    isLoading: boolean;
    mode?: Mode;
    width?: number;
    height?: number;
    children: React.ReactNode | React.ReactElement | React.ReactElement[];
}

export const Skeleton = (props: SkeletonProps): React.ReactElement => {
    const styleProps: React.CSSProperties = { 
        width: props.width, height: props.height 
    };

    if (props.isLoading) {
        switch (props.mode) {
            case "Circle": return <div className="base skeleton-circle my-2" style={styleProps}></div>;
            case "Rect": return <div className="base skeleton-rect my-2" style={styleProps}></div>;
            case "Text": return <div className="base skeleton-text my-2" style={styleProps}></div>;
            default: return <div className="base skeleton my-2" style={styleProps}></div>;
        }
    }

    return <>{props.children}</>;
};
