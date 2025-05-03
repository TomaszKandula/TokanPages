import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { Slide, SlideProps } from "@material-ui/core";
import { ReactSyntheticEvent } from "../../../Shared/types";
import { DialogType } from "../../../Shared/enums";
import { ApplicationState } from "../../../Store/Configuration";
import { ApplicationErrorAction } from "../../../Store/Actions";
import { ApplicationToastView } from "./View/applicationToastView";
import Validate from "validate.js";

const TransitionLeft = (props: Omit<SlideProps, "direction">): React.ReactElement => {
    return <Slide {...props} direction="left" />;
};

export const ApplicationToast = (): React.ReactElement => {
    const vertical = "top";
    const horizontal = "right";
    const toastSeverity = "error";
    const autoHideDuration: number = 15000;

    const dispatch = useDispatch();
    const error = useSelector((state: ApplicationState) => state.applicationError);

    const isEmpty = Validate.isEmpty(error?.errorDetails);
    const isOpen = !isEmpty && error.dialogType === DialogType.toast;

    const closeEventHandler = React.useCallback(
        (event?: ReactSyntheticEvent, reason?: string) => {
            if (event === undefined) return;
            if (reason === "clickaway") return;
            dispatch(ApplicationErrorAction.clear());
        },[]
    );

    return (
        <ApplicationToastView
            anchorOrigin={{ vertical, horizontal }}
            isOpen={isOpen}
            autoHideDuration={autoHideDuration}
            closeEventHandler={closeEventHandler}
            TransitionComponent={TransitionLeft}
            componentKey={vertical + horizontal}
            toastSeverity={toastSeverity}
            toastMessage={error?.errorDetails ?? error?.errorMessage}
        />
    );
};
