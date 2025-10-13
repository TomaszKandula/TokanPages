export type THeaders = "h1" | "h2" | "h3" | "h4" | "h5" | "h6";
export type TComponent = THeaders | "p" | "span" | "div" | "ul" | "ol" | "br" | "blockquote";
export type TSeverity = "debug" | "info" | "warning" | "error" | "fatal";
export type TColour = "has-text-info" | "has-text-success" | "has-text-warning" | "has-text-danger";
export type TObjectFit = "fill" | "contain" | "cover" | "none" | "scale-down";

export type TInputColours =
    | "bulma-is-link"
    | "bulma-is-primary"
    | "bulma-is-info"
    | "bulma-is-success"
    | "bulma-is-warning"
    | "bulma-is-danger";
export type TInputSizes = "bulma-is-small" | "bulma-is-normal" | "bulma-is-medium" | "bulma-is-large";
export type TLoading = "lazy" | "eager";

export type ReactCSSProps = React.CSSProperties;
export type ReactSyntheticEvent = React.SyntheticEvent;

export type ReactMouseEvent = React.MouseEvent<HTMLElement, MouseEvent>;
export type ReactMouseEventButton = React.MouseEvent<HTMLButtonElement, MouseEvent>;
export type ReactMouseEventHandler = React.MouseEventHandler<HTMLButtonElement | HTMLAnchorElement>;
export type ReactMouseDivEventHandler = React.MouseEventHandler<HTMLDivElement>;

export type ReactKeyboardEvent = React.KeyboardEvent<HTMLInputElement>;
export type ReactChangeEvent = React.ChangeEvent<HTMLInputElement>;

export type ReactKeyboardTextEvent = React.KeyboardEvent<HTMLTextAreaElement>;
export type ReactChangeTextEvent = React.ChangeEvent<HTMLTextAreaElement>;

export interface ViewProperties {
    isLoading: boolean;
}
