import * as React from "react";
import { TextFieldProps } from "./Types";

export const TextFieldView = (props: TextFieldProps): React.ReactElement => {
    const rounded = props.isRounded ? "bulma-is-rounded" : "";
    const loading = props.isLoading ? "bulma-is-loading" : "";
    const colour = props.colour ?? "";
    const size = props.size ?? "";
    const icon = props.startIcon ? "bulma-has-icons-left" : "";

    const className = `bulma-input ${rounded} ${colour} ${size}`;

    return (
        <div className={`bulma-control ${icon} ${loading} ${props.className ?? ""}`}>
            <input
                id={props.uuid}
                name={props.uuid}
                type="text"
                required={props.required}
                maxLength={props.maxLength}
                autoComplete={props.autoComplete}
                autoFocus={props.autoFocus}
                value={props.value}
                onKeyUp={props.onKeyUp}
                onChange={props.onChange}
                placeholder={props.placeholder}
                disabled={props.isDisabled}
                className={className}
                readOnly={props.isReadonly}
            />
            {props.startIcon ? <span className="bulma-icon bulma-is-left">{props.startIcon}</span> : null}
        </div>
    );
};
