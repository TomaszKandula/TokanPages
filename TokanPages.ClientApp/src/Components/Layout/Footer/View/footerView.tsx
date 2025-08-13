import * as React from "react";
import { IconDto, LinkDto } from "../../../../Api/Models";
import { Icon, IconButton, Link, ProgressBar } from "../../../../Shared/Components";
import { v4 as uuidv4 } from "uuid";

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

interface LoaderProps {
    isLoading: boolean;
    children: React.ReactElement;
}

const Loader = (props: LoaderProps): React.ReactElement =>
    props.isLoading ? <ProgressBar size={32} /> : <>{props.children}</>;

export const FooterView = (props: Properties): React.ReactElement => (
    <>
        <hr className="line-separator" />
        <footer className="bulma-footer has-background-white">
            <Loader isLoading={props.isLoading}>
                <div className="bulma-content has-text-centered">
                    <div className="is-flex is-justify-content-center mb-4">
                        {props?.icons?.map((item: IconDto, _index: number) => (
                            <Link to={item.href} key={uuidv4()} aria-label={item.name}>
                                <IconButton>
                                    <figure className="is-flex is-align-self-center bulma-image bulma-is-24x24">
                                        <Icon name={item.name} size={1.5} />
                                    </figure>
                                </IconButton>
                            </Link>
                        ))}
                    </div>
                    <p className="is-size-6 is-flex is-flex-direction-column">
                        <span className="has-text-grey-dark my-2">{props?.legalInfo.copyright}</span>
                        <span className="has-text-grey-dark my-1">{props?.legalInfo.reserved}</span>
                        <Link to={props?.terms?.href ?? ""} className="has-text-grey-dark is-underlined my-1">
                            <span>{props?.terms?.text}</span>
                        </Link>
                        <Link to={props?.policy?.href ?? ""} className="has-text-grey-dark is-underlined my-1">
                            <span>{props?.policy?.text}</span>
                        </Link>
                    </p>
                    <p className="is-size-7 has-text-grey py-2">{props?.versionInfo}</p>
                </div>
            </Loader>
        </footer>
    </>
);
