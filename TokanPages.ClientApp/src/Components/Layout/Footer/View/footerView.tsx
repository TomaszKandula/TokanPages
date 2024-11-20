import * as React from "react";
import { Link } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Typography from "@material-ui/core/Typography";
import IconButton from "@material-ui/core/IconButton";
import { GetIcon } from "../../../../Shared/Components/GetIcon/getIcon";
import { IconDto, LinkDto } from "../../../../Api/Models";
import { v4 as uuidv4 } from "uuid";
import Validate from "validate.js";

interface LegalInfoProps {
    copyright: string;
    reserved: string;
}

interface Properties {
    terms: LinkDto;
    policy: LinkDto;
    versionInfo: string;
    hasVersionInfo: boolean;
    legalInfo: LegalInfoProps;
    hasLegalInfo: boolean;
    icons: IconDto[];
    hasIcons: boolean;
    backgroundColor: string;
}

const SetTermsLink = (props: Properties): React.ReactElement => {
    if (Validate.isEmpty(props?.terms?.href)) {
        return <>{props?.terms?.text}</>;
    }

    return (
        <Link to={props?.terms?.href ?? ""} className="footer-links">
            {props?.terms?.text}
        </Link>
    );
};

const SetPolicyLink = (props: Properties): React.ReactElement => {
    if (Validate.isEmpty(props?.policy?.href)) {
        return <>{props?.policy?.text}</>;
    }

    return (
        <Link to={props?.policy?.href ?? ""} className="footer-links">
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
                    rel="noopener"
                >
                    <GetIcon iconName={item.name} />
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
                {props?.legalInfo.copyright} | {props?.legalInfo.reserved} | <SetTermsLink {...props} /> | <SetPolicyLink {...props} />
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
            <Container className="container-wide">
                <RenderCopyrightBar {...props} />
                <RenderVersionInfo {...props} />
                <RenderIconButtons {...props} />
                <div style={{ paddingBottom: 60 }} ></div>
            </Container>
        </footer>
    );
};
