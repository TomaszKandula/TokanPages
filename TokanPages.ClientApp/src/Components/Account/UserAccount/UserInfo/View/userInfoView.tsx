import * as React from "react";
import { AuthenticateUserResultDto, SectionAccountInformation } from "../../../../../Api/Models";
import { GET_USER_IMAGE } from "../../../../../Api";
import { UserMedia } from "../../../../../Shared/enums";
import {
    Backdrop,
    ProgressBar,
    Skeleton,
    TextArea,
    TextField,
    UploadUserMedia,
} from "../../../../../Shared/Components";
import { AccountFormInput } from "../../../../../Shared/Services/FormValidation";
import { ViewProperties } from "../../../../../Shared/Abstractions";
import { ReactChangeEvent, ReactChangeTextEvent, ReactKeyboardEvent } from "../../../../../Shared/types";
import { UserInfoProps } from "../userInfo";
import Validate from "validate.js";
import "./userInfoView.css";

interface UserInfoViewProps extends ViewProperties, UserInfoProps {
    isMobile: boolean;
    fileUploadingCustomHandle?: string;
    userStore: AuthenticateUserResultDto;
    accountForm: AccountFormInput;
    userImageName: string;
    isRequestingVerification: boolean;
    formProgress: boolean;
    keyHandler: (event: ReactKeyboardEvent) => void;
    formHandler: (event: ReactChangeEvent) => void;
    descriptionHandler: (event: ReactChangeTextEvent) => void;
    saveButtonHandler: () => void;
    verifyButtonHandler: () => void;
    sectionAccountInformation: SectionAccountInformation;
    description?: {
        minRows?: number;
        message: string;
    };
}

const UpdateAccountButton = (props: UserInfoViewProps): React.ReactElement => (
    <button
        type="submit"
        onClick={props.saveButtonHandler}
        disabled={props.formProgress}
        className="bulma-button bulma-is-info bulma-is-light"
    >
        {!props.formProgress ? props.sectionAccountInformation?.updateButtonText : <ProgressBar size={20} />}
    </button>
);

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

const GetImageURL = (props: UserInfoViewProps): string =>
    GET_USER_IMAGE.replace("{id}", props.userStore.userId).replace("{name}", props.userImageName);

