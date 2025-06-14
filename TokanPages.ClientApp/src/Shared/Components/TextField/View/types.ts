import { 
    ReactChangeEvent,
    ReactKeyboardEvent,
    ReactMouseEvent,
    TInputColours,
    TInputSizes
} from "Shared/types";

export interface TextFieldProps {
    uuid: string;
    required?: boolean;
    fullWidth?: boolean;
    autoComplete?: string;
    autoFocus?: boolean;
    onKeyUp: (event: ReactKeyboardEvent) => void;
    onChange: (event: ReactChangeEvent) => void;
    value: string | number | readonly string[] | undefined;
    placeholder?: string;
    isDisabled?: boolean;
    colour?: TInputColours;
    size?: TInputSizes;
    isRounded?: boolean;
    isLoading?: boolean;
    isReadonly?: boolean;
    className?: string;
}

export interface TextFieldExtendedProps extends TextFieldProps {
    showPassword: boolean;
    iconOnClick: (event: ReactMouseEvent) => void;
    iconOnMouseDown: (event: ReactMouseEvent) => void;
}
