import React from "react";
import { GET_IMAGES_URL } from "../../../../../Api";
import { Skeleton, Image, Link, Icon } from "../../../../../Shared/Components";
import { ResumeViewProps } from "../../../Types";

const ICON_SIZE = 1.2;

export const HeaderSmall = (props: ResumeViewProps): React.ReactElement => (
    <>
        <Skeleton isLoading={props.isLoading} mode="Text" width={100}>
            <h2 className="is-size-4 has-text-centered has-text-grey-dark has-text-weight-medium is-capitalized">
                {props.page?.resume?.header?.fullName}
            </h2>
        </Skeleton>
        <div className="is-flex is-gap-3.5 is-align-items-center is-justify-content-center">
            <Skeleton isLoading={props.isLoading} mode="Circle" width={64} height={64} disableMarginY>
                <figure className="bulma-image bulma-is-64x64">
                    <Image
                        base={GET_IMAGES_URL}
                        source={props.page?.photo?.href}
                        title={props.page?.photo?.text}
                        alt={props.page?.photo?.text}
                        loading="eager"
                        className="bulma-is-rounded is-round-border-light"
                    />
                </figure>
            </Skeleton>
            <div className="is-flex is-flex-direction-column">
                <Skeleton isLoading={props.isLoading} mode="Text" width={100}>
                    <div className="is-flex is-gap-1.5">
                        <Icon name="Phone" size={ICON_SIZE} className="has-text-grey" />
                        <p className="is-size-6 has-text-grey-dark is-lowercase">
                            {props.page?.resume?.header?.mobilePhone}
                        </p>
                    </div>
                </Skeleton>
                <Skeleton isLoading={props.isLoading} mode="Text" width={100}>
                    <div className="is-flex is-gap-1.5">
                        <Icon name="Web" size={ICON_SIZE} className="has-text-grey" />
                        <Link to={props.page?.resume?.header?.www.href} className="is-size-6 is-underlined">
                            <p className="is-lowercase">{props.page?.resume?.header?.www.text}</p>
                        </Link>
                    </div>
                </Skeleton>
            </div>
            <div className="is-flex is-flex-direction-column">
                <Skeleton isLoading={props.isLoading} mode="Text" width={100}>
                    <div className="is-flex is-gap-1.5">
                        <Icon name="Email" size={ICON_SIZE} className="has-text-grey" />
                        <Link to={`mailto:${props.page?.resume?.header?.email}`} className="is-size-6 is-underlined">
                            <p className="is-lowercase">{props.page?.resume?.header?.email}</p>
                        </Link>
                    </div>
                </Skeleton>
                <Skeleton isLoading={props.isLoading} mode="Text" width={100}>
                    <div className="is-flex is-gap-1.5">
                        <Icon name="GitHub" size={ICON_SIZE} className="has-text-grey" />
                        <Link to={props.page?.resume?.header?.github.href} className="is-size-6 is-underlined">
                            <p className="is-lowercase">{props.page?.resume?.header?.github.text}</p>
                        </Link>
                    </div>
                </Skeleton>
            </div>
        </div>
        <hr className="mt-4" />
    </>
);
