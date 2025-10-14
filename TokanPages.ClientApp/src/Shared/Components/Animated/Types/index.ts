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

type FlipAnimations = "flip-up" | "flip-down" | "flip-left" | "flip-right";
type SlideAnimations = "slide-up" | "slide-down" | "slide-left" | "slide-right";

export type Animations = FadeAnimations | FlipAnimations | SlideAnimations | ZoomAnimations;

export interface AnimatedProps {
    isDisabled?: boolean;
    dataAos: Animations;
    dataAosDelay?: number;
    className?: string;
    children: React.ReactNode;
}
