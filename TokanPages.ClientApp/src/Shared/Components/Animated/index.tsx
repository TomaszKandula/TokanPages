import * as React from "react";
import { HasSnapshotMode } from "../../../Shared/Services/SpaCaching";
import { Animations } from "./types";

interface AnimatedProps {
    isDisabled?: boolean;
    dataAos: Animations;
    dataAosDelay?: number;
    style?: React.CSSProperties;
    children: React.ReactNode;
}

export const Animated = (props: AnimatedProps): React.ReactElement => {
    const hasSnapshot = HasSnapshotMode();
    if (hasSnapshot) {
        return <div style={props.style}>{props.children}</div>;
    }

    if (props.isDisabled) {
        return <div style={props.style}>{props.children}</div>;
    }

    return (
        <div style={props.style} data-aos={props.dataAos} data-aos-delay={props.dataAosDelay ?? 0}>
            {props.children}
        </div>
    );
};
