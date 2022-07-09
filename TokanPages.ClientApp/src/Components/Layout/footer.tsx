import * as React from "react";
import { IGetFooterContent } from "../../Redux/States/Content/getFooterContentState";
import { CustomColours } from "../../Theme/customColours";
import validate from "validate.js";
import FooterView from "./footerView";

interface IGetFooterContentExtended extends IGetFooterContent
{
    backgroundColor?: string;
}

const Footer = (props: IGetFooterContentExtended): JSX.Element => 
{
    const versionDateTime: string = process.env.REACT_APP_VERSION_DATE_TIME ?? "";
    const versionNumber: string = process.env.REACT_APP_VERSION_NUMBER ?? "";
    const versionInfo: string = `Version ${versionNumber} (${versionDateTime})`;

    const hasVersionInfo = validate.isEmpty(versionNumber) && validate.isEmpty(versionDateTime);

    const backgroundColor: string = !props.backgroundColor 
        ? CustomColours.colours.lightGray1 
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

export default Footer;
