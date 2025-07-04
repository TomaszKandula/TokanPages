import * as React from "react";
import { ViewProperties } from "../../../../Shared/Abstractions";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../../Shared/types";
import { Animated, ProgressBar, TextField } from "../../../../Shared/Components";

interface NewsletterViewProps extends ViewProperties {
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

const ActiveButton = (props: NewsletterViewProps): React.ReactElement => {
    return (
        <button
            type="submit"
            onClick={props.buttonHandler}
            className="bulma-button bulma-is-link bulma-is-light bulma-is-fullwidth"
            disabled={props.progress}
        >
            {!props.progress ? props.buttonText : <ProgressBar size={20} />}
        </button>
    );
};

export const NewsletterSectionView = (props: NewsletterViewProps): React.ReactElement => {
    return (
        <section className={`section ${props.background ?? ""}`}>
            <div className="bulma-container">
                <div className="py-6">
                    <div className="p-6">
                        <div className="bulma-columns bulma-is-vcentered">
                            <div className="bulma-column">
                                <Animated dataAos="fade-down" dataAosDelay={150}>
                                    <p className="is-size-3 has-text-grey-dark has-text-centered">
                                        {props.caption}
                                    </p>
                                </Animated>
                                <Animated dataAos="zoom-in" dataAosDelay={200}>
                                    <p className="is-size-5 has-text-grey has-text-centered">
                                        {props.text}
                                    </p>
                                </Animated>
                            </div>
                            <div className="bulma-column">
                                <Animated dataAos="zoom-in" dataAosDelay={300}>
                                    <TextField
                                        required
                                        fullWidth
                                        uuid="email_newletter"
                                        autoComplete="email"
                                        onKeyUp={props.keyHandler}
                                        onChange={props.formHandler}
                                        value={props.email}
                                        placeholder={props.labelEmail}
                                    />
                                </Animated>
                            </div>
                            <div className="bulma-column">
                                <Animated dataAos="zoom-in" dataAosDelay={350}>
                                    <ActiveButton {...props} />
                                </Animated>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    );
};
