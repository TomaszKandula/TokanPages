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
import { AuthenticateUserResultDto, UserInfoProps, UserPermissionDto, UserRoleDto } from "../../../../Api/Models";
import { UserAvatar } from "../../UserAvatar";
import { ApplicationUserInfoStyle } from "./applicationUserInfoStyle";
import { v4 as uuidv4 } from "uuid";

interface ApplicationUserInfoViewProps {
    state: boolean;
    content: UserInfoProps;
    data: AuthenticateUserResultDto;
    closeHandler: () => void;
}

interface ItemsProps {
    item: string;
    className: string;
}

const CustomListItem = (props: ItemsProps): JSX.Element => {
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
};

export const ApplicationUserInfoView = (props: ApplicationUserInfoViewProps): JSX.Element => {
    const classes = ApplicationUserInfoStyle();
    const registered = GetDateTime({
        value: props.data.registered,
        hasTimeVisible: true,
    });

    return (
        <Dialog
            fullWidth
            maxWidth="xs"
            open={props.state}
            onClose={props.closeHandler}
            aria-labelledby="dialog-title"
            aria-describedby="dialog-description"
        >
            <DialogTitle id="dialog-title">
                <Grid container spacing={2} direction="column" alignItems="center">
                    <Grid item xs={12}>
                        <Box mt={1}>
                            <UserAvatar
                                userId={props.data?.userId}
                                isLarge={true}
                                avatarName={props.data?.avatarName}
                                userLetter={props.data?.aliasName?.charAt(0).toUpperCase()}
                            />
                        </Box>
                    </Grid>
                    <Grid item xs={12}>
                        <Typography className={classes.fullname}>
                            {props.data?.firstName} {props.data?.lastName}
                        </Typography>
                    </Grid>
                </Grid>
            </DialogTitle>
            <Divider />
            <DialogContent>
                <Typography className={classes.item}>
                    {props.content?.textUserAlias}:&nbsp;
                    <Typography component="span" className={classes.value}>
                        {props.data?.aliasName}
                    </Typography>
                </Typography>
                <Typography className={classes.item}>
                    {props.content?.textRegistered}:&nbsp;
                    <Typography component="span" className={classes.value}>
                        {registered}
                    </Typography>
                </Typography>
                <Typography className={classes.item}>{props.content?.textRoles}:&nbsp;</Typography>
                <List dense={true}>
                    {props.data.roles?.map((item: UserRoleDto, _index: number) => (
                        <CustomListItem item={item.name} key={item.id ?? uuidv4()} className={classes.value} />
                    ))}
                </List>
                <Typography className={classes.item}>{props.content?.textPermissions}:&nbsp;</Typography>
                <List dense={true}>
                    {props.data.permissions?.map((item: UserPermissionDto, _index: number) => (
                        <CustomListItem item={item.name} key={item.id ?? uuidv4()} className={classes.value} />
                    ))}
                </List>
            </DialogContent>
            <Divider />
            <DialogActions>
                <Button onClick={props.closeHandler} className={classes.button} autoFocus>
                    OK
                </Button>
            </DialogActions>
        </Dialog>
    );
};
