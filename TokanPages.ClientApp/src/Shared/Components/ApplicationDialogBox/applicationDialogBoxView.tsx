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
import { Divider, Typography } from "@material-ui/core";
import { IconType } from "../../enums";
import ApplicationDialogBoxStyle from "./applicationDialogBoxStyle";

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
    onCloseHandler: any;
    onButtonClickHandler: any;
}

const ApplicationDialogBoxView = (props: IBinding): JSX.Element =>
{
    const classes = ApplicationDialogBoxStyle();
    const RenderIcon = (): JSX.Element => 
    {
        switch (props.bind?.icon)
        {
            case IconType.info: return(<InfoIcon className={classes.InfoIcon} />);
            case IconType.warning: return(<WarningIcon className={classes.WarningIcon} />);
            case IconType.error: return(<ErrorIcon className={classes.ErrorIcon} />);
            default: return(<InfoIcon className={classes.InfoIcon} />);
        }        
    };   

    return (
        <Dialog open={props.bind?.state} onClose={props.bind?.onCloseHandler} aria-labelledby="alert-dialog-title" aria-describedby="alert-dialog-description">
            <DialogTitle id="alert-dialog-title">
                <div style={{ display: "flex", alignItems: "center" }}>
                    <RenderIcon />
                    {ReactHtmlParser(props.bind?.title)}
                </div>
            </DialogTitle>
            <Divider />
            <DialogContent>
                <Typography className={classes.Typography} component={"span"} variant={"body1"} id="alert-dialog-description">
                    {ReactHtmlParser(props.bind?.message)}
                </Typography>
            </DialogContent>
            <Divider />
            <DialogActions>
                <Button onClick={props.bind?.onButtonClickHandler} color="primary" autoFocus>
                    OK
                </Button>
            </DialogActions>
        </Dialog>
    );
}

export default ApplicationDialogBoxView;
