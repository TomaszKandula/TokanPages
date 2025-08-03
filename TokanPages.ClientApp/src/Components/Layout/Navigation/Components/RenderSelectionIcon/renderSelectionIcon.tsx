import * as React from "react";
import { LanguageSelectionProps } from "../../Abstractions";
import { Icon } from "../../../../../Shared/Components";

export const RenderSelectionIcon = (props: LanguageSelectionProps): React.ReactElement | null => {
    if (props.languageId === props.selection) {
        return <Icon name="Check" size={props.size ?? 0.75} className="has-text-link" />;
    }

    return null;
};
