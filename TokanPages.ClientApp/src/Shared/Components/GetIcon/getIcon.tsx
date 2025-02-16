import * as React from "react";
import Icon from "@material-ui/core/Icon";
import GitHub from "@material-ui/icons/GitHub";
import LinkedIn from "@material-ui/icons/LinkedIn";
import Instagram from "@material-ui/icons/Instagram";

interface Properties {
    name: string;
    className?: string;
}

const GetNonMaterialIcon = (props: Properties): React.ReactElement | undefined => {
    switch (props.name) {
        case "github":
            return <GitHub className={props.className} />;
        case "linkedin":
            return <LinkedIn className={props.className} />;
        case "instagram":
            return <Instagram className={props.className} />;
        default:
            return undefined;
    }
};

const DisplayIcon = (props: Properties): React.ReactElement => {
    const nonMaterialIcon = GetNonMaterialIcon({ 
        name: props.name, 
        className: props.className 
    });

    if (nonMaterialIcon !== undefined) {
        return nonMaterialIcon;
    }

    return <Icon>{props.name}</Icon>;
};

export const GetIcon = (props: Properties): React.ReactElement => {
    let iconName = props.name !== "" ? props.name?.toLowerCase() : "X";
    return <DisplayIcon name={iconName} className={props.className} />;
};
