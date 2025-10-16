import React from "react";
import { GET_IMAGES_URL } from "../../../../../Api";
import { Skeleton, Image, Link } from "../../../../../Shared/Components";
import { ResumeViewProps } from "../../../Types";

export const HeaderLarge = (props: ResumeViewProps): React.ReactElement => (
    <div className="is-flex is-gap-2.5 mb-4">
        <div className="bulma-cell is-align-content-center">
            <Skeleton isLoading={props.isLoading} mode="Circle" width={128} height={128} disableMarginY>
                <figure className="bulma-image bulma-is-128x128">
                    <Image
                        base={GET_IMAGES_URL}
                        source={props.page?.photo?.href}
                        title={props.page?.photo?.text}
                        alt={props.page?.photo?.text}
                        className="bulma-is-rounded is-round-border-light"
                    />
                </figure>
            </Skeleton>
        </div>
        <div className="bulma-cell is-align-content-center">
            <Skeleton isLoading={props.isLoading} mode="Text" width={100}>
                <p className="is-size-4 has-text-grey-dark has-text-weight-bold is-capitalized">
                    {props.page?.resume?.header?.fullName}
                </p>
            </Skeleton>
            <Skeleton isLoading={props.isLoading} mode="Text" width={100}>
                <p className="is-size-6 has-text-grey-dark is-lowercase">{props.page?.resume?.header?.mobilePhone}</p>
            </Skeleton>
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
);
