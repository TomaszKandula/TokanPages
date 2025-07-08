import * as React from "react";
import { IconDto, LinkDto } from "../../../../Api/Models";
import { Icon, IconButton, Link, ProgressBar } from "../../../../Shared/Components";
import { v4 as uuidv4 } from "uuid";
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
    className?: string;
}

const SetTermsLink = (props: Properties): React.ReactElement => (
    <Link to={props?.terms?.href ?? ""} className="footer-links ml-2 mr-2">
        <span>{props?.terms?.text}</span>
    </Link>
);

const SetPolicyLink = (props: Properties): React.ReactElement => (
    <Link to={props?.policy?.href ?? ""} className="footer-links ml-2 mr-2">
        <span>{props?.policy?.text}</span>
    </Link>
);

export const FooterView = (props: Properties): React.ReactElement => {
    return (
        <footer className={`bulma-footer ${props.className ?? ""}`}>
            {props.isLoading 
            ? <ProgressBar size={32} /> 
            : <div className="bulma-content has-text-centered">
                <p className="is-size-5">
                    <span className="ml-2 mr-2">
                        {props?.legalInfo.copyright}
                    </span>
                    <span>|</span>
                    <span className="ml-2 mr-2">
                        {props?.legalInfo.reserved}
                    </span>
                    <span>|</span>
                    <SetTermsLink {...props} />
                    <span>|</span>
                    <SetPolicyLink {...props} />
                </p>
                <p className="is-size-6">
                    {props?.versionInfo}
                </p>
                <div className="is-flex is-justify-content-center">
                {props?.icons?.map((item: IconDto, _index: number) => (
                    <Link 
                        to={item.href}
                        key={uuidv4()}
                        aria-label={item.name}
                    >
                        <IconButton>
                            <Icon name={item.name} size={1.5} />
                        </IconButton>
                    </Link>
                ))}
                </div>
            </div>}
        </footer>
    );
};
