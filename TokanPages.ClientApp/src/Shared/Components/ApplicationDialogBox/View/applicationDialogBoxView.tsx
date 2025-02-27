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
import { RenderList } from "../../../../Shared/Components/RenderList/renderList";

interface Properties {
    state: boolean;
    icon: IconType | undefined;
    title: string | undefined;
    message: string[] | undefined;
    validation?: object | undefined;
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

const RenderValidationList = (props: Properties): React.ReactElement => {
    const validation = props.validation;

    let result: string[] = [];
    if (validation) {
        Object.keys(validation).forEach((key, _) => {
            const prop = key as keyof typeof validation;
            const data = validation[prop] as string | string[];

            if (Array.isArray(data)) {
                data.forEach((item: string) => {
                    result.push(item);
                });
            } else {
                result.push(data);
            }
        });
    }

    return <RenderList list={result} />;
}

const RenderDialogContent = (props: Properties): React.ReactElement => {
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
                    <RenderParagraphs 
                        text={props.message ?? []}
                        replace={{
                            key: "{LIST}",
                            object: <RenderValidationList {...props} />
                        }}
                    />
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

const RenderDialogBox = (props: Properties): React.ReactElement => {
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
