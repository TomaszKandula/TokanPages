import React from "react";
import { IconMapProps, ToastProps } from "../Types";
import { SuccessIcon, FailureIcon, WarningIcon, CloseIcon } from "../Icons";
import "./toast.css";

const iconMap: IconMapProps = {
    success: <SuccessIcon />,
    failure: <FailureIcon />,
    warning: <WarningIcon />,
};

export const Toast = (props: ToastProps): React.ReactElement => {
    const toastIcon = iconMap[props.type] || null;

    return (
        <div data-testid="toast-view" className={`toast toast--${props.type}`} role="alert">
            <div className="toast-message">
                {toastIcon && <div className="icon icon--lg icon--thumb">{toastIcon}</div>}
                <p>{props.message}</p>
            </div>
            <button className="toast-close-btn" onClick={props.onClose}>
                <span className="icon">
                    <CloseIcon />
                </span>
            </button>
        </div>
    );
};
