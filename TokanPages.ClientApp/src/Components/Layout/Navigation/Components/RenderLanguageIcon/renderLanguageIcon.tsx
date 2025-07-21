import * as React from "react";
import { LanguageSelectionProps } from "../../Abstractions";
import { Icon } from "../../../../../Shared/Components";
import "./renderLanguageIcon.css";

export const RenderLanguageIcon = (props: LanguageSelectionProps): React.ReactElement | null => {
    if (props.languageId === props.selection) {
        return <Icon name="Check" size={1.5} className="navigation-languages-check" />;
    }

    return null;
};
