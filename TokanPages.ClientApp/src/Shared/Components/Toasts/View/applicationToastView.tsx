import React from "react";
import Snackbar from "@material-ui/core/Snackbar";
import { SlideProps, SnackbarOrigin } from "@material-ui/core";
import { Alert, Color } from "@material-ui/lab";
import { ApplicationToastViewStyle } from "./applicationToastViewStyle";
import { ReactSyntheticEvent } from "../../../../Shared/types";

interface Properties
{
    anchorOrigin: SnackbarOrigin;
    isOpen: boolean;
    autoHideDuration: number;
    closeEventHandler: (event?: ReactSyntheticEvent, reason?: string) => void;
    TransitionComponent: (props: Omit<SlideProps, "direction">) => JSX.Element;
    componentKey: string;
    toastSeverity: Color;
    toastMessage: string;
}

export const ApplicationToastView = (props: Properties): JSX.Element => 
{
    const classes = ApplicationToastViewStyle();
    return (
        <div className={classes.root}>
            <Snackbar 
                anchorOrigin={props.anchorOrigin} 
                open={props.isOpen} 
                autoHideDuration={props.autoHideDuration}
                onClose={props.closeEventHandler} 
                TransitionComponent={props.TransitionComponent} 
                key={props.componentKey}
            >
                <Alert 
                    variant="filled"
                    elevation={6} 
                    onClose={props.closeEventHandler} 
                    severity={props.toastSeverity}>
                    {props.toastMessage}
                </Alert>
            </Snackbar>
        </div>
    );
}
