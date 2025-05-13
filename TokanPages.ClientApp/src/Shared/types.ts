export type THeaders = "h1" | "h2" | "h3" | "h4" | "h5" | "h6";
export type TComponent = THeaders | "p" | "span" | "div" | "ul" | "ol" | "br" | "blockquote";
export type TSeverity = "debug" | "info" | "warning" | "error" | "fatal";

export type ReactSyntheticEvent = React.SyntheticEvent;

export type ReactMouseEvent = React.MouseEvent<HTMLElement, MouseEvent>;
export type ReactMouseEventButton = React.MouseEvent<HTMLButtonElement, MouseEvent>;
export type ReactMouseEventHandler = React.MouseEventHandler<HTMLButtonElement>;

export type ReactKeyboardEvent = React.KeyboardEvent<HTMLInputElement>;
export type ReactKeyboardEventHandler = React.KeyboardEventHandler<HTMLInputElement | HTMLTextAreaElement>;

export type ReactChangeEvent = React.ChangeEvent<HTMLInputElement>;
export type ReactChangeEventHandler = React.ChangeEventHandler<HTMLInputElement | HTMLTextAreaElement>;
