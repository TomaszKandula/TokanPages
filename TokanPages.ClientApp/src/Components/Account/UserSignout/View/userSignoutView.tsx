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
    const buttonClass = "bulma-button bulma-is-link bulma-is-light bulma-is-fullwidth";
    const isDisabled = props.isLoading || !props.isAnonymous;

    return (
        <section className={props.background}>
            <div className="bulma-container bulma-is-max-tablet">
                <div className={!props.className ? "py-6" : props.className}>
                    <div className="bulma-card">
                        <div className="bulma-card-content">
                            <div className="is-flex is-flex-direction-column is-align-items-center">
                                <Skeleton isLoading={props.isLoading} mode="Circle" width={72} height={72}>
                                    <Icon name="AccountCircle" size={3} className="has-text-link" />
                                </Skeleton>
                                <Skeleton isLoading={props.isLoading}>
                                    <p className="is-size-3 has-text-grey">{props.caption}</p>
                                </Skeleton>
                            </div>
                            <div className="has-text-centered mt-5">
                                <Skeleton isLoading={props.isLoading}>
                                    <p className="is-size-6 has-text-black">{props.status}</p>
                                </Skeleton>
                            </div>
                            <div className="mt-6 mb-3">
                                <Skeleton isLoading={props.isLoading} mode="Rect">
                                    <Link to={`/${props.languageId}`} className="link" rel="noopener nofollow">
                                        <button className={buttonClass} disabled={isDisabled}>
                                            {props.buttonText}
                                        </button>
                                    </Link>
                                </Skeleton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    );
};
