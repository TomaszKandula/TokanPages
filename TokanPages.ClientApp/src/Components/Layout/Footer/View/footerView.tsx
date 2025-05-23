import * as React from "react";
import { Link } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Typography from "@material-ui/core/Typography";
import IconButton from "@material-ui/core/IconButton";
import { GetIcon } from "../../../../Shared/Components/GetIcon/getIcon";
import { IconDto, LinkDto } from "../../../../Api/Models";
import { ProgressBar } from "../../../../Shared/Components";
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
        <Link to={props?.terms?.href ?? ""} className="footer-links" rel="noopener nofollow">
            {props?.terms?.text}
        </Link>
    );
};

const SetPolicyLink = (props: Properties): React.ReactElement => {
    if (Validate.isEmpty(props?.policy?.href)) {
        return <>{props?.policy?.text}</>;
    }

    return (
        <Link to={props?.policy?.href ?? ""} className="footer-links" rel="noopener nofollow">
            {props?.policy?.text}
        </Link>
    );
};

const RenderIconButtons = (props: Properties): React.ReactElement | null => {
    const icons = (
        <div className="footer-icon-box footer-centred">
            {props?.icons?.map((item: IconDto, _index: number) => (
                <IconButton
                    className="footer-icon"
                    aria-label={item.name}
                    href={item.href}
                    key={uuidv4()}
                    color="default"
                    target="_blank"
                    rel="noopener nofollow"
                >
                    <GetIcon name={item.name} />
                </IconButton>
            ))}
        </div>
    );

    return props.hasIcons ? icons : null;
};

const RenderCopyrightBar = (props: Properties): React.ReactElement | null => {
    const legalInformation = (
        <div className="footer-copyright-box footer-centred">
            <Typography className="footer-copyright">
                {props?.legalInfo.copyright} | {props?.legalInfo.reserved} | <SetTermsLink {...props} /> |{" "}
                <SetPolicyLink {...props} />
            </Typography>
        </div>
    );

    return props.hasLegalInfo ? legalInformation : null;
};

const RenderVersionInfo = (props: Properties): React.ReactElement | null => {
    const applicationVersionInfo = (
        <div className="footer-centred">
            <Typography className="footer-version">{props?.versionInfo}</Typography>
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
                <Container className="container-wide">
                    <RenderCopyrightBar {...props} />
                    <RenderVersionInfo {...props} />
                    <RenderIconButtons {...props} />
                    <div className="pb-64"></div>
                </Container>
            )}
        </footer>
    );
};
