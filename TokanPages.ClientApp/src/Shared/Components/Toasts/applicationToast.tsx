import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { Slide, SlideProps } from "@material-ui/core";
import { ReactSyntheticEvent } from "../../../Shared/types";
import { ApplicationState } from "../../../Store/Configuration";
import { ApplicationErrorAction } from "../../../Store/Actions";
import { RECEIVED_ERROR_MESSAGE } from "../../constants";
import { DialogType } from "../../enums";
import { ApplicationToastView } from "./View/applicationToastView";
import Validate from "validate.js";

interface Properties 
{
    isOpen: boolean;
    errorMessage: string;
}

const ToastState: Properties = 
{
    isOpen: false, 
    errorMessage: ""    
}

const TransitionLeft = (props: Omit<SlideProps, "direction">): JSX.Element => 
{
    return <Slide {...props} direction="left" />;
}

// TODO: remove component state
export const ApplicationToast = (): JSX.Element => 
{
    const vertical = "top";
    const horizontal = "right";
    const toastSeverity = "error";
    const autoHideDuration: number = 15000;

    const dispatch = useDispatch();
    const [toastState, setToastState] = React.useState(ToastState);   
    const error = useSelector((state: ApplicationState) => state.applicationError);
    
    const clearError = React.useCallback(() => 
    { 
        if (!toastState.isOpen && !Validate.isEmpty(toastState.errorMessage))
        {
            dispatch(ApplicationErrorAction.clear());
            setToastState(ToastState);
        }
    }, 
    [ dispatch, toastState ]);
    
    const raiseError = React.useCallback(() => 
    {
        if (error?.dialogType !== DialogType.toast) return;
        if (error?.errorMessage === RECEIVED_ERROR_MESSAGE)
        {
            setToastState(
            { 
                isOpen: true,
                errorMessage: error?.errorDetails 
            });
        }
    }, 
    [ error ]);

    React.useEffect(() => raiseError(), [ raiseError ]);
    React.useEffect(() => clearError(), [ clearError ]);

    const closeEventHandler = React.useCallback((event?: ReactSyntheticEvent, reason?: string) => 
    {
        if (event === undefined) return; 
        if (reason === "clickaway") return;
        setToastState({ ...toastState, isOpen: false });
    }, [ toastState ]);

    return (<ApplicationToastView
        anchorOrigin={{ vertical, horizontal }}
        isOpen={toastState.isOpen}
        autoHideDuration={autoHideDuration}
        closeEventHandler={closeEventHandler}
        TransitionComponent={TransitionLeft}
        componentKey={vertical + horizontal}
        toastSeverity={toastSeverity}
        toastMessage={toastState.errorMessage}
    />);
}
