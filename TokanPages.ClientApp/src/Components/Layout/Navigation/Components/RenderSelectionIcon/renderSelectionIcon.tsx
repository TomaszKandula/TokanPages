import * as React from "react";
import { LanguageSelectionProps } from "../../Types";
import { Icon } from "../../../../../Shared/Components";

export const RenderSelectionIcon = (props: LanguageSelectionProps): React.ReactElement | null => {
    const baseClass = props.className ?? "";

    if (props.languageId === props.selection) {
        return <Icon name="Check" size={props.size ?? 1.0} className={`${baseClass} has-text-link`} />;
    }

    return null;
};
