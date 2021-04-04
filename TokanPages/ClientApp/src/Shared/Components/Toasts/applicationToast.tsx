import React from "react";
import Snackbar, { SnackbarOrigin } from "@material-ui/core/Snackbar";

export interface State extends SnackbarOrigin 
{
  open: boolean;
}

export default function ApplicationToast() 
{
    const [state, setState] = React.useState<State>({ open: false, vertical: "top", horizontal: "right" });
    const { vertical, horizontal, open } = state;
    const handleClose = () => { setState({ ...state, open: false }); };

    return (
        <div>
            <Snackbar
                anchorOrigin={{ vertical, horizontal }}
                open={open}
                onClose={handleClose}
                message="TEST"
                key={vertical + horizontal}
            />
        </div>
    );
}
