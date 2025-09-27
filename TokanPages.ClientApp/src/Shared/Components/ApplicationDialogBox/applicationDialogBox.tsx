import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../Store/Configuration";
import { ApplicationDialogAction } from "../../../Store/Actions";
import { ApplicationDialogBoxView } from "./View/applicationDialogBoxView";

export const ApplicationDialogBox = (): React.ReactElement => {
    const dispatch = useDispatch();
    const dialog = useSelector((state: ApplicationState) => state.applicationDialog);

    const [isActionExecuted, setIsActionExecuted] = React.useState(false);

    const hasTitle = dialog?.title !== undefined;
    const hasIcon = dialog?.icon !== undefined;
    const hasMessage = dialog?.message && dialog?.message?.length !== 0;
    const isOpen = hasTitle && hasIcon && hasMessage;

    const onClickPrimaryButtonHandler = React.useCallback(() => {
        setIsActionExecuted(true);
        if (dialog.buttons?.primaryButton) {
            dialog.buttons?.primaryButton?.action();
        }
    }, [dialog.buttons?.primaryButton]);

    const onClickSecondaryButtonHandler = React.useCallback(() => {
        setIsActionExecuted(true);
        if (dialog.buttons?.secondaryButton) {
            dialog.buttons?.secondaryButton?.action();
        }
    }, [dialog.buttons?.secondaryButton]);

    React.useEffect(() => {
        if (isActionExecuted) {
            setIsActionExecuted(false);
            dispatch(ApplicationDialogAction.clear());
        }
    }, [isActionExecuted]);

    return (
        <ApplicationDialogBoxView
            isOpen={isOpen ?? false}
            icon={dialog?.icon}
            title={dialog?.title}
            message={dialog?.message}
            validation={dialog?.validation}
            primaryButtonLabel={dialog?.buttons?.primaryButton?.label}
            onClickPrimaryButtonHandler={onClickPrimaryButtonHandler}
            secondaryButtonLabel={dialog?.buttons?.secondaryButton?.label}
            onClickSecondaryButtonHandler={onClickSecondaryButtonHandler}
        />
    );
};
