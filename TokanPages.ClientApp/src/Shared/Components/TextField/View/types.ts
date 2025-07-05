import {
    ReactChangeEvent,
    ReactChangeTextEvent,
    ReactKeyboardEvent,
    ReactKeyboardTextEvent,
    ReactMouseEvent,
    TInputColours,
    TInputSizes
} from "../../../../Shared/types";

interface BaseProps {
    uuid: string;
    required?: boolean;
    maxLength?: number;
    autoComplete?: string;
    autoFocus?: boolean;
    value: string | number | readonly string[] | undefined;
    placeholder?: string;
    colour?: TInputColours;
    size?: TInputSizes;
    isDisabled?: boolean;
    isRounded?: boolean;
    isLoading?: boolean;
    isReadonly?: boolean;
    className?: string;
}

export interface TextFieldProps extends BaseProps {
    onKeyUp?: (event: ReactKeyboardEvent) => void;
    onChange?: (event: ReactChangeEvent) => void;
}

export interface TextFieldExtendedProps extends TextFieldProps {
    showPassword: boolean;
    iconOnClick: (event: ReactMouseEvent) => void;
    iconOnMouseDown: (event: ReactMouseEvent) => void;
}

export interface TextAreaProps extends BaseProps {
    rows?: number;
    isFixedSize?: boolean;
    onKeyUp?: (event: ReactKeyboardTextEvent) => void;
    onChange?: (event: ReactChangeTextEvent) => void;
}
