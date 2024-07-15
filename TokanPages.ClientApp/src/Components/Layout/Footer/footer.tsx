import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { Colours } from "../../../Theme";
import { ApplicationState } from "../../../Store/Configuration";
import validate from "validate.js";
import { FooterView } from "./View/footerView";
import { ApplicationNavbarAction } from "Store/Actions";

interface Properties {
    backgroundColor?: string;
}

export const Footer = (props: Properties): JSX.Element => {
    const dispatch = useDispatch();
    const versionDateTime: string = process.env.REACT_APP_VERSION_DATE_TIME ?? "";
    const versionNumber: string = process.env.REACT_APP_VERSION_NUMBER ?? "";
    const versionInfo: string = `Version ${versionNumber} (${versionDateTime})`;
    const hasVersionInfo = validate.isEmpty(versionNumber) && validate.isEmpty(versionDateTime);
    const backgroundColor: string = !props.backgroundColor ? Colours.colours.lightGray1 : props.backgroundColor;
    const footer = useSelector((state: ApplicationState) => state.contentFooter);

    const onTermsClickEvent = React.useCallback(() => {
        dispatch(ApplicationNavbarAction.set({ 
            selection: "footer_terms",
            name: footer?.content?.terms?.text,
            path: footer?.content?.terms?.href,
        }));
    }, [footer?.content?.terms?.text, footer?.content?.terms?.href]);

    const onPolicyClickEvent = React.useCallback(() => {
        dispatch(ApplicationNavbarAction.set({ 
            selection: "footer_policy",
            name: footer?.content?.policy?.text,
            path: footer?.content?.policy?.href,
        }));
    }, [footer?.content?.terms?.text, footer?.content?.terms?.href]);

    return (
        <FooterView
            terms={footer?.content?.terms}
            policy={footer?.content?.policy}
            versionInfo={versionInfo}
            hasVersionInfo={hasVersionInfo}
            backgroundColor={backgroundColor}
            copyright={footer?.content?.copyright}
            reserved={footer?.content?.reserved}
            icons={footer?.content?.icons}
            onTermsClickEvent={onTermsClickEvent}
            onPolicyClickEvent={onPolicyClickEvent}
        />
    );
};
