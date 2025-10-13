import * as React from "react";
import { ReactChangeEvent, ViewProperties } from "../../../../Shared/types";
import { Icon, ProgressBar, Skeleton, TextField } from "../../../../Shared/Components";
import { ExtendedViewProps } from "../newsletterUpdate";

interface NewsletterUpdateViewProps extends ViewProperties, ExtendedViewProps {
    isMobile: boolean;
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
                <div className={`bulma-card ${props.isMobile ? "mx-4" : ""}`}>
                    <div className="bulma-card-content">
                        <div className="is-flex is-flex-direction-column is-align-items-center">
                            <Skeleton isLoading={props.isLoading} mode="Circle" width={72} height={72}>
                                <Icon name="AccountCircle" size={2.5} className="card-icon-colour" />
                            </Skeleton>
                            <Skeleton isLoading={props.isLoading} mode="Text" height={24}>
                                <p className="is-size-3 has-text-black">{props.caption}</p>
                            </Skeleton>
                        </div>
                        <div className="bulma-columns is-flex is-flex-direction-column my-5">
                            <div className="bulma-column">
                                <Skeleton isLoading={props.isLoading} mode="Rect" disableMarginY>
                                    <TextField
                                        required
                                        uuid="email"
                                        autoComplete="email"
                                        onChange={props.formHandler}
                                        value={props.email}
                                        placeholder={props.labelEmail}
                                    />
                                </Skeleton>
                            </div>
                            <div className="bulma-column">
                                <Skeleton isLoading={props.isLoading} mode="Rect" disableMarginY>
                                    <ActiveButton {...props} />
                                </Skeleton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
);
