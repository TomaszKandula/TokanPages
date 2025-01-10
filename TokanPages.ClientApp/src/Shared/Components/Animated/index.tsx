import * as React from "react";
import { HasSnapshotMode } from "../../../Shared/Services/SpaCaching";

type FadeAnimations =
    | "fade-in"
    | "fade-up"
    | "fade-down"
    | "fade-left"
    | "fade-right"
    | "fade-up-right"
    | "fade-up-left"
    | "fade-down-right"
    | "fade-down-left";
type FlipAnimations = "flip-up" | "flip-down" | "flip-left" | "flip-right";
type SlideAnimations = "slide-up" | "slide-down" | "slide-left" | "slide-right";
type ZoomAnimations =
    | "zoom-in"
    | "zoom-in-up"
    | "zoom-in-down"
    | "zoom-in-left"
    | "zoom-in-right"
    | "zoom-out"
    | "zoom-out-up"
    | "zoom-out-down"
    | "zoom-out-left"
    | "zoom-out-right";
type Animations = FadeAnimations | FlipAnimations | SlideAnimations | ZoomAnimations;

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
