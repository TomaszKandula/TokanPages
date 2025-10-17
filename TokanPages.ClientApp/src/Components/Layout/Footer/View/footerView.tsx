import * as React from "react";
import { IconDto } from "../../../../Api/Models";
import { Icon, IconButton, Link, ProgressBar } from "../../../../Shared/Components";
import { LoaderProps, FooterViewProps } from "../Types";
import { v4 as uuidv4 } from "uuid";

const Loader = (props: LoaderProps): React.ReactElement =>
    props.isLoading ? <ProgressBar size={32} /> : <>{props.children}</>;

export const FooterView = (props: FooterViewProps): React.ReactElement => (
    <>
        <hr className="line-separator" />
        <footer className="bulma-footer has-background-white">
            <Loader isLoading={props.isLoading}>
                <div className="bulma-content has-text-centered">
                    <div className="is-flex is-justify-content-center mb-4">
                        {props?.icons?.map((item: IconDto, _index: number) => (
                            <Link to={item.href} key={uuidv4()} aria-label={item.name}>
                                <IconButton size={3.0}>
                                    <figure className="is-flex is-align-self-center bulma-image bulma-is-24x24">
                                        <Icon name={item.name} size={1.5} className="has-text-black" />
                                    </figure>
                                </IconButton>
                            </Link>
                        ))}
                    </div>
                    <div className="is-size-6 is-flex is-flex-direction-column">
                        <span className="has-text-grey-dark my-2">{props?.legalInfo.copyright}</span>
                        <span className="has-text-grey-dark my-1">{props?.legalInfo.reserved}</span>
                        <div className="my-1">
                            <Link to={props?.terms?.href ?? ""}>
                                <span className="has-text-grey-dark is-underlined">{props?.terms?.text}</span>
                            </Link>
                        </div>
                        <div className="my-1">
                            <Link to={props?.policy?.href ?? ""}>
                                <span className="has-text-grey-dark is-underlined">{props?.policy?.text}</span>
                            </Link>
                        </div>
                    </div>
                    <p className="is-size-7 has-text-grey mt-2 py-2">{props?.versionInfo}</p>
                </div>
            </Loader>
        </footer>
    </>
);
