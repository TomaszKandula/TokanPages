import * as React from "react";
import Icon from '@material-ui/core/Icon';

interface IProperty
{
    iconName: string;
}

export const GetIcon = (props: IProperty): JSX.Element =>
{
    return(
        <Icon>
            {props.iconName !== "" 
            ? props.iconName.toLowerCase() 
            : "star"}
        </Icon>);
}
