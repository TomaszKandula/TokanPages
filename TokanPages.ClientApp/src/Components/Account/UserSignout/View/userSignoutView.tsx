import * as React from "react";
import { ViewProperties } from "../../../../Shared/Abstractions";
import { CustomCard, Icon } from "../../../../Shared/Components";
import { UserSignoutProps } from "../userSignout";

interface UserSignoutViewProps extends ViewProperties, UserSignoutProps {
    languageId: string;
    caption: string;
    status: string[];
    buttonText: string;
    isAnonymous: boolean;
}

export const UserSignoutView = (props: UserSignoutViewProps): React.ReactElement => (
        <section className={props.className}>
            <div className="bulma-container bulma-is-max-tablet">
                <div className="py-6">
                    <CustomCard
                        isLoading={props.isLoading}
                        caption={props.caption}
                        text={props.status}
                        icon={<Icon name="Check" size={3} />}
                        colour="has-text-success"
                    />
                </div>
            </div>
        </section>
    );
