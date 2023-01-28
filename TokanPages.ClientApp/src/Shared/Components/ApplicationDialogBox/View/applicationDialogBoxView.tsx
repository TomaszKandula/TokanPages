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
import { ReactHtmlParser } from "../../../../Shared/Services/Renderers";
import { IconType } from "../../../enums";
import { ApplicationDialogBoxStyle } from "./applicationDialogBoxStyle";

interface IProperties
{
    state: boolean;
    icon: IconType;
    title: string;
    message: string;
    closeHandler: any;
}

const RenderIcon = (props: IProperties): JSX.Element => 
{
    const classes = ApplicationDialogBoxStyle();
    switch (props.icon)
    {
        case IconType.info: return(<InfoIcon className={classes.info_icon} />);
        case IconType.warning: return(<WarningIcon className={classes.warning_icon} />);
        case IconType.error: return(<ErrorIcon className={classes.error_icon} />);
        default: return(<InfoIcon className={classes.info_icon} />);
    }        
};   

export const ApplicationDialogBoxView = (props: IProperties): JSX.Element =>
{
    const classes = ApplicationDialogBoxStyle();
    return (
        <Dialog open={props.state} onClose={props.closeHandler} 
            aria-labelledby="alert-dialog-title" aria-describedby="alert-dialog-description">
            <DialogTitle id="alert-dialog-title" className={classes.title}>
                <div className={classes.icon_holder}>
                    <RenderIcon {...props} />
                    <ReactHtmlParser html={props.title} />
                </div>
            </DialogTitle>
            <Divider />
            <DialogContent>
                <Typography component="span" className={classes.description} id="alert-dialog-description">
                    <ReactHtmlParser html={props.message} />
                </Typography>
            </DialogContent>
            <Divider />
            <DialogActions>
                <Button onClick={props.closeHandler} className={classes.button} autoFocus>OK</Button>
            </DialogActions>
        </Dialog>
    );
}
