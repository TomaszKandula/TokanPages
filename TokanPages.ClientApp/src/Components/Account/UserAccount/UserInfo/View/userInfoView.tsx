import * as React from "react";
import { AuthenticateUserResultDto, SectionAccountInformation } from "../../../../../Api/Models";
import { GET_USER_IMAGE } from "../../../../../Api";
import { UserMedia } from "../../../../../Shared/enums";
import { ProgressBar, TextField, UploadUserMedia } from "../../../../../Shared/Components";
import { AccountFormInput } from "../../../../../Shared/Services/FormValidation";
import { ViewProperties } from "../../../../../Shared/Abstractions";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../../../Shared/types";
import { UserInfoProps } from "../userInfo";
import "./userInfoView.css";

interface UserInfoViewProps extends ViewProperties, UserInfoProps {
    fileUploadingCustomHandle?: string;
    userStore: AuthenticateUserResultDto;
    accountForm: AccountFormInput;
    userImageName: string;
    isUserActivated: boolean;
    isRequestingVerification: boolean;
    formProgress: boolean;
    keyHandler: (event: ReactKeyboardEvent) => void;
    formHandler: (event: ReactChangeEvent) => void;
    switchHandler: (event: ReactChangeEvent) => void;
    saveButtonHandler: () => void;
    verifyButtonHandler: () => void;
    sectionAccountInformation: SectionAccountInformation;
    userAbout?: {
        multiline?: boolean;
        minRows?: number;
    };
}

const UpdateAccountButton = (props: UserInfoViewProps): React.ReactElement => {
    return (
        <button
            type="submit"
            onClick={props.saveButtonHandler}
            disabled={props.formProgress}
            className="bulma-button bulma-is-info bulma-is-light"
        >
            {!props.formProgress ? props.sectionAccountInformation?.updateButtonText : <ProgressBar size={20} />}
        </button>
    );
};

const RequestVerificationButton = (props: UserInfoViewProps): React.ReactElement => {
    const clickable = (
        <span onClick={props.verifyButtonHandler} className="has-text-danger is-underlined has-pointer">
            request verification
        </span>
    );

    const link = (
        <>
            <span>&nbsp;(</span>
            {clickable}
            <span>)</span>
        </>
    );

    return props.userStore.isVerified ? <></> : link;
};

const RenderEmailStatus = (props: UserInfoViewProps): React.ReactElement => {
    return props.userStore?.isVerified ? (
        <>{props.sectionAccountInformation?.labelEmailStatus?.positive}</>
    ) : (
        <>{props.sectionAccountInformation?.labelEmailStatus?.negative}</>
    );
};

const RenderBackdrop = (props: UserInfoViewProps) => (
    <>
        {props.isRequestingVerification 
        ? <div className="backdrop">
            <ProgressBar colour="#fff" size={50} thickness={4} />
        </div>
        : null}
    </>
);

