import * as React from "react";
import { ViewProperties } from "../../../../../Shared/Abstractions";
import { SectionAccountDeactivation } from "../../../../../Api/Models";
import { CustomDivider, ProgressBar, RenderParagraphs } from "../../../../../Shared/Components";
import { UserDeactivationProps } from "../userDeactivation";

interface UserDeactivationViewProps extends ViewProperties, UserDeactivationProps {
    buttonHandler: () => void;
    progress: boolean;
    section: SectionAccountDeactivation;
}

const DeactivationButton = (props: UserDeactivationViewProps): React.ReactElement => {
    return (
        <button
            type="submit"
            onClick={props.buttonHandler}
            disabled={props.progress}
            className="bulma-button bulma-is-danger bulma-is-light"
        >
            {!props.progress ? props.section?.deactivateButtonText : <ProgressBar size={20} />}
        </button>
    );
};

export const UserDeactivationView = (props: UserDeactivationViewProps): React.ReactElement => {
    return (
        <section className={`section ${props.background ?? ""}`}>
            <div className="bulma-container bulma-is-max-desktop">
                <div className="pb-40">
                    <div className="bulma-card">
                        <div className="bulma-card-content">
                            <p className="is-size-4 has-text-grey">
                                {props.section?.caption}
                            </p>
                            <CustomDivider mt={15} mb={8} />
                            <div className="pt-8 pb-8">
                                <RenderParagraphs
                                    text={props.section?.warningText}
                                    className="is-size-6 has-text-grey line-height-20"
                                />
                                <CustomDivider mt={15} mb={15} />
                                <div className="has-text-right">
                                    <DeactivationButton {...props} />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    );
};
