import * as React from "react";
import Button from "@material-ui/core/Button";
import Dialog from "@material-ui/core/Dialog";
import DialogActions from "@material-ui/core/DialogActions";
import DialogContent from "@material-ui/core/DialogContent";
import DialogContentText from "@material-ui/core/DialogContentText";
import DialogTitle from "@material-ui/core/DialogTitle";
import Grid from "@material-ui/core/Grid";
import InfoIcon from '@material-ui/icons/Info';
import WarningIcon from '@material-ui/icons/Warning';
import ErrorIcon from '@material-ui/icons/Error';

interface IAlertDialog
{
    State:   boolean,
    Title:   string,
    Message: string,
    Icon:    number
}

export default function AlertDialog(props: IAlertDialog) 
{

    const [state, setState] = React.useState(props.State);
    const handleClose = () => { setState(false); };

    const RenderIcon = () => 
    {
        switch (props.Icon)
        {
            case 0: return(<InfoIcon />);
            case 1: return(<WarningIcon />);
            case 2: return(<ErrorIcon />);
            default: return(<InfoIcon />);
        }        
    };

    return (
        <Dialog open={state} onClose={handleClose} aria-labelledby="alert-dialog-title" aria-describedby="alert-dialog-description">
            <Grid container spacing={2}>
                <Grid item xs={12} sm={3}>
                    <RenderIcon />
                </Grid>
                <Grid item xs={12} sm={9}>
                    <DialogTitle id="alert-dialog-title">
                        {props.Title}
                    </DialogTitle>
                </Grid>
            </Grid>
            <DialogContent>
                <DialogContentText id="alert-dialog-description">
                    {props.Message}
                </DialogContentText>
            </DialogContent>
            <DialogActions>
                <Button onClick={handleClose} color="primary" autoFocus>
                    OK
                </Button>
            </DialogActions>
        </Dialog>
    );

}
