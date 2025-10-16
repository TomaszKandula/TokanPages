import React from "react";
import { GET_IMAGES_URL } from "../../../../../Api";
import { Skeleton, Image, Link } from "../../../../../Shared/Components";
import { ResumeViewProps } from "../../../Types";

export const HeaderSmall = (props: ResumeViewProps): React.ReactElement => (
    <>
        <hr className="mb-2" />
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
                    <p className="is-size-6 has-text-grey-dark has-text-weight-bold is-capitalized">
                        {props.page?.resume?.header?.fullName}
                    </p>
                </Skeleton>
                <Skeleton isLoading={props.isLoading} mode="Text" width={100}>
                    <p className="is-size-6 has-text-grey-dark is-lowercase">
                        {props.page?.resume?.header?.mobilePhone}
                    </p>
                </Skeleton>
            </div>
            <div className="is-flex is-flex-direction-column">
                <Skeleton isLoading={props.isLoading} mode="Text" width={100}>
                    <Link to={`mailto:${props.page?.resume?.header?.email}`} className="is-size-6 is-underlined">
                        <p className="is-lowercase">{props.page?.resume?.header?.email}</p>
                    </Link>
                </Skeleton>
                <Skeleton isLoading={props.isLoading} mode="Text" width={100}>
                    <Link to={props.page?.resume?.header?.github.href} className="is-size-6 is-underlined">
                        <p className="is-lowercase">{props.page?.resume?.header?.github.text}</p>
                    </Link>
                </Skeleton>
            </div>
        </div>
        <hr className="mt-2" />
    </>
);
