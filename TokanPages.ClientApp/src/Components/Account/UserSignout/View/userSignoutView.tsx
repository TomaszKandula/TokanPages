import * as React from "react";
import { CustomCard, Icon } from "../../../../Shared/Components";
import { UserSignoutViewProps } from "../Types";

export const UserSignoutView = (props: UserSignoutViewProps): React.ReactElement => (
    <section className={props.className}>
        <div className="bulma-container bulma-is-max-tablet">
            <div className="py-6">
                <CustomCard
                    isLoading={props.isLoading}
                    caption={props.caption}
                    text={props.status}
                    icon={<Icon name="Check" size={4.5} />}
                    colour="has-text-success"
                />
            </div>
        </div>
    </section>
);
