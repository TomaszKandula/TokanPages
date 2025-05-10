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
import { List, ListItem, ListItemIcon, ListItemText } from "@material-ui/core";
import { GetDateTime } from "../../../../Shared/Services/Formatters";
import { AuthenticateUserResultDto, UserInfoProps, UserPermissionDto, UserRoleDto } from "../../../../Api/Models";
import { UserAvatar } from "../../UserAvatar";
import { v4 as uuidv4 } from "uuid";
import "./applicationUserInfoView.css";

interface ApplicationUserInfoViewProps {
    state: boolean;
    content: UserInfoProps;
    data: AuthenticateUserResultDto;
    disablePortal?: boolean;
    hideBackdrop?: boolean;
    closeHandler: () => void;
}

interface ItemsProps {
    item: string;
    className: string;
}

const CustomListItem = (props: ItemsProps): React.ReactElement => {
    return (
        <ListItem>
            <ListItemIcon className="colour-green-2">
                <CheckIcon />
            </ListItemIcon>
            <ListItemText>
                <Typography className={props.className}>{props.item}</Typography>
            </ListItemText>
        </ListItem>
    );
};

export const ApplicationUserInfoView = (props: ApplicationUserInfoViewProps): React.ReactElement => {
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
            disablePortal={props.disablePortal}
            hideBackdrop={props.hideBackdrop}
            aria-labelledby="dialog-title"
            aria-describedby="dialog-description"
        >
            <DialogTitle id="dialog-title">
                <Grid container spacing={2} direction="column" alignItems="center">
                    <Grid item xs={12}>
                        <div className="mt-8">
                            <UserAvatar
                                userId={props.data?.userId}
                                isLarge={true}
                                avatarName={props.data?.avatarName}
                                userLetter={props.data?.aliasName?.charAt(0).toUpperCase()}
                            />
                        </div>
                    </Grid>
                    <Grid item xs={12}>
                        <Typography className="user-info-fullname">
                            {props.data?.firstName} {props.data?.lastName}
                        </Typography>
                    </Grid>
                </Grid>
            </DialogTitle>
            <Divider />
            <DialogContent>
                <Typography className="user-info-item">
                    {props.content?.textUserAlias}:&nbsp;
                    <Typography component="span" className="user-info-value">
                        {props.data?.aliasName}
                    </Typography>
                </Typography>
                <Typography className="user-info-item">
                    {props.content?.textRegistered}:&nbsp;
                    <Typography component="span" className="user-info-value">
                        {registered}
                    </Typography>
                </Typography>
                <Typography className="user-info-item">{props.content?.textRoles}:&nbsp;</Typography>
                <List dense={true}>
                    {props.data.roles?.map((item: UserRoleDto, _index: number) => (
                        <CustomListItem item={item.name} key={item.id ?? uuidv4()} className="user-info-value" />
                    ))}
                </List>
                <Typography className="user-info-item">{props.content?.textPermissions}:&nbsp;</Typography>
                <List dense={true}>
                    {props.data.permissions?.map((item: UserPermissionDto, _index: number) => (
                        <CustomListItem item={item.name} key={item.id ?? uuidv4()} className="user-info-value" />
                    ))}
                </List>
            </DialogContent>
            <Divider />
            <DialogActions>
                <Button onClick={props.closeHandler} className="button" autoFocus>
                    OK
                </Button>
            </DialogActions>
        </Dialog>
    );
};