export const UserInfoView = (props: UserInfoViewProps): React.ReactElement => {
    const previewImage = Validate.isEmpty(props.userImageName) ? "" : GetImageURL({ ...props });

    return (
        <section className={props.className}>
            <Backdrop isLoading={props.isRequestingVerification} />
            <div className="bulma-container bulma-is-max-desktop">
                <div className="mt-6 mb-4">
                    <div className={`bulma-card ${props.isMobile ? "mx-4" : ""}`}>
                        <div className="bulma-card-content">
                            <Skeleton isLoading={props.isLoading} mode="Text" height={24} disableMarginY>
                                <p className="is-size-4 has-text-grey">{props.sectionAccountInformation?.caption}</p>
                            </Skeleton>
                            <hr />
                            <div className="bulma-columns m-0 py-2">
                                <div className="bulma-column bulma-is-one-fifth p-0">
                                    <Skeleton isLoading={props.isLoading} mode="Text" height={24} disableMarginY>
                                        <p className="is-size-6 has-text-grey-darker">
                                            {props.sectionAccountInformation?.labelUserId}
                                        </p>
                                    </Skeleton>
                                </div>
                                <div className="bulma-column p-0">
                                    <Skeleton isLoading={props.isLoading} mode="Text" height={24} disableMarginY>
                                        <p className="is-size-6 has-text-grey">{props.userStore?.userId}</p>
                                    </Skeleton>
                                </div>
                            </div>
                            <div className="bulma-columns m-0 py-2">
                                <div className="bulma-column bulma-is-one-fifth p-0">
                                        <Skeleton isLoading={props.isLoading} mode="Text" height={24} disableMarginY>
                                            <p className="is-size-6 has-text-grey-darker">
                                                {props.sectionAccountInformation?.labelEmailStatus?.label}
                                            </p>
                                        </Skeleton>
                                </div>
                                <div className="bulma-column p-0">
                                    <Skeleton isLoading={props.isLoading} mode="Text" height={24} disableMarginY>
                                        <p className="is-size-6 has-text-grey">
                                            <RenderEmailStatus {...props} />
                                            <RequestVerificationButton {...props} />
                                        </p>
                                    </Skeleton>
                                </div>
                            </div>
                            <div className="bulma-columns m-0 py-2">
                                <div className="bulma-column bulma-is-one-fifth p-0">
                                    <Skeleton isLoading={props.isLoading} mode="Text" height={24} disableMarginY>
                                        <p className="is-size-6 has-text-grey-darker">
                                            {props.sectionAccountInformation?.labelUserAlias}
                                        </p>
                                    </Skeleton>
                                </div>
                                <div className="bulma-column p-0">
                                    <Skeleton isLoading={props.isLoading} mode="Text" height={24} disableMarginY>
                                        <p className="is-size-6 has-text-grey">{props.userStore?.aliasName}</p>
                                    </Skeleton>
                                </div>
                            </div>
                            <hr />
                            <div className="bulma-block m-0 py-2">
                                <div className="is-flex is-align-items-center p-0">
                                    <Skeleton isLoading={props.isLoading} mode="Text" height={24} disableMarginY>
                                        <p className="is-size-6 has-text-grey-darker mr-4">
                                            {props.sectionAccountInformation?.labelUserAvatar}
                                        </p>
                                    </Skeleton>
                                    <Skeleton
                                        isLoading={props.isLoading}
                                        mode="Circle"
                                        width={48}
                                        height={48}
                                        disableMarginY
                                    >
                                        <UploadUserMedia
                                            customHandle={props.fileUploadingCustomHandle}
                                            mediaTarget={UserMedia.userImage}
                                            handle="userInfoSection_userImage"
                                            previewImage={previewImage}
                                        />
                                    </Skeleton>
                                </div>
                            </div>
                            <hr />
                            <div className="bulma-columns m-0 py-2">
                                    <div className="bulma-column bulma-is-one-fifth is-align-self-center p-0">
                                        <Skeleton isLoading={props.isLoading} mode="Text" height={24} disableMarginY>
                                            <p className="is-size-6 has-text-grey-darker">
                                                {props.sectionAccountInformation?.labelFirstName}
                                            </p>
                                        </Skeleton>
                                    </div>
                                    <div className="bulma-column p-0">
                                        <Skeleton isLoading={props.isLoading} mode="Rect" disableMarginY>
                                            <TextField
                                                required
                                                uuid="firstName"
                                                value={props.accountForm?.firstName}
                                                onKeyUp={props.keyHandler}
                                                onChange={props.formHandler}
                                            />
                                        </Skeleton>
                                    </div>
                            </div>
                            <div className="bulma-columns m-0 py-2">
                                    <div className="bulma-column bulma-is-one-fifth is-align-self-center p-0">
                                        <Skeleton isLoading={props.isLoading} mode="Text" height={24} disableMarginY>
                                            <p className="is-size-6 has-text-grey-darker">
                                                {props.sectionAccountInformation?.labelLastName}
                                            </p>
                                        </Skeleton>
                                    </div>
                                    <div className="bulma-column p-0">
                                        <Skeleton isLoading={props.isLoading} mode="Rect" disableMarginY>
                                            <TextField
                                                required
                                                uuid="lastName"
                                                value={props.accountForm?.lastName}
                                                onKeyUp={props.keyHandler}
                                                onChange={props.formHandler}
                                            />
                                        </Skeleton>
                                    </div>
                            </div>
                            <div className="bulma-columns m-0 py-2">
                                    <div className="bulma-column bulma-is-one-fifth is-align-self-center p-0">
                                        <Skeleton isLoading={props.isLoading} mode="Text" height={24} disableMarginY>
                                            <p className="is-size-6 has-text-grey-darker">
                                                {props.sectionAccountInformation?.labelEmail}
                                            </p>
                                        </Skeleton>
                                    </div>
                                    <div className="bulma-column p-0">
                                        <Skeleton isLoading={props.isLoading} mode="Rect" disableMarginY>
                                            <TextField
                                                required
                                                uuid="email"
                                                value={props.accountForm?.email}
                                                onKeyUp={props.keyHandler}
                                                onChange={props.formHandler}
                                            />
                                        </Skeleton>
                                    </div>
                            </div>
                            <div className="bulma-columns m-0 py-2">
                                    <div className="bulma-column bulma-is-one-fifth is-align-self-center p-0">
                                        <Skeleton isLoading={props.isLoading} mode="Text" height={24} disableMarginY>
                                            <p className="is-size-6 has-text-grey-darker">
                                                {props.sectionAccountInformation?.labelDescription}
                                            </p>
                                        </Skeleton>
                                    </div>
                                    <div className="bulma-column p-0">
                                        <Skeleton isLoading={props.isLoading} mode="Rect" disableMarginY>
                                            <TextArea
                                                required
                                                isFixedSize
                                                uuid="description"
                                                rows={props.description?.minRows}
                                                value={props.description?.message}
                                                onChange={props.descriptionHandler}
                                            />
                                        </Skeleton>
                                    </div>
                            </div>
                            <hr />
                            <div className="has-text-right">
                                 <Skeleton isLoading={props.isLoading} mode="Rect" disableMarginY>
                                    <UpdateAccountButton {...props} />
                                </Skeleton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    );
};
