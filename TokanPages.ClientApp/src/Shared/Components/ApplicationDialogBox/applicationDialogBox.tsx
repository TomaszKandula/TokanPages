import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../Store/Configuration";
import { ApplicationDialogAction } from "../../../Store/Actions";
import { ApplicationDialogBoxView } from "./View/applicationDialogBoxView";

export const ApplicationDialogBox = (): React.ReactElement => {
    const dispatch = useDispatch();
    const dialog = useSelector((state: ApplicationState) => state.applicationDialog);

    const onClickHandler = React.useCallback(() => {
        dispatch(ApplicationDialogAction.clear());
    }, []);

    const hasTitle = dialog?.title !== undefined;
    const hasIcon = dialog?.icon !== undefined;
    const hasMessage = dialog?.message && dialog?.message?.length !== 0;

    return (
        <ApplicationDialogBoxView
            state={(hasTitle && hasIcon && hasMessage) ?? false}
            icon={dialog?.icon}
            title={dialog?.title}
            message={dialog?.message}
            validation={dialog?.validation}
            closeHandler={onClickHandler}
        />
    );
};
