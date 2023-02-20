export type LanguageChangeEvent = React.ChangeEvent<{ name?: string; value: unknown }>;
export type ReactSyntheticEvent = React.SyntheticEvent;

export type ReactMouseEvent = React.MouseEvent<HTMLElement, MouseEvent>;
export type ReactMouseEventHandler = React.MouseEventHandler<HTMLButtonElement>;

export type ReactKeyboardEvent = React.KeyboardEvent<HTMLInputElement>;
export type ReactKeyboardEventHandler = React.KeyboardEventHandler<HTMLInputElement | HTMLTextAreaElement>;

export type ReactChangeEvent = React.ChangeEvent<HTMLInputElement>;
export type ReactChangeEventHandler = React.ChangeEventHandler<HTMLInputElement | HTMLTextAreaElement>;