export const UserInfoView = (props: UserInfoViewProps): React.ReactElement => {
    const previewImage = GET_USER_IMAGE.replace("{id}", props.userStore.userId).replace(
        "{name}",
        props.userImageName ?? ""
    );

    return (
        <section className={`section ${props.background ?? ""}`}>
            <RenderBackdrop {...props} />
            <div className="bulma-container bulma-is-max-desktop">
                <div className="pt-120 pb-40">
                    <div className="bulma-card">
                        <div className="bulma-card-content">
                            <p className="is-size-4 has-text-grey">
                                {props.sectionAccountInformation?.caption}
                            </p>
                            <hr />
                            <div className="py-4">
                                <div className="bulma-columns cancel-margin-bottom">
                                    <div className="bulma-column bulma-is-one-fifth">
                                        <p className="is-size-6 has-text-grey">
                                            {props.sectionAccountInformation?.labelUserId}
                                        </p>
                                    </div>
                                    <div className="bulma-column">
                                        <p className="is-size-6 has-text-grey">
                                            {props.userStore?.userId}
                                        </p>
                                    </div>
                                </div>
                                <div className="bulma-columns cancel-margin-bottom">
                                    <div className="bulma-column bulma-is-one-fifth">
                                        <p className="is-size-6 has-text-grey">
                                            {props.sectionAccountInformation?.labelEmailStatus?.label}
                                        </p>
                                    </div>
                                    <div className="bulma-column">
                                        <p className="is-size-6 has-text-grey">
                                            <RenderEmailStatus {...props} />
                                            <RequestVerificationButton {...props} />
                                        </p>
                                    </div>
                                </div>
                                <div className="bulma-columns cancel-margin-bottom">
                                    <div className="bulma-column bulma-is-one-fifth">
                                        <p className="is-size-6 has-text-grey">
                                            {props.sectionAccountInformation?.labelUserAlias}
                                        </p>
                                    </div>
                                    <div className="bulma-column">
                                        <p className="is-size-6 has-text-grey">
                                            {props.userStore?.aliasName}
                                        </p>
                                    </div>
                                </div>
                                <hr />
                                <div className="bulma-columns cancel-margin-bottom">
                                    <div className="bulma-column bulma-is-one-fifth is-align-self-center">
                                        <p className="is-size-6 has-text-grey">
                                            {props.sectionAccountInformation?.labelUserAvatar}
                                        </p>
                                    </div>
                                    <div className="bulma-column">
                                        {props.isLoading ? null : (
                                            <UploadUserMedia
                                                customHandle={props.fileUploadingCustomHandle}
                                                mediaTarget={UserMedia.userImage}
                                                handle="userInfoSection_userImage"
                                                previewImage={previewImage}
                                            />
                                        )}
                                    </div>
                                </div>
                                <hr />
                                <div className="bulma-columns cancel-margin-bottom">
                                    <div className="bulma-column bulma-is-one-fifth is-align-self-center">
                                        <p className="is-size-6 has-text-grey">
                                            {props.sectionAccountInformation?.labelFirstName}
                                        </p>
                                    </div>
                                    <div className="bulma-column">
                                        <TextField
                                            required
                                            fullWidth
                                            uuid="firstName"
                                            value={props.accountForm?.firstName}
                                            onKeyUp={props.keyHandler}
                                            onChange={props.formHandler}
                                        />
                                    </div>
                                </div>
                                <div className="bulma-columns cancel-margin-bottom">
                                    <div className="bulma-column bulma-is-one-fifth is-align-self-center">
                                        <p className="is-size-6 has-text-grey">
                                            {props.sectionAccountInformation?.labelLastName}
                                        </p>
                                    </div>
                                    <div className="bulma-column">
                                        <TextField
                                            required
                                            fullWidth
                                            uuid="lastName"
                                            value={props.accountForm?.lastName}
                                            onKeyUp={props.keyHandler}
                                            onChange={props.formHandler}
                                        />
                                    </div>
                                </div>
                                <div className="bulma-columns cancel-margin-bottom">
                                    <div className="bulma-column bulma-is-one-fifth is-align-self-center">
                                        <p className="is-size-6 has-text-grey">
                                            {props.sectionAccountInformation?.labelEmail}
                                        </p>
                                    </div>
                                    <div className="bulma-column">
                                        <TextField
                                            required
                                            fullWidth
                                            uuid="email"
                                            value={props.accountForm?.email}
                                            onKeyUp={props.keyHandler}
                                            onChange={props.formHandler}
                                        />
                                    </div>
                                </div>
                                <div className="bulma-columns cancel-margin-bottom">
                                    <div className="bulma-column bulma-is-one-fifth is-align-self-center">
                                        <p className="is-size-6 has-text-grey">
                                            {props.sectionAccountInformation?.labelShortBio}
                                        </p>
                                    </div>
                                    <div className="bulma-column">
                                        <TextField
                                            required
                                            fullWidth
                                            //multiline={props.userAbout?.multiline}
                                            //minRows={props.userAbout?.minRows}
                                            uuid="userAboutText"
                                            value={props.accountForm?.userAboutText}
                                            onKeyUp={() => {}}
                                            onChange={props.formHandler}
                                        />
                                    </div>
                                </div>
                                <hr />
                                <div className="has-text-right">
                                    <UpdateAccountButton {...props} />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    );
};
