import * as React from "react";
import { ProgressBar, RenderParagraphs, Skeleton } from "../../../../../Shared/Components";
import { UserDeactivationViewProps } from "../Types";

const DeactivationButton = (props: UserDeactivationViewProps): React.ReactElement => (
    <button
        type="submit"
        onClick={props.buttonHandler}
        disabled={props.progress}
        className="bulma-button bulma-is-danger bulma-is-light"
    >
        {!props.progress ? props.section?.deactivateButtonText : <ProgressBar size={20} />}
    </button>
);

export const UserDeactivationView = (props: UserDeactivationViewProps): React.ReactElement => (
    <section className={props.className}>
        <div className="bulma-container bulma-is-max-desktop">
            <div className="py-4">
                <div className={`bulma-card ${props.isMobile ? "mx-4" : ""}`}>
                    <div className="bulma-card-content">
                        <Skeleton isLoading={props.isLoading} mode="Rect">
                            <p className="is-size-4 has-text-grey">{props.section?.caption}</p>
                        </Skeleton>
                        <hr />
                        <RenderParagraphs
                            isLoading={props.isLoading}
                            text={props.section?.warningText}
                            className="is-size-6 has-text-grey line-height-20"
                        />
                        <hr />
                        <div className="has-text-right">
                            <Skeleton isLoading={props.isLoading} mode="Rect">
                                <DeactivationButton {...props} />
                            </Skeleton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
);
