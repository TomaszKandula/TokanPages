import { AuthenticateUserResultDto, SectionAccountInformation } from "../../../../../Api/Models";
import { ValidateAccountFormProps } from "../../../../../Shared/Services/FormValidation";
import {
    ReactChangeEvent,
    ReactChangeTextEvent,
    ReactKeyboardEvent,
    ViewProperties,
} from "../../../../../Shared/Types";

export interface UpdateStoreProps {
    canUpdate: boolean;
    isVerified: boolean;
}

export interface UserInfoProps {
    className?: string;
}

export interface UserInfoViewProps extends ViewProperties, UserInfoProps {
    isMobile: boolean;
    fileUploadingCustomHandle?: string;
    userStore: AuthenticateUserResultDto;
    accountForm: ValidateAccountFormProps;
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
