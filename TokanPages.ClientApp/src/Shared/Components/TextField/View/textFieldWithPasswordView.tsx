import * as React from "react";
import { ReactChangeEvent, ReactKeyboardEvent, ReactMouseEvent } from "../../../types";
import { Icon } from "../../Icon";
import { IconButton } from "../../IconButton";
import "./textFiedWithPasswordView.css";

interface Properties {
    uuid: string;
    fullWidth?: boolean;
    disabled?: boolean;
    inputValue: string;
    inputLabel: string;
    showPassword: boolean;
    className?: string;
    onKeyUpHandler: (event: ReactKeyboardEvent) => void;
    onChangeHandler: (event: ReactChangeEvent) => void;
    iconOnClickHandler: (event: ReactMouseEvent) => void;
    iconOnMouseDownHandler: (event: ReactMouseEvent) => void;
}

export const TextFieldWithPasswordView = (props: Properties): React.ReactElement => {
    const IconElement = (): React.ReactElement => props.showPassword ? <Icon name="Eye" size={1} /> : <Icon name="EyeOff" size={1} />;

    const type = props.showPassword ? "text" : "password";
    const icon = "bulma-icon bulma-is-right text-field-with-password";
    const control = `bulma-control bulma-has-icons-right ${props.className ?? ""}`;

    return (
        <div className={control}>
            <input
                id={props.uuid}
                name={props.uuid}
                type={type}
                value={props.inputValue}
                onKeyUp={props.onKeyUpHandler}
                onChange={props.onChangeHandler}
                disabled={props.disabled}
                placeholder={props.inputLabel}
                className="bulma-input"
            />
            <span className={icon}>
                <IconButton 
                    onClick={props.iconOnClickHandler} 
                    onMouseDown={props.iconOnMouseDownHandler} 
                    hasNoHoverEffect
                >
                    <IconElement />
                </IconButton>
            </span>
        </div>
    );
};
