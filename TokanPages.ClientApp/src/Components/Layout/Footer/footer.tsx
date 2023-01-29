import * as React from "react";
import { ContentFooterState } from "../../../Store/States";
import { Colours } from "../../../Theme";
import validate from "validate.js";
import { FooterView } from "./View/footerView";

interface Properties extends ContentFooterState
{
    backgroundColor?: string;
}

export const Footer = (props: Properties): JSX.Element => 
{
    const versionDateTime: string = process.env.REACT_APP_VERSION_DATE_TIME ?? "";
    const versionNumber: string = process.env.REACT_APP_VERSION_NUMBER ?? "";
    const versionInfo: string = `Version ${versionNumber} (${versionDateTime})`;

    const hasVersionInfo = validate.isEmpty(versionNumber) && validate.isEmpty(versionDateTime);

    const backgroundColor: string = !props.backgroundColor 
    ? Colours.colours.lightGray1 
    : props.backgroundColor;

    return (<FooterView
        terms={props.content?.terms}
        policy={props.content?.policy}
        versionInfo={versionInfo}
        hasVersionInfo={hasVersionInfo}
        backgroundColor={backgroundColor}
        copyright={props.content?.copyright}
        reserved={props.content?.reserved}
        icons={props.content?.icons}
    />);
}
