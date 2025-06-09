import * as React from "react";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../types";

type Colours = "bulma-is-link" | "bulma-is-primary" | "bulma-is-info" | "bulma-is-success" | "bulma-is-warning" | "bulma-is-danger";
type Sizes = "bulma-is-small" | "bulma-is-normal" | "bulma-is-medium" | "bulma-is-large";

interface TextFieldProps {
    uuid: string;
    required?: boolean;
    fullWidth?: boolean;
    autoComplete?: string;
    autoFocus: boolean;
    onKeyUp: (event: ReactKeyboardEvent) => void;
    onChange: (event: ReactChangeEvent) => void;
    value: string | number | readonly string[] | undefined;
    placeholder?: string;
    isDisabled?: boolean;
    colour?: Colours;
    size?: Sizes;
    isRounded?: boolean;
    isLoading?: boolean;
    isReadonly?: boolean;
}

export const TextFieldView = (props: TextFieldProps): React.ReactElement => {
    const rounded = props.isRounded ? "bulma-is-rounded" : "";
    const loading = props.isLoading ? "bulma-is-loading" : "";
    const colour = props.colour ?? "";
    const size = props.size ?? "";

    const className = `bulma-input ${rounded} ${colour} ${size}`;

    return (
        <div className={`bulma-control ${loading}`}>
            <input 
                id={props.uuid}
                name={props.uuid}
                type="text"
                required={props.required}
                autoComplete={props.autoComplete}
                autoFocus={props.autoFocus}
                value={props.value}
                placeholder={props.placeholder}
                disabled={props.isDisabled}
                className={className}
                readOnly={props.isReadonly}
            />
        </div>
    );
}
