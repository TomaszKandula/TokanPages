import * as React from "react";
import { ReactChangeEvent, ReactKeyboardEvent, ReactMouseEvent } from "../../../../Shared/types";
import { Icon } from "../../../../Shared/Components/Icon";
import { IconButton } from "../../../../Shared/Components/IconButton";
import "./textFiedWithPasswordView.css";

interface Properties {
    uuid: string;
    fullWidth?: boolean;
    disabled?: boolean;
    inputValue: string;
    inputLabel: string;
    showPassword: boolean;
    onKeyUpHandler: (event: ReactKeyboardEvent) => void;
    onChangeHandler: (event: ReactChangeEvent) => void;
    iconOnClickHandler: (event: ReactMouseEvent) => void;
    iconOnMouseDownHandler: (event: ReactMouseEvent) => void;
}

export const TextFiedWithPasswordView = (props: Properties): React.ReactElement => {
    return (
        <>
            <div className="bulma-control bulma-has-icons-right">
                <input
                    id={props.uuid}
                    name={props.uuid}
                    type={props.showPassword ? "text" : "password"}
                    value={props.inputValue}
                    onKeyUp={props.onKeyUpHandler}
                    onChange={props.onChangeHandler}
                    disabled={props.disabled}
                    placeholder={props.inputLabel}
                    className="bulma-input"
                />
                <span className="bulma-icon bulma-is-right text-field-with-password">
                    <IconButton onClick={props.iconOnClickHandler} onMouseDown={props.iconOnMouseDownHandler} hasNoHoverEffect>
                        {props.showPassword ? <Icon name="Eye" size={1} /> : <Icon name="EyeOff" size={1} />}
                    </IconButton>
                </span>
            </div>
        </>
    );
};
