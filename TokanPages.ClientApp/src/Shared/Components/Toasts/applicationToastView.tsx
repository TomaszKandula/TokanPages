import React from "react";
import Snackbar from "@material-ui/core/Snackbar";
import { SnackbarOrigin } from "@material-ui/core";
import { Alert, Color } from "@material-ui/lab";
import applicationToastViewStyle from "./applicationToastViewStyle";

interface IBinding
{
    bind: IProperties;
}

interface IProperties
{
    anchorOrigin: SnackbarOrigin;
    isOpen: boolean;
    autoHideDuration: number;
    closeEventHandler: any;
    TransitionComponent: any;
    key: any;
    toastSeverity: Color;
    toastMessage: string;
}

export default function ApplicationToastView(props: IBinding) 
{
    const classes = applicationToastViewStyle();
    return (
        <div className={classes.root}>
            <Snackbar 
                anchorOrigin={props.bind?.anchorOrigin} 
                open={props.bind?.isOpen} 
                autoHideDuration={props.bind?.autoHideDuration}
                onClose={props.bind?.closeEventHandler} 
                TransitionComponent={props.bind?.TransitionComponent} 
                key={props.bind?.key}
            >
                <Alert onClose={props.bind?.closeEventHandler} severity={props.bind?.toastSeverity} elevation={6} variant="filled">
                    {props.bind?.toastMessage}
                </Alert>
            </Snackbar>
        </div>
    );
}
