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
import useStyles from "./Hooks/styleAlertDialog";
import { Divider, Typography } from "@material-ui/core";

interface IAlertDialog
{
    State:   boolean,
    Handle:  any,
    Title:   string,
    Message: string,
    Icon:    number
}

export default function AlertDialog(props: IAlertDialog) 
{

    const classes = useStyles();
    const RenderIcon = () => 
    {
        switch (props.Icon)
        {
            case 0: return(<InfoIcon className={classes.InfoIcon} />);
            case 1: return(<WarningIcon className={classes.WarningIcon} />);
            case 2: return(<ErrorIcon className={classes.ErrorIcon} />);
            default: return(<InfoIcon className={classes.InfoIcon} />);
        }        
    };   

    return (
        <Dialog open={props.State} onClose={props.Handle} aria-labelledby="alert-dialog-title" aria-describedby="alert-dialog-description">
            <DialogTitle id="alert-dialog-title">
                <div style={{ display: "flex", alignItems: "center" }}>
                    <RenderIcon />
                    {ReactHtmlParser(props.Title)}
                </div>
            </DialogTitle>
            <Divider />
            <DialogContent>
                <Typography className={classes.Typography} component={"span"} variant={"body1"} id="alert-dialog-description">
                    {ReactHtmlParser(props.Message)}
                </Typography>
            </DialogContent>
            <Divider />
            <DialogActions>
                <Button onClick={props.Handle} color="primary" autoFocus>
                    OK
                </Button>
            </DialogActions>
        </Dialog>
    );

}
