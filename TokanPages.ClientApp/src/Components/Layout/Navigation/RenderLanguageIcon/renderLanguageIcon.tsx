import * as React from "react";
import Icon from "@mdi/react";
import { mdiCheck } from "@mdi/js";
import { LanguageSelectionProps } from "../Abstractions";

export const RenderLanguageIcon = (props: LanguageSelectionProps): React.ReactElement | null => {
    if (props.languageId === props.selection) {
        return <Icon path={mdiCheck} size={1.5} className="navigation-languages-check" />;
    }

    return null;
};
