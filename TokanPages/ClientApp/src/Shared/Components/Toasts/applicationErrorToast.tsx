import React from "react";
import Snackbar from "@material-ui/core/Snackbar";
import { useDispatch, useSelector } from "react-redux";
import { IApplicationState } from "../../../Redux/applicationState";
import { ActionCreators } from "../../../Redux/Actions/raiseErrorAction";
import { RECEIVED_ERROR_MESSAGE } from "../../constants";

export default function ApplicationErrorToast() 
{
    const vertical = "top";
    const horizontal = "right";
    const [state, setState] = React.useState({ isOpen: false, errorMessage: "" });   
    const dispatch = useDispatch();
    const raiseErrorState = useSelector((state: IApplicationState) => state.raiseError);
    const clearError = React.useCallback(() => dispatch(ActionCreators.clearError()), [ dispatch ]);
    
    React.useEffect(() => 
    { 
        if (raiseErrorState === undefined) return; 
        
        if (raiseErrorState.defaultErrorMessage === RECEIVED_ERROR_MESSAGE)
            setState({ isOpen: true, errorMessage: raiseErrorState.attachedErrorObject?.ErrorMessage });

        if (raiseErrorState.defaultErrorMessage === RECEIVED_ERROR_MESSAGE && !state.isOpen)
            clearError();

    }, 
    [ clearError, raiseErrorState, state.isOpen ]);

    const handleClose = () => setState({ isOpen: false, errorMessage: "" });

    return (
        <div>
            <Snackbar
                anchorOrigin={{ vertical, horizontal }}
                open={state.isOpen}
                onClose={handleClose}
                message={state.errorMessage}
                key={vertical + horizontal}
            />
        </div>
    );
}
