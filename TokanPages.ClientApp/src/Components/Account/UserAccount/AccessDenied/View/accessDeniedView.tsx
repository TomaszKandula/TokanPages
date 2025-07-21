import * as React from "react";
import { CustomCard, Icon } from "../../../../../Shared/Components";
import { AccessDeniedProps } from "../accessDenied";

interface AccessDeniedViewProps extends AccessDeniedProps {
    isLoading: boolean;
    languageId: string;
    accessDeniedCaption: string;
    accessDeniedPrompt: string[];
    homeButtonText: string;
}

export const AccessDeniedView = (props: AccessDeniedViewProps): React.ReactElement => {
    return (
        <section className={props.background}>
            <div className="bulma-container bulma-is-max-desktop">
                <div className="py-6">
                    <CustomCard
                        isLoading={props.isLoading}
                        caption={props.accessDeniedCaption}
                        text={props.accessDeniedPrompt}
                        icon={<Icon name="CloseCircle" size={3} />}
                        colour="has-text-danger"
                    />
                </div>
            </div>
        </section>
    );
};
