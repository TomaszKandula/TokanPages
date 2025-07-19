import * as React from "react";
import { ViewProperties } from "../../../../Shared/Abstractions";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../../Shared/types";
import { Animated, ProgressBar, Skeleton, TextField } from "../../../../Shared/Components";

interface NewsletterViewProps extends ViewProperties {
    isMobile: boolean;
    caption: string;
    text: string;
    keyHandler: (event: ReactKeyboardEvent) => void;
    formHandler: (event: ReactChangeEvent) => void;
    email: string;
    buttonHandler: () => void;
    progress: boolean;
    buttonText: string;
    labelEmail: string;
    background?: string;
}

const ActiveButton = (props: NewsletterViewProps): React.ReactElement => (
        <button
            type="submit"
            onClick={props.buttonHandler}
            className="bulma-button bulma-is-link bulma-is-light bulma-is-fullwidth"
            disabled={props.progress}
        >
            {!props.progress ? props.buttonText : <ProgressBar size={20} />}
        </button>
    );

export const NewsletterSectionView = (props: NewsletterViewProps): React.ReactElement => (
        <section className={props.background}>
            <div className="bulma-container">
                <div className="py-6">
                    <div className={props.isMobile ? "p-5" : "p-6"}>
                        <div className="bulma-columns bulma-is-vcentered">
                            <div className="bulma-column">
                                <Animated dataAos="fade-down" dataAosDelay={150}>
                                    <Skeleton isLoading={props.isLoading} mode="Text" height={32}>
                                    <p className="is-size-3 has-text-grey-dark has-text-centered">{props.caption}</p>
                                    </Skeleton>
                                </Animated>
                                <Animated dataAos="zoom-in" dataAosDelay={200}>
                                    <Skeleton isLoading={props.isLoading} mode="Text" height={24}>
                                    <p className="is-size-5 has-text-grey has-text-centered">{props.text}</p>
                                    </Skeleton>
                                </Animated>
                            </div>
                            <div className="bulma-column">
                                <Animated dataAos="zoom-in" dataAosDelay={300}>
                                    <Skeleton isLoading={props.isLoading} mode="Rect" height={40}>
                                    <TextField
                                        required
                                        uuid="email"
                                        autoComplete="email"
                                        onKeyUp={props.keyHandler}
                                        onChange={props.formHandler}
                                        value={props.email}
                                        placeholder={props.labelEmail}
                                    />
                                    </Skeleton>
                                </Animated>
                            </div>
                            <div className="bulma-column">
                                <Animated dataAos="zoom-in" dataAosDelay={350}>
                                    <Skeleton isLoading={props.isLoading} mode="Rect" height={40}>
                                    <ActiveButton {...props} />
                                    </Skeleton>
                                </Animated>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    );
