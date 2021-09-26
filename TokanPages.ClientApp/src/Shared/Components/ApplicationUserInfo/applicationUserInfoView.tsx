import * as React from "react";
import Dialog from "@material-ui/core/Dialog";
import DialogTitle from "@material-ui/core/DialogTitle";
import Divider from "@material-ui/core/Divider";
import DialogContent from "@material-ui/core/DialogContent";
import DialogActions from "@material-ui/core/DialogActions";
import Button from "@material-ui/core/Button";
import Grid from "@material-ui/core/Grid";
import Typography from "@material-ui/core/Typography";
import CheckIcon from "@material-ui/icons/Check";
import Box from "@material-ui/core/Box";
import { green } from '@material-ui/core/colors';
import { List, ListItem, ListItemIcon, ListItemText } from "@material-ui/core";
import { FormatDateTime } from "../../../Shared/helpers";
import { IUserDataDto } from "../../../Api/Models";
import UserAvatar from "../UserAvatar/userAvatar";

interface IBinding
{
    bind: IProperties;
}

interface IProperties
{
    state: boolean;
    data: IUserDataDto;
    closeHandler: any;
}

const ApplicationUserInfoView = (props: IBinding): JSX.Element => 
{
    return (
        <Dialog fullWidth maxWidth="xs" open={props.bind?.state} onClose={props.bind?.closeHandler} aria-labelledby="dialog-title" aria-describedby="dialog-description">
            <DialogTitle id="dialog-title">
                <Grid container spacing={2} direction="column" alignItems="center">
                    <Grid item xs={12}>
                        <Box mt={1}>
                            <UserAvatar 
                                isLargeScale={true} 
                                avatarName={props.bind?.data.avatarName} 
                                userLetter={props.bind?.data.aliasName?.charAt(0).toUpperCase()} 
                            />
                        </Box>
                    </Grid>
                    <Grid item xs={12}>
                        <Typography component={"p"} variant={"h5"} >
                            {props.bind?.data.firstName} {props.bind?.data.lastName}
                        </Typography>
                    </Grid>
                </Grid>
            </DialogTitle>
            <Divider />
            <DialogContent>
                <Box pt={2}>
                    <Typography component={"p"} variant={"body1"} >
                        User alias: {props.bind?.data.aliasName}
                    </Typography>
                </Box>
                <Box pt={2}>
                    <Typography component={"p"} variant={"body1"} >
                        Registered: {FormatDateTime(props.bind?.data.registered, true)}
                    </Typography>
                </Box>
                <Box pt={2}>
                    <Typography component={"p"} variant={"body1"} >
                        Roles assigned:
                    </Typography>
                </Box>
                <List dense={true}>
                {props.bind?.data.roles?.map((item, index) => 
                (
                    <ListItem key={index}>
                        <ListItemIcon style={{ color: green[500] }}>
                            <CheckIcon />
                        </ListItemIcon>
                        <ListItemText>{item.name}</ListItemText>
                    </ListItem>
                ))}
                </List>
                <Box pt={0}>
                    <Typography component={"p"} variant={"body1"} >
                        Permissions assigned:
                    </Typography>
                </Box>                
                <List dense={true}>
                {props.bind?.data.permissions?.map((item, index) => 
                (
                    <ListItem key={index}>
                        <ListItemIcon style={{ color: green[500] }}>
                            <CheckIcon />
                        </ListItemIcon>
                        <ListItemText>{item.name}</ListItemText>
                    </ListItem>
                ))}
                </List>
            </DialogContent>
            <Divider />
            <DialogActions>
                <Button onClick={props.bind?.closeHandler} color="primary" autoFocus>
                    OK
                </Button>
            </DialogActions>
        </Dialog>
    );
}

export default ApplicationUserInfoView;
