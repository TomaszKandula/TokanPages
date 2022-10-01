import * as React from "react";
import { IContentFooter } from "../../../Store/States";
import { Colours } from "../../../Theme";
import validate from "validate.js";
import { FooterView } from "./View/footerView";

interface IGetFooterContentExtended extends IContentFooter
{
    backgroundColor?: string;
}

export const Footer = (props: IGetFooterContentExtended): JSX.Element => 
{
    const versionDateTime: string = process.env.REACT_APP_VERSION_DATE_TIME ?? "";
    const versionNumber: string = process.env.REACT_APP_VERSION_NUMBER ?? "";
    const versionInfo: string = `Version ${versionNumber} (${versionDateTime})`;

    const hasVersionInfo = validate.isEmpty(versionNumber) && validate.isEmpty(versionDateTime);

    const backgroundColor: string = !props.backgroundColor 
        ? Colours.colours.lightGray1 
        : props.backgroundColor;

    return (<FooterView bind=
    {{
        terms: props.content?.terms,
        policy: props.content?.policy,
        versionInfo: versionInfo,
        hasVersionInfo: hasVersionInfo,
        backgroundColor: backgroundColor,
        copyright: props.content?.copyright,
        reserved: props.content?.reserved,
        icons: props.content?.icons
    }}/>);
}
