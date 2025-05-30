import * as React from "react";
import { IconDto, LinkDto } from "../../../../Api/Models";
import { Icon, IconButton, ProgressBar, Link } from "../../../../Shared/Components";
import { v4 as uuidv4 } from "uuid";
import Validate from "validate.js";
import "./footerView.css";

interface LegalInfoProps {
    copyright: string;
    reserved: string;
}

interface Properties {
    isLoading: boolean;
    terms: LinkDto;
    policy: LinkDto;
    versionInfo: string;
    hasVersionInfo: boolean;
    legalInfo: LegalInfoProps;
    hasLegalInfo: boolean;
    icons: IconDto[];
    hasIcons: boolean;
}

const SetTermsLink = (props: Properties): React.ReactElement => {
    if (Validate.isEmpty(props?.terms?.href)) {
        return <>{props?.terms?.text}</>;
    }

    return (
        <Link to={props?.terms?.href ?? ""} className="footer-links">
            <span>{props?.terms?.text}</span>
        </Link>
    );
};

const SetPolicyLink = (props: Properties): React.ReactElement => {
    if (Validate.isEmpty(props?.policy?.href)) {
        return <>{props?.policy?.text}</>;
    }

    return (
        <Link to={props?.policy?.href ?? ""} className="footer-links">
            <span>{props?.policy?.text}</span>
        </Link>
    );
};

const RenderIconButtons = (props: Properties): React.ReactElement | null => {
    const icons = (
        <div className="footer-icon-box footer-centred">
            {props?.icons?.map((item: IconDto, _index: number) => (
                <Link 
                    to={item.href}
                    key={uuidv4()}
                    aria-label={item.name}
                >
                    <IconButton>
                        <Icon name={item.name} size={48} className="footer-icon" />
                    </IconButton>
                </Link>
            ))}
        </div>
    );

    return props.hasIcons ? icons : null;
};

const RenderCopyrightBar = (props: Properties): React.ReactElement | null => {
    const legalInformation = (
        <div className="footer-copyright-box footer-centred">
            <div className="footer-copyright">
                {props?.legalInfo.copyright} | {props?.legalInfo.reserved} | <SetTermsLink {...props} /> |{" "}
                <SetPolicyLink {...props} />
            </div>
        </div>
    );

    return props.hasLegalInfo ? legalInformation : null;
};

const RenderVersionInfo = (props: Properties): React.ReactElement | null => {
    const applicationVersionInfo = (
        <div className="footer-centred">
            <div className="footer-version">{props?.versionInfo}</div>
        </div>
    );

    return props.hasVersionInfo ? applicationVersionInfo : null;
};

export const FooterView = (props: Properties): React.ReactElement => {
    return (
        <footer className="footer-page-footer">
            {props.isLoading ? (
                <ProgressBar classNameWrapper="p-25" classNameColour="colour-white" size={32} />
            ) : (
                <div className="bulma-container container-wide">
                    <RenderCopyrightBar {...props} />
                    <RenderVersionInfo {...props} />
                    <RenderIconButtons {...props} />
                    <div className="pb-64"></div>
                </div>
            )}
        </footer>
    );
};
