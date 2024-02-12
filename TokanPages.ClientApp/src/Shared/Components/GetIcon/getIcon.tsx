import * as React from "react";
import Icon from "@material-ui/core/Icon";
import GitHub from "@material-ui/icons/GitHub";
import LinkedIn from "@material-ui/icons/LinkedIn";

interface Properties {
    iconName: string;
}

const GetNonMaterialIcon = (name: string): JSX.Element | undefined => {
    switch (name) {
        case "github":
            return <GitHub />;
        case "linkedin":
            return <LinkedIn />;
        default:
            return undefined;
    }
};

const DisplayIcon = (args: { name: string }): JSX.Element => {
    const nonMaterialIcon = GetNonMaterialIcon(args.name);
    if (nonMaterialIcon !== undefined) {
        return nonMaterialIcon;
    }

    return <Icon>{args.name}</Icon>;
};

export const GetIcon = (props: Properties): JSX.Element => {
    let iconName = props.iconName !== "" ? props.iconName.toLowerCase() : "X";
    return <DisplayIcon name={iconName} />;
};
