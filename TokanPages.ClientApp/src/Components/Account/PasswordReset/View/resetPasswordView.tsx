import * as React from "react";
import TextField from "@material-ui/core/TextField";
import { Card, CardContent, CircularProgress } from "@material-ui/core";
import Skeleton from "@material-ui/lab/Skeleton";
import { ViewProperties } from "../../../../Shared/Abstractions";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../../Shared/types";
import { PasswordResetProps } from "../passwordReset";
import { Icon } from "Shared/Components";

interface Properties extends ViewProperties, PasswordResetProps {
    progress: boolean;
    caption: string;
    button: string;
    email: string;
    keyHandler: (event: ReactKeyboardEvent) => void;
    formHandler: (event: ReactChangeEvent) => void;
    buttonHandler: () => void;
    labelEmail: string;
}

const ActiveButton = (props: Properties): React.ReactElement => {
    return (
        <button
            type="submit"
            onClick={props.buttonHandler}
            className="bulma-button bulma-is-light bulma-is-fullwidth"
            disabled={props.progress || props.email.length === 0}
        >
            {!props.progress ? props.button : <CircularProgress size={20} />}
        </button>
    );
};

export const PasswordResetView = (props: Properties): React.ReactElement => {
    return (
        <section className={`section ${props.background ?? ""}`}>
            <div className="bulma-container bulma-is-max-tablet">
                <div className={!props.className ? "pt-96 pb-80" : props.className}>
                    
                    <Card elevation={0} className="card">
                        <CardContent className="card-content">
                            
                            <div className="has-text-centered">
                                <Icon name="Account" size={3} className="account" />
                                <p className="caption">
                                    {props.isLoading ? <Skeleton variant="text" /> : props.caption}
                                </p>
                            </div>
                            
                            <div>


                                <TextField
                                    required
                                    fullWidth
                                    id="email"
                                    name="email"
                                    variant="outlined"
                                    autoComplete="email"
                                    onKeyUp={props.keyHandler}
                                    onChange={props.formHandler}
                                    value={props.email}
                                    label={props.labelEmail}
                                />

                                <ActiveButton {...props} />


                            </div>

                        </CardContent>
                    </Card>

                </div>
            </div>

        </section>
    );
};
