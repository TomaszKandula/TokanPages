import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../Store/Configuration";
import { ApplicationDialogAction } from "../../../Store/Actions";
import { ApplicationDialogState } from "../../../Store/States";
import { IconType } from "../../enums";
import { ApplicationDialogBoxView } from "./View/applicationDialogBoxView";
import Validate from "validate.js";

interface Properties extends ApplicationDialogState {
    state: boolean;
}

const DialogState: Properties = {
    state: false,
    title: "",
    message: "",
    icon: IconType.info,
};

// TODO: refactor, remove component state
export const ApplicationDialogBox = (): JSX.Element => {
    const dispatch = useDispatch();
    const [dialogState, setDialogState] = React.useState(DialogState);
    const dialog = useSelector((state: ApplicationState) => state.applicationDialog);

    const clearDialog = React.useCallback(() => {
        if (!dialogState.state && !Validate.isEmpty(dialogState.message)) {
            dispatch(ApplicationDialogAction.clear());
            setDialogState(DialogState);
        }
    }, [dispatch, dialogState]);

    const raiseDialog = React.useCallback(() => {
        if (!Validate.isEmpty(dialog?.message)) {
            setDialogState({
                state: true,
                title: dialog?.title,
                message: dialog?.message,
                icon: dialog?.icon,
            });
        }
    }, [dialog]);

    React.useEffect(() => raiseDialog(), [raiseDialog]);
    React.useEffect(() => clearDialog(), [clearDialog]);

    const onClickHandler = React.useCallback(() => {
        setDialogState({ ...dialogState, state: false });
    }, [dialogState]);

    return (
        <ApplicationDialogBoxView
            state={dialogState.state}
            icon={dialogState.icon}
            title={dialogState.title}
            message={dialogState.message}
            closeHandler={onClickHandler}
        />
    );
};
