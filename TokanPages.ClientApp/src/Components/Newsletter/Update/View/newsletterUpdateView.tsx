import * as React from "react";
import { ViewProperties } from "../../../../Shared/Abstractions";
import { ReactChangeEvent } from "../../../../Shared/types";
import { Icon, ProgressBar, TextField } from "../../../../Shared/Components";
import { ExtendedViewProps } from "../newsletterUpdate";

interface NewsletterUpdateViewProps extends ViewProperties, ExtendedViewProps {
    caption: string;
    formHandler: (event: ReactChangeEvent) => void;
    email: string;
    buttonHandler: () => void;
    buttonState: boolean;
    progress: boolean;
    buttonText: string;
    labelEmail: string;
}

const ActiveButton = (props: NewsletterUpdateViewProps): React.ReactElement => (
        <button
            onClick={props.buttonHandler}
            className="bulma-button bulma-is-link bulma-is-light bulma-is-fullwidth"
            disabled={props.progress || !props.buttonState}
        >
            {!props.progress ? props.buttonText : <ProgressBar size={20} />}
        </button>
    );

export const NewsletterUpdateView = (props: NewsletterUpdateViewProps): React.ReactElement => (
        <section className="section">
            <div className="bulma-container bulma-is-max-tablet">
                <div className={!props.className ? "py-6" : props.className}>
                    <div className="bulma-card">
                        <div className="bulma-card-content">
                            <div className="has-text-centered">
                                <Icon name="AccountCircle" size={3} className="has-text-link" />
                                <p className="is-size-3 has-text-grey">{props.caption}</p>
                            </div>
                            <div className="bulma-columns is-flex is-flex-direction-column my-5">
                                <div className="bulma-column">
                                    <TextField
                                        required
                                        uuid="email"
                                        autoComplete="email"
                                        onChange={props.formHandler}
                                        value={props.email}
                                        placeholder={props.labelEmail}
                                    />
                                </div>
                                <div className="bulma-column">
                                    <ActiveButton {...props} />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    );
