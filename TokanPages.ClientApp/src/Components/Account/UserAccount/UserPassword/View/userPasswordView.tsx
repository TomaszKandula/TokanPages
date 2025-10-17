import * as React from "react";
import { ProgressBar, Skeleton, TextFieldWithPassword } from "../../../../../Shared/Components";
import { UserPasswordViewProps } from "../Types";

const UpdatePasswordButton = (props: UserPasswordViewProps): React.ReactElement => (
    <button
        type="submit"
        onClick={props.buttonHandler}
        disabled={props.formProgress}
        className="bulma-button bulma-is-info bulma-is-light"
    >
        {!props.formProgress ? props.sectionAccountPassword?.updateButtonText : <ProgressBar size={20} />}
    </button>
);

export const UserPasswordView = (props: UserPasswordViewProps): React.ReactElement => (
    <section className={props.className}>
        <div className="bulma-container bulma-is-max-desktop">
            <div className="py-4">
                <div className={`bulma-card ${props.isMobile ? "mx-4" : ""}`}>
                    <div className="bulma-card-content">
                        <Skeleton isLoading={props.isLoading} mode="Rect">
                            <p className="is-size-4 has-text-grey">{props.sectionAccountPassword?.caption}</p>
                        </Skeleton>
                        <hr />
                        <Skeleton isLoading={props.isLoading} mode="Rect">
                            <div className="py-2">
                                <TextFieldWithPassword
                                    uuid="oldPassword"
                                    value={props.oldPassword}
                                    onKeyUp={props.keyHandler}
                                    onChange={props.formHandler}
                                    placeholder={props.sectionAccountPassword?.labelOldPassword}
                                />
                            </div>
                        </Skeleton>
                        <Skeleton isLoading={props.isLoading} mode="Rect">
                            <div className="py-2">
                                <TextFieldWithPassword
                                    uuid="newPassword"
                                    value={props.newPassword}
                                    onKeyUp={props.keyHandler}
                                    onChange={props.formHandler}
                                    placeholder={props.sectionAccountPassword?.labelNewPassword}
                                />
                            </div>
                        </Skeleton>
                        <Skeleton isLoading={props.isLoading} mode="Rect">
                            <div className="py-2">
                                <TextFieldWithPassword
                                    uuid="confirmPassword"
                                    value={props.confirmPassword}
                                    onKeyUp={props.keyHandler}
                                    onChange={props.formHandler}
                                    placeholder={props.sectionAccountPassword?.labelConfirmPassword}
                                />
                            </div>
                        </Skeleton>
                        <hr />
                        <div className="has-text-right">
                            <Skeleton isLoading={props.isLoading} mode="Rect">
                                <UpdatePasswordButton {...props} />
                            </Skeleton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
);
