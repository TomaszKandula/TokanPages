import * as React from "react";
import { Link } from "react-router-dom";
import { ViewProperties } from "../../../../Shared/Abstractions";
import { CustomCard, Icon, Skeleton } from "../../../../Shared/Components";
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
        <section className={props.className}>
            <div className="bulma-container bulma-is-max-tablet">
                <div className="py-6">
                    <CustomCard
                        isLoading={props.isLoading}
                        caption={props.caption}
                        text={[props.status]}
                        icon={<Icon name="Check" size={3} />}
                        colour="has-text-success"
                        externalButton={
                            <div className="mt-6 mb-3">
                                <Skeleton isLoading={props.isLoading} mode="Rect">
                                    <Link to={`/${props.languageId}`} className="link" rel="noopener nofollow">
                                        <button className={buttonClass} disabled={isDisabled}>
                                            {props.buttonText}
                                        </button>
                                    </Link>
                                </Skeleton>
                            </div>
                        }
                    />
                </div>
            </div>
        </section>
    );
};
