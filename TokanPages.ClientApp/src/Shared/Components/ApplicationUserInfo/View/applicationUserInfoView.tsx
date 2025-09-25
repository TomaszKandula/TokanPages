import * as React from "react";
import { GetDateTime } from "../../../../Shared/Services/Formatters";
import { Icon } from "../../../../Shared/Components";
import { FigoureSize } from "../../../../Shared/enums";
import { AuthenticateUserResultDto, UserInfoProps, UserPermissionDto, UserRoleDto } from "../../../../Api/Models";
import { UserAvatar } from "../../UserAvatar";
import { v4 as uuidv4 } from "uuid";

interface ApplicationUserInfoViewProps {
    isOpen: boolean;
    content: UserInfoProps;
    data: AuthenticateUserResultDto;
    disablePortal?: boolean;
    hideBackdrop?: boolean;
    closeHandler: () => void;
}

interface ItemsProps {
    item: string;
}

const CustomListItem = (props: ItemsProps): React.ReactElement => {
    return (
        <li>
            <div className="is-flex">
                <p className="is-size-6 m-1">{props.item}</p>
                <Icon name="Check" size={1.5} className="has-text-primary-35 m-1" />
            </div>
        </li>
    );
};

export const ApplicationUserInfoView = (props: ApplicationUserInfoViewProps): React.ReactElement => {
    const registered = GetDateTime({
        value: props.data.registered,
        hasTimeVisible: true,
    });

    return (
        <div className={`bulma-modal ${props.isOpen ? "bulma-is-active" : ""}`}>
            <div className="bulma-modal-background"></div>
            <div className="bulma-modal-card py-6 my-6">
                <header className="bulma-modal-card-head">
                    <UserAvatar
                        userId={props.data?.userId}
                        size={FigoureSize.large}
                        avatarName={props.data?.avatarName}
                        userLetter={props.data?.aliasName?.charAt(0).toUpperCase()}
                    />
                    <p className="bulma-modal-card-title p-3">
                        {props.data?.firstName} {props.data?.lastName}
                    </p>
                    <button className="bulma-delete" aria-label="close" onClick={props.closeHandler}></button>
                </header>
                <section className="bulma-modal-card-body">
                    <div className="bulma-content">
                        <div className="is-flex">
                            <p className="is-size-6">{props.content?.textUserAlias}:</p>
                            <p className="is-size-6 pl-2 has-text-weight-semibold">{props.data?.aliasName}</p>
                        </div>
                        <div className="is-flex">
                            <p className="is-size-6">{props.content?.textRegistered}:</p>
                            <span className="is-size-6 pl-2 has-text-weight-semibold">{registered}</span>
                        </div>
                        <p className="is-size-6">{props.content?.textRoles}:</p>
                        <ul>
                            {props.data.roles?.map((item: UserRoleDto, _index: number) => (
                                <CustomListItem item={item.name} key={item.id ?? uuidv4()} />
                            ))}
                        </ul>
                        <p className="is-size-6">{props.content?.textPermissions}:</p>
                        <ul>
                            {props.data.permissions?.map((item: UserPermissionDto, _index: number) => (
                                <CustomListItem item={item.name} key={item.id ?? uuidv4()} />
                            ))}
                        </ul>
                    </div>
                </section>
                <footer className="bulma-modal-card-foot is-justify-content-flex-end">
                    <div className="bulma-buttons">
                        <button className="bulma-button bulma-is-link bulma-is-light" onClick={props.closeHandler}>
                            OK
                        </button>
                    </div>
                </footer>
            </div>
        </div>
    );
};
