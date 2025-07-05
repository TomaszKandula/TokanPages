import * as React from "react";
import { Link } from "react-router-dom";
import { ViewProperties } from "../../../../Shared/Abstractions";
import { Icon, Skeleton } from "../../../../Shared/Components";
import { UserSignoutProps } from "../userSignout";

interface UserSignoutViewProps extends ViewProperties, UserSignoutProps {
    languageId: string;
    caption: string;
    status: string;
    buttonText: string;
    isAnonymous: boolean;
}

export const UserSignoutView = (props: UserSignoutViewProps): React.ReactElement => {
    return (
        <section className={`section ${props.background} ?? ""`}>
            <div className="bulma-container bulma-is-max-tablet">
                <div className={!props.className ? "py-6" : props.className}>
                    <div className="bulma-card">
                        <div className="bulma-card-content">
                            <Skeleton isLoading={props.isLoading}>
                                <div className="is-flex is-flex-direction-column is-align-items-center">
                                    <Icon name="Account" size={3} className="account" />
                                    <p className="is-size-3 has-text-grey">
                                        {props.caption}
                                    </p>
                                </div>
                                <div className="has-text-centered mt-5">
                                    <p className="is-size-6 has-text-black">
                                        {props.status}
                                    </p>
                                </div>
                                <div className="mt-6 mb-3">
                                    <Link to={`/${props.languageId}`} className="link" rel="noopener nofollow">
                                        <button
                                            className="bulma-button bulma-is-link bulma-is-light bulma-is-fullwidth"
                                            disabled={props.isLoading || !props.isAnonymous}
                                        >
                                            {props.buttonText}
                                        </button>
                                    </Link>
                                </div>
                            </Skeleton>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    );
};
