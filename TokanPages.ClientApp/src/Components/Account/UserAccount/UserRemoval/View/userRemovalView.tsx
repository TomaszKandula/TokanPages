import * as React from "react";
import { SectionAccountRemoval } from "../../../../../Api/Models";
import { ViewProperties } from "../../../../../Shared/Abstractions";
import { ProgressBar, RenderParagraphs } from "../../../../../Shared/Components";
import { UserRemovalProps } from "../userRemoval";

interface UserRemovalViewProps extends ViewProperties, UserRemovalProps {
    deleteButtonHandler: () => void;
    deleteAccountProgress: boolean;
    sectionAccountRemoval: SectionAccountRemoval;
}

const DeleteAccountButton = (props: UserRemovalViewProps): React.ReactElement => {
    return (
        <button
            type="submit"
            onClick={props.deleteButtonHandler}
            disabled={props.deleteAccountProgress}
            className="bulma-button bulma-is-danger bulma-is-light"
        >
            {!props.deleteAccountProgress ? props.sectionAccountRemoval?.deleteButtonText : <ProgressBar size={20} />}
        </button>
    );
};

export const UserRemovalView = (props: UserRemovalViewProps): React.ReactElement => {
    return (
        <section className={`section ${props.background ?? ""}`}>
            <div className="bulma-container bulma-is-max-desktop">
                <div className={!props.className ? "py-6" : props.className}>
                    <div className="bulma-card">
                        <div className="bulma-card-content">
                            <p className="is-size-4 has-text-grey">{props.sectionAccountRemoval?.caption}</p>
                            <hr />
                            <RenderParagraphs
                                text={props.sectionAccountRemoval?.warningText}
                                className="is-size-6 has-text-grey line-height-20"
                            />
                            <hr />
                            <div className="has-text-right">
                                <DeleteAccountButton {...props} />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    );
};
