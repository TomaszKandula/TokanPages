import React from "react";
import { NotificationProps } from "./types";
import { Icon } from "../Icon";
import { RenderHtml } from "../RenderHtml";

export const Notification = (props: NotificationProps): React.ReactElement => (
    <div className="bulma-notification is-flex is-align-items-center">
        <Icon name="Information" size={1.5} className="has-text-link" />
        <RenderHtml value={props.text} tag="span" className="is-size-6 ml-4" />
    </div>
);
