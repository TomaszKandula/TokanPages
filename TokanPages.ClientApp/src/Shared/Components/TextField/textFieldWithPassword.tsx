import * as React from "react";
import { ReactMouseEvent } from "../../../Shared/Types";
import { TextFieldWithPasswordView } from "./View/textFieldWithPasswordView";
import { TextFieldProps } from "./Types";

export const TextFieldWithPassword = (props: TextFieldProps): React.ReactElement => {
    const [isPasswordVisible, setIsPasswordVisible] = React.useState(false);

    const iconOnClick = React.useCallback(() => {
        setIsPasswordVisible(!isPasswordVisible);
    }, [isPasswordVisible]);

    const iconOnMouseDown = React.useCallback((event: ReactMouseEvent) => {
        event.preventDefault();
    }, []);

    return (
        <TextFieldWithPasswordView
            className={props.className}
            uuid={props.uuid}
            isDisabled={props.isDisabled}
            value={props.value}
            placeholder={props.placeholder ?? ""}
            showPassword={isPasswordVisible}
            onKeyUp={props.onKeyUp}
            onChange={props.onChange}
            iconOnClick={iconOnClick}
            iconOnMouseDown={iconOnMouseDown}
        />
    );
};
