import React from "react";
import { NotificationProps } from "./types";
import { Icon } from "../Icon";
import { ProcessParagraphs } from "../RenderContent/Renderers";

const RenderIcon = (props: NotificationProps): React.ReactElement => {
    if (!props.hasIcon) {
        return <></>;
    }

    let iconName = "Information";
    switch (props.mode) {
        case "success":
            iconName = "CheckCircle";
            break;
        case "warning":
            iconName = "Alert";
            break;
        case "danger":
            iconName = "AlertCircle";
            break;
    }

    let style = "has-text-link";
    switch (props.mode) {
        case "link":
            style = `has-text-link${props.isLight ? "" : "-light"}`;
            break;
        case "primary":
            style = `has-text-primary${props.isLight ? "" : "-light"}`;
            break;
        case "info":
            style = `has-text-info${props.isLight ? "" : "-light"}`;
            break;
        case "success":
            style = `has-text-success${props.isLight ? "" : "-light"}`;
            break;
        case "warning":
            style = `has-text-warning${props.isLight ? "" : "-light"}`;
            break;
        case "danger":
            style = `has-text-danger${props.isLight ? "" : "-light"}`;
            break;
    }

    return <Icon name={iconName} size={1.5} className={style} />;
};

export const Notification = (props: NotificationProps): React.ReactElement => (
    <div
        className={`bulma-notification ${props.mode ?? ""} ${props.isLight ? "bulma-is-light" : ""} is-flex is-align-items-center ${props.className ?? ""}`}
    >
        {props.onClose ? <button className="bulma-delete"></button> : null}
        <RenderIcon {...props} />
        <div className={props.hasIcon ? "ml-4" : ""}>
            <ProcessParagraphs tag="span" html={props.text} className="is-size-6" />
        </div>
    </div>
);
