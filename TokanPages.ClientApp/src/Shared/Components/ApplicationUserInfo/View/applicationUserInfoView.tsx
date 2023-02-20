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
import { green } from "@material-ui/core/colors";
import { List, ListItem, ListItemIcon, ListItemText } from "@material-ui/core";
import { GetDateTime } from "../../../../Shared/Services/Formatters";
import { AuthenticateUserResultDto, UserPermissionDto, UserRoleDto } from "../../../../Api/Models";
import { UserAvatar } from "../../UserAvatar";
import { ApplicationUserInfoStyle } from "./applicationUserInfoStyle";
import { v4 as uuidv4 } from "uuid";

interface Properties
{
    state: boolean;
    data: AuthenticateUserResultDto;
    closeHandler: () => void;
}

const Items = (props: { item: string, className: string }): JSX.Element => 
{
    return (
        <ListItem>
            <ListItemIcon style={{ color: green[500] }}>
                <CheckIcon />
            </ListItemIcon>
            <ListItemText>
                <Typography className={props.className}>{props.item}</Typography>
            </ListItemText>
        </ListItem>
    );
}

export const ApplicationUserInfoView = (props: Properties): JSX.Element => 
{
    const classes = ApplicationUserInfoStyle();
    const registered = GetDateTime(
    {
        value: props.data.registered,
        hasTimeVisible: true
    });

    return (
        <Dialog fullWidth maxWidth="xs" open={props.state} onClose={props.closeHandler} 
            aria-labelledby="dialog-title" aria-describedby="dialog-description">
            <DialogTitle id="dialog-title">
                <Grid container spacing={2} direction="column" alignItems="center">
                    <Grid item xs={12}>
                        <Box mt={1}>
                            <UserAvatar 
                                userId={props.data.userId}
                                isLarge={true} 
                                avatarName={props.data.avatarName} 
                                userLetter={props.data.aliasName?.charAt(0).toUpperCase()} 
                            />
                        </Box>
                    </Grid>
                    <Grid item xs={12}>
                        <Typography className={classes.fullname}>
                            {props.data.firstName} {props.data.lastName}
                        </Typography>
                    </Grid>
                </Grid>
            </DialogTitle>
            <Divider />
            <DialogContent>
                <Box pt={2}>
                    <Typography className={classes.item}>
                        User alias: <Typography component="span" className={classes.value}>{props.data.aliasName}</Typography>
                    </Typography>
                </Box>
                <Box pt={2}>
                    <Typography className={classes.item}>
                        Registered: <Typography component="span" className={classes.value}>{registered}</Typography>
                    </Typography>
                </Box>
                <Box pt={2}>
                    <Typography className={classes.item}>
                        Roles assigned:
                    </Typography>
                </Box>
                <List dense={true}>
                {props.data.roles?.map((item: UserRoleDto, _index: number) => 
                (
                    <Items item={item.name} key={item.id ?? uuidv4()} className={classes.value} />
                ))}
                </List>
                <Box pt={0}>
                    <Typography className={classes.item}>
                        Permissions assigned:
                    </Typography>
                </Box>                
                <List dense={true}>
                {props.data.permissions?.map((item: UserPermissionDto, _index: number) => 
                (
                    <Items item={item.name} key={item.id ?? uuidv4()} className={classes.value} />
                ))}
                </List>
            </DialogContent>
            <Divider />
            <DialogActions>
                <Button onClick={props.closeHandler} className={classes.button} autoFocus>OK</Button>
            </DialogActions>
        </Dialog>
    );
}
