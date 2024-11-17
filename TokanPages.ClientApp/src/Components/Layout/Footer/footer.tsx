import * as React from "react";
import { useSelector } from "react-redux";
import { ApplicationState } from "../../../Store/Configuration";
import { FooterView } from "./View/footerView";
import validate from "validate.js";

interface Properties {
    backgroundColor?: string;
}

export const Footer = (props: Properties): React.ReactElement => {
    const versionDateTime: string = process.env.REACT_APP_VERSION_DATE_TIME ?? "";
    const versionNumber: string = process.env.REACT_APP_VERSION_NUMBER ?? "";
    const versionInfo: string = `Version ${versionNumber} (${versionDateTime})`;
    const hasVersionInfo = !validate.isEmpty(versionNumber) && !validate.isEmpty(versionDateTime);
    const backgroundColor: string = !props.backgroundColor ? "#FAFAFA" : props.backgroundColor;

    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const footer = data?.components?.footer;
    const hasLegalInfo = !validate.isEmpty(footer?.copyright) && !validate.isEmpty(footer?.reserved);

    return (
        <FooterView
            terms={footer?.terms}
            policy={footer?.policy}
            versionInfo={versionInfo}
            hasVersionInfo={hasVersionInfo}
            legalInfo={{ 
                copyright: footer?.copyright,
                reserved: footer?.reserved
            }}
            hasLegalInfo={hasLegalInfo}
            icons={footer?.icons}
            hasIcons={footer?.icons.length > 0}
            backgroundColor={backgroundColor}
        />
    );
};
