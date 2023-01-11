import * as React from "react";
import ReactHtmlParser from "react-html-parser";
import Button from "@material-ui/core/Button";
import Dialog from "@material-ui/core/Dialog";
import DialogActions from "@material-ui/core/DialogActions";
import DialogContent from "@material-ui/core/DialogContent";
import DialogTitle from "@material-ui/core/DialogTitle";
import InfoIcon from '@material-ui/icons/Info';
import WarningIcon from '@material-ui/icons/Warning';
import ErrorIcon from '@material-ui/icons/Error';
import { ClassNameMap } from "@material-ui/core/styles/withStyles";
import { Divider, Typography } from "@material-ui/core";
import { IconType } from "../../../enums";
import { ApplicationDialogBoxStyle } from "./applicationDialogBoxStyle";

interface IBinding
{
    bind: IProperties;
}

interface IProperties
{
    state: boolean;
    icon: IconType;
    title: string;
    message: string;
    closeHandler: any;
}

type classType = ClassNameMap<"icon_holder" | "button" | "info_icon" | "warning_icon" | "error_icon" | "title" | "description">;

const RenderIcon = (args: { icon: IconType, classes: classType }): JSX.Element => 
{
    switch (args.icon)
    {
        case IconType.info: return(<InfoIcon className={args.classes.info_icon} />);
        case IconType.warning: return(<WarningIcon className={args.classes.warning_icon} />);
        case IconType.error: return(<ErrorIcon className={args.classes.error_icon} />);
        default: return(<InfoIcon className={args.classes.info_icon} />);
    }        
};   

export const ApplicationDialogBoxView = (props: IBinding): JSX.Element =>
{
    const classes = ApplicationDialogBoxStyle();
    return (
        <Dialog open={props.bind?.state} onClose={props.bind?.closeHandler} 
            aria-labelledby="alert-dialog-title" aria-describedby="alert-dialog-description">
            <DialogTitle id="alert-dialog-title" className={classes.title}>
                <div className={classes.icon_holder}>
                    <RenderIcon icon={props.bind?.icon} classes={classes} />
                    {ReactHtmlParser(props.bind?.title)}
                </div>
            </DialogTitle>
            <Divider />
            <DialogContent>
                <Typography className={classes.description} id="alert-dialog-description">
                    {ReactHtmlParser(props.bind?.message)}
                </Typography>
            </DialogContent>
            <Divider />
            <DialogActions>
                <Button onClick={props.bind?.closeHandler} className={classes.button} autoFocus>OK</Button>
            </DialogActions>
        </Dialog>
    );
}
