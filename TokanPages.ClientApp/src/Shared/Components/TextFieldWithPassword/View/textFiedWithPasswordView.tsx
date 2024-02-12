import * as React from "react";
import { TextFiedWithPasswordStyle } from "./textFiedWithPasswordStyle";
import { Visibility, VisibilityOff } from "@material-ui/icons";

import { FormControl, IconButton, OutlinedInput, InputAdornment, InputLabel } from "@material-ui/core";

import { ReactChangeEvent, ReactKeyboardEvent, ReactMouseEvent } from "../../../../Shared/types";

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

export const TextFiedWithPasswordView = (props: Properties): JSX.Element => {
    const classes = TextFiedWithPasswordStyle();
    return (
        <>
            <FormControl fullWidth={props.fullWidth} className={classes.textField} variant="outlined">
                <InputLabel htmlFor={props.uuid}>{props.inputLabel}</InputLabel>
                <OutlinedInput
                    id={props.uuid}
                    name={props.uuid}
                    type={props.showPassword ? "text" : "password"}
                    value={props.inputValue}
                    onKeyUp={props.onKeyUpHandler}
                    onChange={props.onChangeHandler}
                    disabled={props.disabled}
                    labelWidth={70}
                    endAdornment={
                        <InputAdornment position="end">
                            <IconButton
                                aria-label="toggle password visibility"
                                onClick={props.iconOnClickHandler}
                                onMouseDown={props.iconOnMouseDownHandler}
                                edge="end"
                            >
                                {props.showPassword ? <Visibility /> : <VisibilityOff />}
                            </IconButton>
                        </InputAdornment>
                    }
                />
            </FormControl>
        </>
    );
};
