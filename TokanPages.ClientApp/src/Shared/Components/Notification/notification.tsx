import React from "react";
import { NotificationProps } from "./types";
import { Icon } from "../Icon";
import { ProcessParagraphs } from "../RenderContent/Renderers";

export const Notification = (props: NotificationProps): React.ReactElement => (
    <div className="bulma-notification is-flex is-align-items-center">
        <Icon name="Information" size={1.5} className="has-text-link" />
        <ProcessParagraphs tag="span" html={props.text} className="is-size-6 ml-4" />
    </div>
);
