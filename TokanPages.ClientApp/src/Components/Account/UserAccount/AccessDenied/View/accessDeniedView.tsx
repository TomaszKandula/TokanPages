import * as React from "react";
import { CustomCard, Icon } from "../../../../../Shared/Components";
import { AccessDeniedViewProps } from "../Types";

export const AccessDeniedView = (props: AccessDeniedViewProps): React.ReactElement => {
    return (
        <section className={props.className}>
            <div className="bulma-container bulma-is-max-desktop">
                <div className="py-6">
                    <CustomCard
                        isLoading={props.isLoading}
                        caption={props.accessDeniedCaption}
                        text={props.accessDeniedPrompt}
                        icon={<Icon name="CloseCircle" size={4.5} />}
                        colour="has-text-danger"
                    />
                </div>
            </div>
        </section>
    );
};
