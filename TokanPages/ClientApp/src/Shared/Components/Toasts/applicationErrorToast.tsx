import React from "react";
import { useDispatch, useSelector } from "react-redux";
import Snackbar from "@material-ui/core/Snackbar";
import { Slide, SlideProps } from "@material-ui/core";
import { Alert } from "@material-ui/lab";
import { IApplicationState } from "../../../Redux/applicationState";
import { ActionCreators } from "../../../Redux/Actions/raiseErrorAction";
import { RECEIVED_ERROR_MESSAGE } from "../../constants";
import applicationErrorToastStyle from "./Styles/applicationErrorToastStyle";

const TransitionLeft = (props: Omit<SlideProps, "direction">) =>
{
    return <Slide {...props} direction="left" />;
}

export default function ApplicationErrorToast() 
{
    const vertical = "top";
    const horizontal = "right";
    const classes = applicationErrorToastStyle();
    
    const [toastState, setToastState] = React.useState({ isOpen: false, errorMessage: "" });   
    const dispatch = useDispatch();
    
    const raiseErrorState = useSelector((state: IApplicationState) => state.raiseError);
    const clearError = React.useCallback(() => dispatch(ActionCreators.clearError()), [ dispatch ]);
    
    React.useEffect(() => 
    { 
        if (raiseErrorState === undefined) 
            return; 
        
        if (raiseErrorState.defaultErrorMessage === RECEIVED_ERROR_MESSAGE)
            setToastState({ isOpen: true, errorMessage: raiseErrorState.attachedErrorObject });

        if (raiseErrorState.defaultErrorMessage === RECEIVED_ERROR_MESSAGE && !toastState.isOpen)
            clearError();
    }, 
    [ clearError, raiseErrorState, toastState.isOpen ]);

    const handleClose = (event?: React.SyntheticEvent, reason?: string) => 
    {
        if (event === undefined) return; 
        if (reason === "clickaway") return;
        setToastState({ isOpen: false, errorMessage: "" });
    }

    return (
        <div className={classes.root}>
            <Snackbar anchorOrigin={{ vertical, horizontal }} open={toastState.isOpen} autoHideDuration={15000}
                onClose={handleClose} TransitionComponent={TransitionLeft} key={vertical + horizontal}
            >
                <Alert onClose={handleClose} severity="error" elevation={6} variant="filled">
                    {toastState.errorMessage}
                </Alert>
            </Snackbar>
        </div>
    );
}
