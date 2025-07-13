import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationErrorAction } from "../../../Store/Actions";
import { ApplicationState } from "../../../Store/Configuration";
import { DialogType } from "../../../Shared/enums";
import { NotificationToasterProps, ToastData, MessageType } from "./Types";
import { ToastList } from "./ToastList";
import Validate from "validate.js";

export const ApplicationToaster = (props: NotificationToasterProps): React.ReactElement => {
    const dispatch = useDispatch();
    const error = useSelector((state: ApplicationState) => state.applicationError);

    const isEmpty = Validate.isEmpty(error?.errorDetails);
    const isOpen = !isEmpty && error.dialogType === DialogType.toast;

    const [toasts, setToasts] = React.useState<ToastData[]>([]);

    const removeToast = React.useCallback((id: number) => {
        setToasts(prevToasts => prevToasts.filter(toast => toast.id !== id));
        dispatch(ApplicationErrorAction.clear());
    }, []);

    const showToast = React.useCallback(
        (message: string, type: MessageType) => {
            const toast: ToastData = {
                id: Date.now(),
                message,
                type,
            };

            setToasts(prevToasts => [...prevToasts, toast]);

            if (props.hasAutoClose) {
                setTimeout(() => {
                    removeToast(toast.id);
                }, props.AutoCloseDurationSec * 1000);
            }
        },
        [props.hasAutoClose, props.AutoCloseDurationSec]
    );

    React.useEffect(() => {
        if (isOpen) {
            showToast(error?.errorDetails ?? error?.errorMessage, "failure");
        }
    }, [isOpen]);

    return <ToastList data={toasts} position={props.position ?? "bottom-right"} removeToast={removeToast} />;
};
