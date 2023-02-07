import * as React from "react";
import { ReactChangeEvent, ReactKeyboardEvent, ReactMouseEvent } from "../../../Shared/types";
import { TextFiedWithPasswordView } from "./View/textFiedWithPasswordView";

interface Properties 
{
    uuid: string;
    fullWidth?: boolean;
    value: string;
    label?: string;
    onKeyUp: (event: ReactKeyboardEvent) => void;
    onChange: (event: ReactChangeEvent) => void;
    isDisabled?: boolean;
}

export const TextFiedWithPassword = (props: Properties): JSX.Element => 
{
    const [isPasswordVisible, setIsPasswordVisible] = React.useState(false);

    const iconOnClickHandler = React.useCallback(() => 
    {
        setIsPasswordVisible(!isPasswordVisible);
    }, [ isPasswordVisible ]);

    const iconOnMouseDownHandler = React.useCallback((event: ReactMouseEvent) => 
    {
        event.preventDefault();
    }, [ ]);

    return (
        <TextFiedWithPasswordView 
            uuid={props.uuid}
            fullWidth={props.fullWidth}
            disabled={props.isDisabled}
            inputValue={props.value}
            inputLabel={props.label ?? ""}
            showPassword={isPasswordVisible}
            onKeyUpHandler={props.onKeyUp}
            onChangeHandler={props.onChange}
            iconOnClickHandler={iconOnClickHandler}
            iconOnMouseDownHandler={iconOnMouseDownHandler}
        />
    );
}
