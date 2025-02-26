import * as React from "react";
import Button from "@material-ui/core/Button";
import Dialog from "@material-ui/core/Dialog";
import DialogActions from "@material-ui/core/DialogActions";
import DialogContent from "@material-ui/core/DialogContent";
import DialogTitle from "@material-ui/core/DialogTitle";
import InfoIcon from "@material-ui/icons/Info";
import WarningIcon from "@material-ui/icons/Warning";
import ErrorIcon from "@material-ui/icons/Error";
import { Divider, Typography } from "@material-ui/core";
import { IconType } from "../../../enums";
import { RenderParagraphs } from "../../../../Shared/Components/RenderParagraphs/renderParagraphs";

interface Properties {
    state: boolean;
    icon: IconType | undefined;
    title: string | undefined;
    message: string[] | undefined;
    disablePortal?: boolean;
    hideBackdrop?: boolean;
    closeHandler: () => void;
}

const RenderIcon = (props: Properties): React.ReactElement | null => {
    switch (props.icon) {
        case IconType.info:
            return <InfoIcon className="dialog-box-info-icon" />;
        case IconType.warning:
            return <WarningIcon className="dialog-box-warning-icon" />;
        case IconType.error:
            return <ErrorIcon className="dialog-box-error-icon" />;
        default:
            return null;
    }
};

const RenderDialogContent = (props: Properties) => {
    return (
        <>
            <DialogTitle id="alert-dialog-title" className="dialog-box-title">
                <div className="dialog-box-icon-holder">
                    <RenderIcon {...props} />
                    {props.title}
                </div>
            </DialogTitle>
            <Divider />
            <DialogContent>
                <Typography component="span" className="dialog-box-description" id="alert-dialog-description">
                    <RenderParagraphs text={props.message ?? []} />
                </Typography>
            </DialogContent>
            <Divider />
            <DialogActions>
                <Button onClick={props.closeHandler} className="button" autoFocus>
                    OK
                </Button>
            </DialogActions>
        </>
    );
}

const RenderDialogBox = (props: Properties) => {
    const hasTitle = props?.title !== undefined;
    const hasIcon = props?.icon !== undefined;
    const hasMessage = props?.message && props?.message?.length !== 0;

    return (
        <Dialog
            open={props.state}
            onClose={props.closeHandler}
            disablePortal={props.disablePortal}
            hideBackdrop={props.hideBackdrop}
            aria-labelledby="alert-dialog-title"
            aria-describedby="alert-dialog-description"
        >
            {hasTitle && hasIcon && hasMessage 
            ? <RenderDialogContent {...props} /> 
            : <></>}
        </Dialog>
    );
} 

export const ApplicationDialogBoxView = (props: Properties): React.ReactElement => {
    return <RenderDialogBox {...props} />;
};
