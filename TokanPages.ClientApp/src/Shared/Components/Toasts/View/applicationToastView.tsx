import React from "react";
import Snackbar from "@material-ui/core/Snackbar";
import { SnackbarOrigin } from "@material-ui/core";
import { Alert, Color } from "@material-ui/lab";
import { ApplicationToastViewStyle } from "./applicationToastViewStyle";

interface Properties
{
    anchorOrigin: SnackbarOrigin;
    isOpen: boolean;
    autoHideDuration: number;
    closeEventHandler: any;
    TransitionComponent: any;
    componentKey: any;
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
