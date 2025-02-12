import * as React from "react";
import Icon from "@material-ui/core/Icon";
import GitHub from "@material-ui/icons/GitHub";
import LinkedIn from "@material-ui/icons/LinkedIn";
import Instagram from "@material-ui/icons/Instagram";

interface Properties {
    iconName: string;
}

const GetNonMaterialIcon = (name: string): React.ReactElement | undefined => {
    switch (name) {
        case "github":
            return <GitHub />;
        case "linkedin":
            return <LinkedIn />;
        case "instagram":
            return <Instagram />;
        default:
            return undefined;
    }
};

const DisplayIcon = (args: { name: string }): React.ReactElement => {
    const nonMaterialIcon = GetNonMaterialIcon(args.name);
    if (nonMaterialIcon !== undefined) {
        return nonMaterialIcon;
    }

    return <Icon>{args.name}</Icon>;
};

export const GetIcon = (props: Properties): React.ReactElement => {
    let iconName = props.iconName !== "" ? props.iconName.toLowerCase() : "X";
    return <DisplayIcon name={iconName} />;
};
