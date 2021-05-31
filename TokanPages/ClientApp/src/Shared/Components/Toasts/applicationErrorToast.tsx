import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { Slide, SlideProps } from "@material-ui/core";
import { IApplicationState } from "../../../Redux/applicationState";
import { ActionCreators } from "../../../Redux/Actions/raiseErrorAction";
import { RECEIVED_ERROR_MESSAGE } from "../../constants";
import { DialogType } from "../../../Shared/enums";
import ApplicationToastView from "./applicationToastView";

interface IDefaultToastState 
{
    isOpen: boolean;
    errorMessage: string;
}

const DefaultToastState: IDefaultToastState = 
{
    isOpen: false, 
    errorMessage: ""    
}

const TransitionLeft = (props: Omit<SlideProps, "direction">) =>
{
    return <Slide {...props} direction="left" />;
}

export default function ApplicationErrorToast() 
{
    const vertical = "top";
    const horizontal = "right";
    const toastSeverity = "error";
    const autoHideDuration: number = 15000;

    const dispatch = useDispatch();
    const [toastState, setToastState] = React.useState(DefaultToastState);   
    const raiseErrorState = useSelector((state: IApplicationState) => state.raiseError);
    
    const clearError = React.useCallback(() => 
    { 
        if (!toastState.isOpen && toastState.errorMessage !== "")
        {
            dispatch(ActionCreators.clearError());
            setToastState(DefaultToastState);
        }

    }, [ dispatch, toastState ]);
    
    const raiseError = React.useCallback(() => 
    {
        if (raiseErrorState?.dialogType !== DialogType.toast) return;
        
        if (raiseErrorState?.defaultErrorMessage === RECEIVED_ERROR_MESSAGE)
        {
            setToastState(
            { 
                isOpen: true,
                errorMessage: raiseErrorState?.attachedErrorObject 
            });
        }
    }, 
    [ raiseErrorState ]);

    React.useEffect(() => raiseError(), [ raiseError ]);
    React.useEffect(() => clearError(), [ clearError ]);

    const closeEventHandler = (event?: React.SyntheticEvent, reason?: string) => 
    {
        if (event === undefined) return; 
        if (reason === "clickaway") return;
        setToastState({ ...toastState, isOpen: false });
    }

    return (<ApplicationToastView bind=
    {{
        anchorOrigin: { vertical, horizontal },
        isOpen: toastState.isOpen,
        autoHideDuration: autoHideDuration,
        closeEventHandler: closeEventHandler,
        TransitionComponent: TransitionLeft,
        key: vertical + horizontal,
        toastSeverity: toastSeverity,
        toastMessage: toastState.errorMessage
    }}/>);
}
