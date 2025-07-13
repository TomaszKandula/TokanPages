import * as React from "react";
import { SectionAccountPassword } from "../../../../../Api/Models";
import { ViewProperties } from "../../../../../Shared/Abstractions";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../../../Shared/types";
import { ProgressBar, TextFieldWithPassword } from "../../../../../Shared/Components";
import { UserPasswordProps } from "../userPassword";

interface UserPasswordViewProps extends ViewProperties, UserPasswordProps {
    oldPassword: string;
    newPassword: string;
    confirmPassword: string;
    formProgress: boolean;
    keyHandler: (event: ReactKeyboardEvent) => void;
    formHandler: (event: ReactChangeEvent) => void;
    buttonHandler: () => void;
    sectionAccountPassword: SectionAccountPassword;
}

const UpdatePasswordButton = (props: UserPasswordViewProps): React.ReactElement => {
    return (
        <button
            type="submit"
            onClick={props.buttonHandler}
            disabled={props.formProgress}
            className="bulma-button bulma-is-info bulma-is-light"
        >
            {!props.formProgress ? props.sectionAccountPassword?.updateButtonText : <ProgressBar size={20} />}
        </button>
    );
};

export const UserPasswordView = (props: UserPasswordViewProps): React.ReactElement => {
    return (
        <section className={props.background}>
            <div className="bulma-container bulma-is-max-desktop">
                <div className={!props.className ? "py-6" : props.className}>
                    <div className="bulma-card">
                        <div className="bulma-card-content">
                            <p className="is-size-4 has-text-grey">{props.sectionAccountPassword?.caption}</p>
                            <hr />
                            <div className="py-2">
                                <TextFieldWithPassword
                                    uuid="oldPassword"
                                    value={props.oldPassword}
                                    onKeyUp={props.keyHandler}
                                    onChange={props.formHandler}
                                    placeholder={props.sectionAccountPassword?.labelOldPassword}
                                />
                            </div>
                            <div className="py-2">
                                <TextFieldWithPassword
                                    uuid="newPassword"
                                    value={props.newPassword}
                                    onKeyUp={props.keyHandler}
                                    onChange={props.formHandler}
                                    placeholder={props.sectionAccountPassword?.labelNewPassword}
                                />
                            </div>
                            <div className="py-2">
                                <TextFieldWithPassword
                                    uuid="confirmPassword"
                                    value={props.confirmPassword}
                                    onKeyUp={props.keyHandler}
                                    onChange={props.formHandler}
                                    placeholder={props.sectionAccountPassword?.labelConfirmPassword}
                                />
                            </div>
                            <hr />
                            <div className="has-text-right">
                                <UpdatePasswordButton {...props} />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    );
};
