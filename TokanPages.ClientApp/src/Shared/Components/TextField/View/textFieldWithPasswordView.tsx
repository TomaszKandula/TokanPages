import * as React from "react";
import { Icon } from "../../Icon";
import { IconButton } from "../../IconButton";
import "./textFiedWithPasswordView.css";
import { TextFieldExtendedProps } from "./types";

const IconElement = (props: TextFieldExtendedProps): React.ReactElement =>
    props.showPassword ? <Icon name="Eye" size={1} /> : <Icon name="EyeOff" size={1} />;

export const TextFieldWithPasswordView = (props: TextFieldExtendedProps): React.ReactElement => {
    const rounded = props.isRounded ? "bulma-is-rounded" : "";
    const type = props.showPassword ? "text" : "password";
    const icon = "bulma-icon bulma-is-right text-field-with-password";
    const control = `bulma-control bulma-has-icons-right ${props.className ?? ""}`;
    const colour = props.colour ?? "";
    const size = props.size ?? "";

    const className = `bulma-input ${rounded} ${colour} ${size}`;

    return (
        <div className={control}>
            <input
                id={props.uuid}
                name={props.uuid}
                type={type}
                value={props.value}
                onKeyUp={props.onKeyUp}
                onChange={props.onChange}
                disabled={props.isDisabled}
                placeholder={props.placeholder}
                readOnly={props.isReadonly}
                className={className}
            />
            <span className={icon}>
                <IconButton onClick={props.iconOnClick} onMouseDown={props.iconOnMouseDown} hasNoHoverEffect>
                    <IconElement {...props} />
                </IconButton>
            </span>
        </div>
    );
};
